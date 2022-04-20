using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class Sc_Camera_Follow_Ship : MonoBehaviour
{
    public int camera_position_z;
    public GameObject shipRef;
    public GameObject playerRef;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shipRef == null)
        {
            shipRef = GameObject.FindGameObjectWithTag("Player");
        }
        else // potential error fix
        {
            //shipRef.transform.position.x, shipRef.transform.position.y, camera_position_z
            if (gameObject.tag == "MainCamera")
            {
                gameObject.transform.position = new Vector3(shipRef.transform.position.x, shipRef.transform.position.y, camera_position_z);
                if (shipRef.GetComponent<Sc_Ship_Move>().onPlanet)
                {
                    GetComponent<Camera>().orthographicSize = 4;
                }
                else
                {
                    GetComponent<Camera>().orthographicSize = 20;
                }
            }
            else
            {
                gameObject.transform.position = new Vector3(shipRef.transform.position.x, shipRef.transform.position.y, camera_position_z);
            }
            //else
            //{
            //    if(shipRef.GetComponent<Sc_Ship_Move>().onPlanet)
            //    {
            //        gameObject.transform.position = new Vector3(0, 0, camera_position_z);
            //    }
            //    else
            //    {
            //        gameObject.transform.position = new Vector3(0, -1000, camera_position_z);
            //    }
                
            //}
        }
    }

    
}
