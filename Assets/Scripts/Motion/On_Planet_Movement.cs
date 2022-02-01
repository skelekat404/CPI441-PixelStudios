using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class On_Planet_Movement : MonoBehaviour
{
    public float active_speed;
    public float passive_speed;
    public float degrees;
    float curr_passive_speed;
    //Vector3 forward2D = new Vector3(1, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        //active_speed = 5;
        //passive_speed = 1;
        //curr_passive_speed = 0;
        //degrees = 4;
    }

    // Update is called once per frame
    void Update()
    {
        
            //moving
            if (Input.GetKey("up") || Input.GetKey(KeyCode.W))
            {
                gameObject.transform.position += gameObject.transform.right * active_speed;
                curr_passive_speed = passive_speed;
            }
            else if (Input.GetKeyDown("down") || Input.GetKey(KeyCode.S))
            {
                curr_passive_speed = 0;
            }
            //turning
            if (Input.GetKey("left") || Input.GetKey(KeyCode.A))
            {
                gameObject.transform.eulerAngles += gameObject.transform.forward * degrees * Time.deltaTime;
            }
            else if (Input.GetKey("right") || Input.GetKey(KeyCode.D))
            {
                gameObject.transform.eulerAngles += gameObject.transform.forward * -degrees * Time.deltaTime;
            }
            else if ((Input.GetKey("up") || Input.GetKey(KeyCode.W)) && Input.GetKey(KeyCode.LeftShift))
            {
                gameObject.transform.position += gameObject.transform.right * active_speed * 1.1f;
                curr_passive_speed = passive_speed;
            }

            //apply passive speed
            gameObject.transform.position += gameObject.transform.right * curr_passive_speed;
        


    }
}
