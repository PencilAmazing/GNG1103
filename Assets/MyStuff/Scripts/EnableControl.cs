using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableControl : MonoBehaviour
{
    [Tooltip("Camera to give control to at first")]
    public Camera StartCamera;
    [Tooltip("Camera to switch control")]
    public Camera ControlCamera;
    [Tooltip("Teleport area to enable/disable")]
    public Valve.VR.InteractionSystem.Teleport TeleportScriptRef;

    // Start is called before the first frame update
    void Start()
    {
        if(TeleportScriptRef == null)
        {
            Debug.Log("No teleport ref found");
            return;
        }

        StartCamera.enabled = true;
        ControlCamera.enabled = false;
        TeleportScriptRef.enabled = false;
    }

    public void SwitchAroundControl()
    {
        StartCamera.enabled = false;
        ControlCamera.enabled = true;
        TeleportScriptRef.enabled = true;
    }
}
