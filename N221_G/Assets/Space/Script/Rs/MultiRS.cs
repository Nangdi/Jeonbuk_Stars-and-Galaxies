using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 멀티 RS232 연동 클래스 
/// </summary>
public class MultiRS : OutsideProcessControl
{

    public override void OnInit() { 
    
    }

    //프로세스 로드 완료 이벤트 핸들러
    public void OnLoadComplete() { 
        
    }

    //ReceiverData
    public override void ReceiverData(string data)
    {
        base.ReceiverData(data);
        Debug.Log("ReceiverData} :"  +data);
        /* Debug.Log(data);
         RsDataVO rsDataVO = JsonConvert.DeserializeObject<RsDataVO>(data);
         Debug.Log(rsDataVO.Index+":"+ rsDataVO.Data);*/


    }

}
