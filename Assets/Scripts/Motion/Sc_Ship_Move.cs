using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class Sc_Ship_Move : NetworkBehaviour
{
    private float active_speed;//was public 
    private float passive_speed;//was public 
    private float degrees;//was public 
    float curr_passive_speed;
    //Vector3 forward2D = new Vector3(1, 0, 0);
    // Start is called before the first frame update

    //Sprint cooldown variables
    private float sprintSpeed;//was public 
    private float sprintLength;//was public 
    private float sprintCoolDown;//was public 

    private float sprintCounter;
    private float sprintCoolCounter;

    private float active_speed_high;
    private float active_speed_low;
    public bool onPlanet;

    private Animator animator;

    void Awake()
    {
        sprintSpeed = 2f;
        sprintLength = 0.5f;
        sprintCoolDown = 120f;
        active_speed = 0.009f;
        active_speed_high = 0.04f;
        active_speed_low = 0.009f;
        passive_speed = 0.001f;
        degrees = 400;
        onPlanet = false;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
        if (IsLocalPlayer)
        {
            //update speeds
            //when we have animated player and ship, this 'if' will also
            //update the sprite of the player object
            if (onPlanet)
            {
                sprintSpeed = 2f;
                sprintLength = 0.1f;
                sprintCoolDown = 3f;
                active_speed_high = 0.009f;
                active_speed_low = 0.005f;
                passive_speed = 0f;
            }
            else
            {
                sprintSpeed = 2f;
                sprintLength = 0.5f;
                sprintCoolDown = 120f;
                active_speed_high = 0.04f;
                active_speed_low = 0.009f;
                passive_speed = 0.001f;
            }

            //move
        
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
            else if((Input.GetKey("up") || Input.GetKey(KeyCode.W)) && Input.GetKey(KeyCode.LeftShift))
            {
                if(sprintCoolCounter <= 0 && sprintCounter <= 0)
                {
                    //Update this variable for faster/slower accelerate speed
                    animator.SetBool("Accelerate", true);
                    active_speed = active_speed_high;
                    gameObject.transform.position += gameObject.transform.right * active_speed;
                    curr_passive_speed = passive_speed;

                    sprintCounter = sprintLength;
                }
                
            }

            if(sprintCounter > 0)
            {
                sprintCounter -= Time.deltaTime;
                if(sprintCounter <= 0)
                {
                    //Reset speed back to default
                    animator.SetBool("Accelerate", false);
                    active_speed = active_speed_low;
                    sprintCoolCounter = sprintCoolDown;
                }
            }

            if(sprintCoolCounter > 0)
            {
                sprintCoolCounter -= Time.deltaTime;
            }

            //apply passive speed
            gameObject.transform.position += gameObject.transform.right * curr_passive_speed;
        }

        
    }
}
