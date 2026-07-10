using RenderHeads.Media.AVProVideo;
using RenderHeads.Media.AVProVideo.Demos;
using UnityEngine;

/// <summary>
/// 사용자 컨트롤러
/// </summary>
public class UserControl : MonoBehaviour
{
    //영상연동
    public MediaPlayer mediaPlayer1;
    //public MediaPlayer mediaPlayer2;


    public void Stage1_OnPlay() {
       // mediaPlayer2.Control.Seek(0);
       // mediaPlayer2.Stop();
        mediaPlayer1.Control.Seek(0);
        mediaPlayer1.Play();
    }
    public void Stage2_OnPlay()
    {
        mediaPlayer1.Control.Seek(0);
        mediaPlayer1.Stop();
       // mediaPlayer2.Control.Seek(0);
       // mediaPlayer2.Play();
    }


}

