using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
public class ScreenCount : MonoBehaviour
{

    public void Awake()
    {
        DebugManager.instance.enableRuntimeUI = false;
    }
}
