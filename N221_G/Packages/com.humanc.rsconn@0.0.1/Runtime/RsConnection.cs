using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using static com.humanc.rsconn.ConsoleProcessStart;

namespace com.humanc.rsconn
{

    //RS232 연동 모듈
    public class RsConnection : MonoBehaviour
    {
        private Coroutine onInitCoroutine = null;

        /// <summary>
        /// 콘솔 연동 
        /// </summary>
        private ConsoleProcessStart consoleProcessStart;

        //초기화 리셋 데이터 정보
        public string resetData = "";
        public float resetTime = 0;
        public RSDataEvent rsEvent = new RSDataEvent();

        [System.Serializable]
        public class RSDataEvent : UnityEvent<string> { }

        // Start is called before the first frame update
        void Start()
        {
            RemoveCoroutine(onInitCoroutine);
            onInitCoroutine =StartCoroutine(OnInit());
        }

        /// <summary>
        /// 코루틴 초기화 구간 
        /// </summary>
        /// <param name="coroutine"></param>
        private void RemoveCoroutine(Coroutine coroutine) {
            if (coroutine != null) {
                StopCoroutine(coroutine);
                coroutine = null;
            }
        }

        //연동 초기 설정구간
        IEnumerator OnInit() {
            yield return null;

            string jsonPath = Application.streamingAssetsPath + Path.DirectorySeparatorChar + "rsconn.json";
            Debug.Log("jsonPath] : "+jsonPath);
            UnityWebRequest webRequest = UnityWebRequest.Get(jsonPath);
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                // Show results as text
                Debug.Log(webRequest.downloadHandler.text);
                string text = webRequest.downloadHandler.text;

                RsVO rsVO = JsonConvert.DeserializeObject<RsVO>(text);
                consoleProcessStart = new ConsoleProcessStart();
                consoleProcessStart.OnInit(rsVO,this);
                SetEvent();
                while (true) {
                  
                    yield return new WaitUntil(() => !string.IsNullOrEmpty(rsData));
                    yield return new WaitUntil(()=> rsData.Length>0);
                    rsEvent.Invoke(rsData);
                    rsData = "";
                }

            }
        }


        /// <summary>
        /// 시작시 RS232 초기화 데이터 전송
        /// </summary>
        public void OnResetData() {
            ResetOnResetDataCoroutine();
            onResetDataCoroutine=StartCoroutine(OnResetDataCoroutine(resetTime));
        }

        Coroutine onResetDataCoroutine = null;

        /// <summary>
        /// 리셋 코루틴
        /// </summary>
        /// <returns></returns>
        IEnumerator OnResetDataCoroutine(float resetTime) {
            yield return new WaitForSeconds(resetTime);
            //resetData
            if (!string.IsNullOrEmpty(resetData))
            {
                SendData(resetData);
            }
        }

        /// <summary>
        /// 리셋 데이터 코루틴 작동구간
        /// </summary>
        private void ResetOnResetDataCoroutine() {
            if (onResetDataCoroutine != null) { 
                StopCoroutine(onResetDataCoroutine);
                onResetDataCoroutine = null;
            }
        }

        /// <summary>
        /// 이벤트 연동 구간
        /// </summary>
        private void SetEvent()
        {
            consoleProcessStart.m_rsEvent.AddListener(ReturnData);
        }

        private string rsData = null;
        /// <summary>
        /// 리턴 데이터 정보 
        /// </summary>
        private void ReturnData(string data)
        {
            rsData = data;
        }

        /// <summary>
        /// 리턴테스트   
        /// </summary>
        /// <param name="data"></param>
        public void ReturnData2(string data)
        {
            Debug.Log(data);
        }

        //데이터 전송
        public void SendData(string message) {
            if (this.enabled){
                try
                {
                    Debug.Log("rs232] Message : "+message);
                    consoleProcessStart.SendConsole(message);
                }
                catch (Exception e) { 
                    Debug.Log(e.Message.ToString());
                }
            }
        }

        private void OnApplicationQuit()
        {
            consoleProcessStart.ProcessKill();
        }
    }
}
