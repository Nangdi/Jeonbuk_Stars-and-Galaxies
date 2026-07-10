using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRotionCenterText : MonoBehaviour
{

    [Header("타겟의 Transform")]
    public Transform targetTransform;
    public float speed = 0.01f;

    public Image imageSliderUI;

    public void OnEnable()
    {
        ResetSliderUI(); 
    }

    public void OnDisable()
    {
        ResetSliderUI();
    }

    //슬라이더 리셋
    private void ResetSliderUI() {
        if (imageSliderUI)
        {
            imageSliderUI.fillAmount = 0;
        }
    }

    public void Update()
    {

        if (targetTransform)
        {
            Vector3 angles = targetTransform.localEulerAngles;
            angles.z = angles.z + speed;
            targetTransform.localEulerAngles = angles;
        }
    }
}
