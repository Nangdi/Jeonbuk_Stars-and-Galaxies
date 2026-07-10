using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustControl : MonoBehaviour
{

    public ParticleSystem particleSystem;
    public bool isPlay = false;

    public void OnEnable()
    {
        OnStop();
        if (isPlay)
        {
            OnPlay();
        }
    }

    private void OnDisable()
    {
        isPlay = false;
    }

    //파티클 실행
    public void OnPlay() {
        particleSystem.Play();
    }

    //파티클 소멸
    public void OnStop() {
        particleSystem.Stop();
    }

}
