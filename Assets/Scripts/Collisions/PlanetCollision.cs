using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class PlanetCollision : NetworkBehaviour
{
    public Rigidbody2D planet;

    public GameObject planetOneText;

    private bool planetOneBool;
    void Awake()
    {
        planet = GetComponent<Rigidbody2D>();

        planetOneText.SetActive(false);
        planetOneBool = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.root.tag.Equals("Player") && collision.gameObject.GetComponent<NetworkBehaviour>().IsLocalPlayer)
        {
            //get the scene name reference from the planet the player is colliding with and store it in the player
            //collision.gameObject.GetComponent<player_last_collision>().set_last_planet_collide("hello");
            //string boi = gameObject.GetComponent<scene_name>().scene_name_ref;
            //Debug.Log(boi);
            collision.gameObject.GetComponent<player_last_collision>().set_last_planet_collide(gameObject.GetComponent<scene_name>().scene_name_ref);
            
            //***
            planetOneText.SetActive(true);
            planetOneBool = true;
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.root.tag.Equals("Player") && collision.gameObject.GetComponent<NetworkBehaviour>().IsLocalPlayer)
        {
            planetOneText.SetActive(false);
            planetOneBool = false;
        }
        
    }
}
