using com.humanc.rsconn;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.LookDev;
using static com.humanc.rsconn.ConsoleProcessStart;
using static OutsideProcessControl;

public class OutsideProcessControl : MonoBehaviour
{
    //외부 컨트롤 데이터 정보 
    public MissionDataLoader missionDataLoader;

    [Header("외부 파일 로드 경로")]
    [SerializeField]
    private string processFile;

    [Header("프로세스 데이터 리스트")]
    public List<ProcessData> processDataList;


    [Header("이벤트 핸들러")]
    public RsEventHandler rsEventHandler = new RsEventHandler();

    [System.Serializable]
    public class RsEventHandler : UnityEvent<string> { }

    public delegate void RsReceivedDelegate(string data);
    public static event RsReceivedDelegate RsReceived;

    public static void RsReceivedData(string data) {
        if (RsReceived != null) {
            RsReceived.Invoke(data);
        }
    }

    /*public delegate void myEventData(string data);
    public event myEventData;*/

    [System.Serializable]
    public class ProcessData {
        //외부 프로세스 정보
        public Process process;

        [Header("프로세스 인덱스 정보")]
        public int processIndex;

        [Header("rs232 comName")]
        public string comName;

        [Header("rs232 bit")]
        public int bit;

        public string data = "";

    }


    //프로그램 실행 컨트롤
    IEnumerator Start() {
        yield return null;
        ProcessPlay();
       // yield return StartCoroutine(OnLoop());
    }

    //외부 프로세스 실행
    private void ProcessPlay() {
        for (int i=0;i< processDataList.Count; i++) {

            ProcessData processData = processDataList[i];
            
            //프로세스 실행 컨트롤 
            processData.process = new Process();

            System.Diagnostics.ProcessStartInfo proinfo = new ProcessStartInfo();
            proinfo.FileName = Application.streamingAssetsPath + Path.DirectorySeparatorChar + processFile;
            proinfo.Arguments = " " + processData.comName + " " + processData. bit + " "+ i;

            proinfo.CreateNoWindow = true;
            proinfo.UseShellExecute = false;

            processData.process.EnableRaisingEvents = false;

            proinfo.RedirectStandardOutput = true;
            proinfo.RedirectStandardInput = true;
            proinfo.RedirectStandardError = true;

            processData.process.StartInfo = proinfo;
            processData.process.OutputDataReceived += DataReceived;
            processData.process.ErrorDataReceived += Process_ErrorDataReceived;
            processData.process.Start();

            processData.process.BeginOutputReadLine();
            processData.processIndex = i;
            UnityEngine.Debug.Log("[Rs232 Process Start] index : "+ processData.processIndex);
            //rsConnection.OnResetData();

        }

       
    }

    /*
    IEnumerator OnLoop() {
        while (true)
        {
            yield return null;
            UnityEngine.Debug.Log("On Loop]");
            if (string.IsNullOrEmpty(data)) {
                RsReceivedData(data);
                //저장되어있는 데이터 정보 초기화 
                //data = "";
            }

        }
    }*/

    private void Update()
    {
        if (!string.IsNullOrEmpty(data))
        {
            RsReceivedData(data); 
           
            data = "";
        }
        
    }
    private void Process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
    {
        UnityEngine.Debug.Log("[Process_ErrorDataReceived]");

    }
    public string data;
    /// <summary>
    /// 리시버 이벤트 핸들러 작동구간 (수정 : 추가적인 작업 필요함)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="eventArgs"></param>
    void DataReceived(object sender, DataReceivedEventArgs eventArgs)
    { 
        data = eventArgs.Data;
        
        //// RsReceivedData(data);
    }

    public void SendConsole(int index,string message)
    {
        if (processDataList[index] != null && processDataList[index].process != null)
        {
            Process process = processDataList[index].process;
            if (process != null)
            {
                //UnityEngine.Debug.Log("전송 데이터 정보"+ processDataList[index].comName);
                //UnityEngine.Debug.Log("전송 데이터 정보" + message);
                if (message.Equals("1"))
                {
                    UnityEngine.Debug.Log("볼 출력 진행: 인덱스" + index);
                }
                else {
                    UnityEngine.Debug.Log("볼 바람 종료 진행: 인덱스" + index);
                }
                string s = missionDataLoader.jsonLoadData.windowsSetting.startData;
                string e= missionDataLoader.jsonLoadData.windowsSetting.endData;
                process.StandardInput.WriteLine(s+message+e);
            }
        }
        else {
            UnityEngine.Debug.Log("연결되지 않음");
        }
       
    }


    public virtual void ReceiverData(string data) { }

    /*

    private void Update() {
        UnityEngine.Debug.Log("Data");
        if (string.IsNullOrEmpty(data))
        {
            UnityEngine.Debug.Log(data);
            data = "";
        }
    }*/


    //프로그램 실행 경로 
    private void ProcessStart() { 
    
    }


    public virtual void OnInit() { 
            
    }


    /// <summary>
    /// 프로세스 종료
    /// </summary>
    public void OnApplicationQuit()
    {
        for (int i = 0; i < processDataList.Count; i++)
        {
            if (processDataList[i].process != null) {
                processDataList[i].process.Kill();
            }
        }
    }

}
