using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class ProjectLoad : MonoBehaviour
{
    public Camera camera1;
    public Camera camera2;

    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(OnCameraLoad());
    }
    IEnumerator OnCameraLoad()
    {
        yield return new WaitForSeconds(2f);
        Camera[] Cameras=this.GetComponentsInChildren<Camera>();
        for (int i=0; i< Cameras.Length; i++) {
            Cameras[i].cullingMask= ~(1 << LayerMask.NameToLayer("Starfield"));
        }

        //camera1.cullingMask = ~(1 << LayerMask.NameToLayer("Starfield"));
       // camera2.cullingMask = ~(1 << LayerMask.NameToLayer("Starfield"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
