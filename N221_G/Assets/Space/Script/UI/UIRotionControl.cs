using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRotionControl : MonoBehaviour
{

    public RectTransform thisRT;
    public float movepos = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = thisRT.transform.eulerAngles;
        pos.z = thisRT.transform.eulerAngles.z + movepos;
        if (pos.z >= 360)
        {
            pos.z = 0;

        }
        thisRT.transform.eulerAngles = pos;
        
    }
}
