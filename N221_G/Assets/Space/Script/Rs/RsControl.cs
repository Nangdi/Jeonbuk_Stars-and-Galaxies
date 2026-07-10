using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

public class RsControl : MonoBehaviour
{
    public MultiRS multiRS;
    public void Start()
    {
        SetEvent();
    }

    //이벤트 등록 
    private void SetEvent() {
        MultiRS.RsReceived += DataReceivedEvent;
    }


    /// <summary>
    /// 데이터 이벤트
    /// </summary>
    /// <param name="data"></param>
    private void DataReceivedEvent(string data)
    {
        try
        {
            RsDataVO rsDataVO = JsonConvert.DeserializeObject<RsDataVO>(data);
            Debug.Log(rsDataVO.Index + ":" + rsDataVO.Data);
            Command(rsDataVO.Data, rsDataVO.Index);
        }
        catch (Exception e) { }
    }

    private void Command(string data,int userData) {
        switch (data) {
            case "A1"://회전 데이터
                OnRotatingDevice(userData);
                break;
            case "B1"://공투입
                OnInsertBall(userData);
                break;
        }
    }

    //회전 데이터 정보
    private void OnRotatingDevice(int userIndex) {
        //회전데이터
        UserTableController gameObjectControl = GameObjectControl.Instance.userTableSettingList[userIndex];
        gameObjectControl.RotatingDevice();
    }

    //볼 입력
    private void OnInsertBall(int userIndex) {
        //볼입력
        UserTableController gameObjectControl = GameObjectControl.Instance.userTableSettingList[userIndex];
        gameObjectControl.InsertBall();

    }


    /* 1 -> 바람
   0 -> 바람 해제
   2 -> 입구막기*/

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) {
            multiRS.SendConsole(3,"1");
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            multiRS.SendConsole(3, "0");

        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            multiRS.SendConsole(3, "2");

        }
    }

    public void ReceivedData(string data) { 
        
    }



}
