using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class Camera_OnOff : NetworkBehaviour
{
    public Camera camera_ref;
    // Start is called before the first frame update
    void Awake()
    {
        cameraChecking();
    }

    // Update is called once per frame
    void Update()
    {
        cameraChecking();
    }

    void cameraChecking()
    {
        if (IsLocalPlayer)
        {
            camera_ref.enabled = true;
            return;
        }
        camera_ref.enabled = false;
    }
}
