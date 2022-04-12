using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using TMPro;

public class SCR_Projectile : MonoBehaviour
{
    // Projectile Stuff
    public float projectileSpeed = 7f;
    Rigidbody2D projectileRB;
    Vector2 projectileDirection;

    // Player Stuff
    private GameObject[] player_refs;
    private GameObject player_ref;
    private GameObject playerChild;
    private GameObject playerChild2;


    public SCR_HealthBar healthBar;

    public SCR_ImportantVariables importantVariables;

    float healthPlayer;

    public Scene_Manager sceneManager;

    // Start is called before the first frame update
    private void Awake()
    {
        importantVariables = GameObject.FindGameObjectWithTag("Player").GetComponent<SCR_ImportantVariables>();

        sceneManager = GameObject.FindGameObjectWithTag("Player").GetComponent<Scene_Manager>();
    }
    void Start()
    {


        healthPlayer = importantVariables.getPlayerHealth();

        projectileRB = GetComponent<Rigidbody2D>();

        player_refs = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < player_refs.Length; i++)
        {
            if (player_refs[i].GetComponent<NetworkBehaviour>().IsLocalPlayer)
            {
                player_ref = player_refs[i];
            }
        }
        playerChild = player_ref.transform.GetChild(0).gameObject; // gets the ship object which is a child of the player
        playerChild2 = playerChild.transform.GetChild(0).gameObject;
        healthBar = playerChild2.GetComponentInChildren<SCR_HealthBar>();
        
        projectileDirection = (playerChild.transform.position - transform.position).normalized * projectileSpeed;
        projectileRB.velocity = new Vector2(projectileDirection.x, projectileDirection.y);
        

        Destroy(gameObject, 4f);
    }
    void Update()
    {
        if(healthPlayer <= 0.0f)
        {
            if (player_ref != null)
            {
                if (player_ref.GetComponentInChildren<Sc_Ship_Move>().onPlanet)
                {
                    if (player_ref.GetComponent<NetworkBehaviour>().IsLocalPlayer)
                    {

                        sceneManager.startLoadScene();

                        //miniMenu = GameObject.FindGameObjectWithTag("mini");
                        //if (player_ref.GetComponentInChildren<Sc_Ship_Move>().onPlanet)
                        //{
                        // miniMenu.SetActive(false);
                        //}

                    }
                }
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.LogError(importantVariables);
        //Debug.LogError(importantVariables.getPlayerHealth());
        //SCR_Projectile project = collision.GetComponent<SCR_Projectile>();
        if (collision.gameObject.name == "Ship")
        {
            
            
            //Debug.Log(healthPlayer);
           
            healthPlayer -= 0.1f;

            healthBar.SetSize(healthPlayer);

            if (healthPlayer <= 0.8f)
            {
                healthBar.SetColor(Color.yellow);
            }
            if (healthPlayer <= 0.3f)
            {
                healthBar.SetColor(Color.red);
            }

            importantVariables.setPlayerHealth(healthPlayer);
            //importantVariables.playerHealth -= 10;
            Destroy(gameObject);
            Debug.LogWarning("Current Health" + importantVariables.getPlayerHealth());
        }
    }
}
