using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class Sc_Orbit_Star : NetworkBehaviour
{
    float speed;
    public Transform star_pos;
    private Vector3 z_axis = Vector3.forward;
    // Start is called before the first frame update
    void Start()
    {
        speed = 0.003f;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.RotateAround(star_pos.position, z_axis, speed);
    }
}
