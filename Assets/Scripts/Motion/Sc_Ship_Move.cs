using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class Sc_Ship_Move : NetworkBehaviour
{
    //public Animator animator2;
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

    //for on planet moving / animation state
    private float upFrames;
    private float downFrames;
    private float rightFrames;
    private float leftFrames;
    private float xVelocity;
    private float yVelocity;
    private int anim_state;

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

        //on planet moving
        xVelocity = 0.005f;
        yVelocity = 0.005f;
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
                gameObject.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
                animator.SetBool("on_planet", true);
                sprintSpeed = 2f;
                sprintLength = 0.1f;
                sprintCoolDown = 3f;
                active_speed_high = 0.009f;
                active_speed_low = 0.005f;
                passive_speed = 0f;
            }
            else
            {
                gameObject.transform.localScale = Vector3.one * 2;
                animator.SetBool("on_planet", false);
                sprintSpeed = 2f;
                sprintLength = 0.5f;
                sprintCoolDown = 120f;
                active_speed_high = 0.04f;
                active_speed_low = 0.009f;
                passive_speed = 0.001f;
            }
            //planet moving
            if (onPlanet)
            {
                //store current direction
                if(Input.GetKey("up") || Input.GetKey("w"))
                {
                    upFrames += 1;
                    gameObject.transform.position += Vector3.up * yVelocity;
                }
                else
                {
                    upFrames = 0;
                }
                if (Input.GetKey("down") || Input.GetKey("s"))
                {
                    downFrames += 1;
                    gameObject.transform.position += Vector3.down * yVelocity;
                }
                else
                {
                    downFrames = 0;
                }
                if (Input.GetKey("left") || Input.GetKey("a"))
                {
                    leftFrames += 1;
                    gameObject.transform.position += Vector3.left * xVelocity;
                }
                else
                {
                    leftFrames = 0;
                }
                if (Input.GetKey("right") || Input.GetKey("d"))
                {
                    rightFrames += 1;
                    gameObject.transform.position += Vector3.right * xVelocity;
                }
                else
                {
                    rightFrames = 0;
                }

                //if up is max
                if(upFrames > downFrames && upFrames > leftFrames && upFrames > rightFrames)
                {
                    
                    anim_state = 1;//up animation
                    animator.SetInteger("anim_state", anim_state);
                }
                //if down is max
                if (downFrames > upFrames && downFrames > leftFrames && downFrames > rightFrames)
                {
                    anim_state = 2;//down animation
                    animator.SetInteger("anim_state", anim_state);
                }
                //if left is max
                if (leftFrames > downFrames && leftFrames > upFrames && leftFrames > rightFrames)
                {
                    anim_state = 3;//left animation
                    animator.SetInteger("anim_state", anim_state);
                }
                //if right is max
                if (rightFrames > downFrames && rightFrames > leftFrames && rightFrames > upFrames)
                {
                    anim_state = 4;//right animation
                    animator.SetInteger("anim_state", anim_state);
                }

                if((downFrames - upFrames == 0) && (rightFrames - leftFrames == 0))
                {
                    anim_state = 0;
                    animator.SetInteger("anim_state", anim_state);
                }

                ////switch sprite/animation according to anim_state
                //if(anim_state == 0)
                //{

                //}
                //if (anim_state == 1)
                //{

                //}
                //if (anim_state == 2)
                //{

                //}
                //if (anim_state == 3)
                //{

                //}
                //if (anim_state == 4)
                //{

                //}

            }
            //move
            //space moving
            else
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
                    if (sprintCoolCounter <= 0 && sprintCounter <= 0)
                    {
                        //Update this variable for faster/slower accelerate speed
                        animator.SetBool("Accelerate", true);
                        active_speed = active_speed_high;
                        gameObject.transform.position += gameObject.transform.right * active_speed;
                        curr_passive_speed = passive_speed;

                        sprintCounter = sprintLength;
                    }

                }

                if (sprintCounter > 0)
                {
                    sprintCounter -= Time.deltaTime;
                    if (sprintCounter <= 0)
                    {
                        //Reset speed back to default
                        animator.SetBool("Accelerate", false);
                        active_speed = active_speed_low;
                        sprintCoolCounter = sprintCoolDown;
                    }
                }

                if (sprintCoolCounter > 0)
                {
                    sprintCoolCounter -= Time.deltaTime;
                }

                //apply passive speed
                gameObject.transform.position += gameObject.transform.right * curr_passive_speed;
            }
        }

        
    }
}
