using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStarfield : MonoBehaviour
{

    private bool isCheck = false;
    public float speed = 0.01f;
    // Update is called once per frame
    void Update()
    {
        Vector3 movez = this.transform.position;
        if (movez.z > 100000000)
        {
            isCheck = true;
        }
        else if (movez.z < 0) {
            isCheck = false;
        }
        if (isCheck)
        {
            movez.z -= speed;
        }
        else {
            movez.z += speed;
        }
            
        this.transform.position = movez;
    }
}
