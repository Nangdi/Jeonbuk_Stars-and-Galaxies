using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndControl : MonoBehaviour
{
    //Å¸°Ù Äµ¹ö½º
    public Canvas targetCanvas;

    public void OnEnable()
    {
        targetCanvas.gameObject.SetActive(true);
        
    }

    public void OnDisable()
    {
        targetCanvas.gameObject.SetActive(false);
    }


}
