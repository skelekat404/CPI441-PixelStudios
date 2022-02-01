using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class Sc_Orbit_Axis : NetworkBehaviour
{
    float degrees;
    // Start is called before the first frame update
    void Start()
    {
        degrees = 4;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.eulerAngles += Vector3.forward * degrees * Time.deltaTime;
    }
}
