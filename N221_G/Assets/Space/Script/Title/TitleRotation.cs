using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleRotation : MonoBehaviour
{
    public RectTransform thisRt;

    // Update is called once per frame
    void Update()
    {

        Vector3 re = thisRt.eulerAngles;
        if (re.z >= 360) {
            re.z = 0;
        }
        re.z += 0.1f;
        thisRt.eulerAngles = re;

    }
}
