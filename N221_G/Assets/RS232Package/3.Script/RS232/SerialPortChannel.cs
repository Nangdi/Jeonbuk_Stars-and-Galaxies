using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

// 시리얼 포트 1개에 해당하는 채널. 컨트롤러당 1개 인스턴스를 사용한다.
public class SerialPortChannel
{
    private const int MaxBufferSize = 64 * 1024;
    private const int MaxQueueSize = 1024;
    private const int IdlePollDelayMs = 10;
    private const int WriteTimeoutMs = 500;
    private const int CloseWaitMs = 1000;
    private const int ReadStuckWarnMs = 2000;

    public int ControllerId { get; }
    public string Com { get; }
    public int BaudLate { get; }
    public bool IsOpen => serialPort != null && serialPort.IsOpen;

    private readonly Action<int, string> onMessage;
    private readonly Action<int, string> onSent;
    private readonly Action<int, bool> onConnectionChanged;

    private SerialPort serialPort;
    private CancellationTokenSource cancellationTokenSource;
    private readonly StringBuilder serialBuffer = new StringBuilder();
    private readonly Queue<string> dataQueue = new Queue<string>();

    public SerialPortChannel(int controllerId, string com, int baudLate,
        Action<int, string> onMessage,
        Action<int, string> onSent = null,
        Action<int, bool> onConnectionChanged = null)
    {
        ControllerId = controllerId;
        Com = com;
        BaudLate = baudLate;
        this.onMessage = onMessage;
        this.onSent = onSent;
        this.onConnectionChanged = onConnectionChanged;
    }

    public bool Open()
    {
        try
        {
            serialPort = new SerialPort(Com, BaudLate, Parity.None, 8, StopBits.One);
            serialPort.Encoding = Encoding.ASCII; // 장치 규약이 ASCII 가정. 변경 시 송/수신 양쪽 함께 변경 필요
            serialPort.NewLine = "\r\n"; // 송신(WriteLine) 종결자를 기존 exe 브리지와 동일하게 CR+LF로 맞춤
            // 기본값(무한 대기)이면 장치/드라이버가 멈췄을 때 송신이 메인 스레드를 영구히 붙잡아 게임이 응답없음이 됨
            serialPort.WriteTimeout = WriteTimeoutMs;
            serialPort.ReadTimeout = WriteTimeoutMs;
            Debug.Log($"[Ctrl {ControllerId}] 포트연결시도 ({Com})");
            serialPort.Open();
        }
        catch (Exception ex)
        {
            Debug.LogError($"[Ctrl {ControllerId}] 포트 열기 실패 ({Com}): {ex.Message}");
            onConnectionChanged?.Invoke(ControllerId, false);
            return false;
        }

        Debug.Log($"[Ctrl {ControllerId}] 연결완료 ({Com})");
        onConnectionChanged?.Invoke(ControllerId, true);
        StartReaderLoop();
        return true;
    }

    private async void StartReaderLoop()
    {
        cancellationTokenSource = new CancellationTokenSource();
        var token = cancellationTokenSource.Token;

        while (!token.IsCancellationRequested && IsOpen)
        {
            try
            {
                var readTask = Task.Run(() => ReadSerialData(), token);

                // 감시: 읽기 1회는 보통 즉시 끝난다. 오래 안 끝나면 드라이버/USB 변환기에서 멈춘 것 →
                // 이후 데이터 수신 불가 + 종료 시 프로그램이 안 꺼지는 원인이므로 기록한다.
                if (await Task.WhenAny(readTask, Task.Delay(ReadStuckWarnMs, token)) != readTask)
                {
                    Debug.LogError($"[Ctrl {ControllerId}] 수신 멈춤 감지({Com}) - 읽기가 {ReadStuckWarnMs}ms 넘게 반환되지 않음 (USB-시리얼 드라이버/변환기 문제 의심)");
                    onConnectionChanged?.Invoke(ControllerId, false);
                }
                string input = await readTask;

                if (!string.IsNullOrEmpty(input))
                {
                    Debug.Log($"[Ctrl {ControllerId}] 받은데이터 : {input}");
                    onMessage?.Invoke(ControllerId, input);
                }
                else
                {
                    // ReadExisting 은 즉시 반환하므로 idle 시 슬립 없으면 CPU 점유됨
                    await Task.Delay(IdlePollDelayMs, token);
                }
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (Exception ex)
            {
                // 수신 중단 원인 추적용 - 예외를 조용히 삼키지 않고 기록
                Debug.LogError($"[Ctrl {ControllerId}] 수신 처리 오류: {ex}");
                try { await Task.Delay(IdlePollDelayMs, token); }
                catch (OperationCanceledException) { break; }
            }
        }

        if (!token.IsCancellationRequested)
            Debug.LogError($"[Ctrl {ControllerId}] 수신 루프 종료됨 - 포트 닫힘 ({Com}). 이후 데이터는 받지 못함");
    }

    private string ReadSerialData()
    {
        if (dataQueue.Count > 0)
        {
            return dataQueue.Dequeue();
        }

        if (!IsOpen) return "";

        string input = serialPort.ReadExisting();
        if (!string.IsNullOrEmpty(input))
        {
            serialBuffer.Append(input);

            if (serialBuffer.Length > MaxBufferSize)
            {
                Debug.LogWarning($"[Ctrl {ControllerId}] 수신 버퍼 임계 초과({serialBuffer.Length}B) - 폐기");
                serialBuffer.Clear();
            }

            ExtractMessages();
        }

        return dataQueue.Count > 0 ? dataQueue.Dequeue() : "";
    }

    // 마지막 줄바꿈 뒤의 미완성 데이터는 다음 수신과 이어붙이기 위해 버퍼에 보존한다.
    private void ExtractMessages()
    {
        string buffer = serialBuffer.ToString();
        int cursor = 0;

        while (cursor < buffer.Length)
        {
            int crIdx = buffer.IndexOf('\r', cursor);
            int lfIdx = buffer.IndexOf('\n', cursor);

            int delimIdx;
            int delimLen;

            if (crIdx == -1 && lfIdx == -1)
            {
                break;
            }
            else if (crIdx == -1)
            {
                delimIdx = lfIdx;
                delimLen = 1;
            }
            else if (lfIdx == -1)
            {
                delimIdx = crIdx;
                delimLen = 1;
            }
            else if (crIdx < lfIdx)
            {
                delimIdx = crIdx;
                delimLen = (lfIdx == crIdx + 1) ? 2 : 1; // \r\n 한 묶음 처리
            }
            else
            {
                delimIdx = lfIdx;
                delimLen = 1;
            }

            string message = buffer.Substring(cursor, delimIdx - cursor).Trim();
            if (!string.IsNullOrEmpty(message))
            {
                if (dataQueue.Count >= MaxQueueSize)
                {
                    Debug.LogWarning($"[Ctrl {ControllerId}] 수신 큐 가득참 - 오래된 메시지 폐기");
                    dataQueue.Dequeue();
                }
                dataQueue.Enqueue(message);
            }
            cursor = delimIdx + delimLen;
        }

        if (cursor > 0)
        {
            string remaining = buffer.Substring(cursor);
            serialBuffer.Clear();
            serialBuffer.Append(remaining);
        }
    }

    public void Send(string message)
    {
        if (!IsOpen)
        {
            Debug.LogWarning($"[Ctrl {ControllerId}] 포트가 열려 있지 않음 - 송신 실패");
            return;
        }

        try
        {
            serialPort.WriteLine(message);
            Debug.Log($"[Ctrl {ControllerId}] Sent: {message}");
            onSent?.Invoke(ControllerId, message);
        }
        catch (TimeoutException)
        {
            Debug.LogError($"[Ctrl {ControllerId}] 송신 타임아웃({WriteTimeoutMs}ms) - 장치/USB 변환기 응답없음: {message}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"[Ctrl {ControllerId}] 송신 오류: {ex.Message}");
        }
    }

    public void Close()
    {
        bool wasOpen = IsOpen;

        if (cancellationTokenSource != null)
        {
            try { cancellationTokenSource.Cancel(); }
            catch { /* 이미 dispose 됐을 수 있음 */ }
            cancellationTokenSource.Dispose();
            cancellationTokenSource = null;
        }

        if (serialPort != null)
        {
            // 장치/드라이버가 멈춘 상태에서 Close()가 영구히 블록되어 프로그램이 종료되지 않는 문제 방지:
            // 별도 스레드에서 닫고 일정 시간만 기다린 뒤 진행한다.
            var port = serialPort;
            serialPort = null;
            var closeTask = Task.Run(() =>
            {
                try
                {
                    if (port.IsOpen) port.Close();
                    port.Dispose();
                }
                catch (Exception ex)
                {
                    Debug.LogWarning($"[Ctrl {ControllerId}] 포트 종료 중 예외: {ex.Message}");
                }
            });
            if (!closeTask.Wait(CloseWaitMs))
                Debug.LogError($"[Ctrl {ControllerId}] 포트 종료 응답없음({Com}) - 대기 생략하고 진행");
        }

        if (wasOpen) onConnectionChanged?.Invoke(ControllerId, false);
    }
}
