using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class SCR_Projectile : MonoBehaviour
{
    // Projectile Stuff
    public float projectileSpeed = 5f;
    Rigidbody2D rigidbody;
    Vector2 projectileDirection;

    // Player Stuff
    private GameObject[] player_refs;
    private GameObject player_ref;
    private GameObject playerChild;

    //private GameObject projectileTarget;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        player_refs = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < player_refs.Length; i++)
        {
            if (player_refs[i].GetComponent<NetworkBehaviour>().IsLocalPlayer)
            {
                player_ref = player_refs[i];
            }
        }

        playerChild = player_ref.transform.GetChild(0).gameObject;

        projectileDirection = (playerChild.transform.position - transform.position).normalized * projectileSpeed;
        rigidbody.velocity = new Vector2(projectileDirection.x, projectileDirection.y);
        

        Destroy(gameObject, 4f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Ship"))
        {
            Debug.Log("Projectile Hit!");
            Destroy(gameObject);
        }
    }
}
