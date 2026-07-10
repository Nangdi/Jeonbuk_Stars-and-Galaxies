using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemImpect : MonoBehaviour
{
    public GameObject destroyObject;
    public ParticleSystem hitImpectParticleSystem;

    //스탑 이벤트 핸들러 작동 되는지 확인 
    public void OnParticleSystemStopped()
    {
        Debug.Log("파티클 시스템 정지 [Stop] : 2");
        GameObject.Destroy(destroyObject);
    }
}
