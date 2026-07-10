using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//사운드 구성 스크립트
public class SoundControl : MonoBehaviour
{
    public AudioClip backgraundSound1;
    public AudioClip backgraundSound2;
    public AudioClip backgraundSound3;
    //오디오 소스
    public AudioSource audioSource;

    public enum SoundType { Sound1 , Sound2 , Sound3 }

    public void OnEnable()
    {
        audioSource.loop = true;
    }

    public void SoundStop() { 
        
    }

    //사운드 플레이(배경사운드)
    public void BackgraundSoundPlay(SoundType soundType) {
        switch (soundType) { 
            case SoundType.Sound1 :
                audioSource.clip = backgraundSound1;
                audioSource.Play();
                break;

            case SoundType.Sound2:
                audioSource.clip = backgraundSound2;
                audioSource.Play();
                break;

            case SoundType.Sound3:
                audioSource.clip = backgraundSound3;
                audioSource.Play();
                break;

        }  
    }


}
