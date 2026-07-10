using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

namespace com.humanc.rsconn
{

    /// <summary>
    /// 콘솔 프로젝트 연동 구간
    /// </summary>
    
    public class ConsoleProcessStart
    {
        public RSEvent m_rsEvent = new RSEvent();

        [System.Serializable]
        public class RSEvent : UnityEvent<string> { }

        //프로세스 
        public Process process;
        public string data = "";
        private RsConnection rsConnection;
        /// <summary>
        /// 프로세스 연동 설정 구간
        /// </summary>
        public void OnInit(RsVO rsVO, RsConnection rsConnection) {
            // this.m_rsEvent = m_rsEvent;
            this.rsConnection = rsConnection;
            process = new Process();
            System.Diagnostics.ProcessStartInfo proinfo = new ProcessStartInfo();
            proinfo.FileName = Application.streamingAssetsPath + Path.DirectorySeparatorChar + "Rs232_Console_Connection.exe";
            proinfo.Arguments = " "+rsVO.ComName+" "+ rsVO.Bit+"";
            proinfo.CreateNoWindow = true;
            proinfo.UseShellExecute = false;
            process.EnableRaisingEvents = false;

            proinfo.RedirectStandardOutput = true;
            proinfo.RedirectStandardInput = true;
            proinfo.RedirectStandardError = true;
            process.StartInfo = proinfo;
            process.OutputDataReceived += new DataReceivedEventHandler(DataReceived);
            process.ErrorDataReceived += Process_ErrorDataReceived;
            process.Start();

            process.BeginOutputReadLine();
            UnityEngine.Debug.Log("[Rs232 Process Start]");
            rsConnection.OnResetData();
        }

        private void Process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            UnityEngine.Debug.Log("[Process_ErrorDataReceived]");

        }

        /// <summary>
        /// 리시버 이벤트 핸들러 작동구간 (수정 : 추가적인 작업 필요함)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        void DataReceived(object sender, DataReceivedEventArgs eventArgs)
        {
            data = eventArgs.Data;
            m_rsEvent.Invoke(data);
        }

        //콘솔 데이터 전송
        public void SendConsole(string message) {
            if (process != null)
            {
                //UnityEngine.Debug.Log(message);
                process.StandardInput.WriteLine(message);
            }
        }

        /// <summary>
        /// 프로세스 종료 
        /// 예외 처리 작업  완료 
        /// </summary>
        public void ProcessKill() {
            try
            {
                if (process != null)
                {
                    //프로세스 종료 구간 
                    process.Kill();
                }
            }
            catch (Exception e) { UnityEngine.Debug.Log("[Kill Error]"); }
        }

    }
}