using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MLAPI;

public class Scene_Manager : NetworkBehaviour
{
    //public delegate void sweeper();
    //public static event sweeper sceneChanged;

    private GameObject[] player_refs;
    private GameObject player_ref;

    private GameObject multMenu;
    private GameObject miniMenu;
    private GameObject pauseMenu;

    private Transform[] parts;

    private AsyncOperation sceneAsync;
    private string prevPlanet;
    private bool localOnPlanet = false;

    //public void Awake()
    //{
    //    //look for local player
    //    player_refs = GameObject.FindGameObjectsWithTag("Player");
    //    for (int i = 0; i < player_refs.Length; i++)
    //    {
    //        if (player_refs[i].GetComponent<NetworkBehaviour>().IsLocalPlayer)
    //        {
    //            player_ref = player_refs[i];
    //        }
    //    }
    //}

    public void startLoadScene()
    {
        Debug.Log("Started Load Scene...");

        //look for local player
        player_refs = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < player_refs.Length; i++)
        {
            if (player_refs[i].GetComponent<NetworkBehaviour>().IsLocalPlayer)
            {
                player_ref = player_refs[i];
            }
        }

        //get scene name
        string planetToLoad = "";
        if (player_ref.GetComponentInChildren<Sc_Ship_Move>().onPlanet == false)
            planetToLoad = player_ref.transform.Find("Ship").GetComponent<player_last_collision>().get_last_planet_collide();
        else
            planetToLoad = "SampleScene";
        
        //load scene
        StartCoroutine(loadScene(planetToLoad));
    }

    IEnumerator loadScene(string in_name)
    {
        //if the scene is not loaded already
        if(!SceneManager.GetSceneByName(in_name).isLoaded)
        {
            //load scene async
            AsyncOperation scene = SceneManager.LoadSceneAsync(in_name, LoadSceneMode.Additive);
            //scene.allowSceneActivation = false;
            sceneAsync = scene;

            //wait until scene is completely finished loading
            while (scene.progress < 0.999f)
            {
                Debug.Log("Loading scene " + " [][] Progress: " + scene.progress);
                yield return null;
            }
        }
        
        

        //only reset the player position if you're coming from space
        //if (!player_ref.GetComponentInChildren<Sc_Ship_Move>().onPlanet)
        //{
        //reset player position
        parts = player_ref.GetComponentsInChildren<Transform>();//was local
        //foreach (Transform t in parts)
        //{
        //    t.position = Vector3.zero;
        //    t.rotation = Quaternion.identity;
        //}
        //}
        //finish
        OnFinishedLoadingScene(in_name);
    }

    //it's okay just go with it
    void OnFinishedLoadingScene(string lname)
    {
        enableScene(lname);
    }

    //finalize
    void enableScene(string load_name)
    {
        Debug.Log("allowing scene activation...");

        //get the last collided planet from the player_last_collision script which is updated in the PlanetCollision script

        bool unload_boi = false;
        bool testEarthUnload = false;
        Scene sceneToLoad = SceneManager.GetSceneByName(load_name);
        //error check + move player to scene
        Debug.Log(sceneToLoad.IsValid());
        if(sceneToLoad.IsValid())
        {
            Scene scene_unload = gameObject.scene;
            //int num = 0;
            //for (int i = 0; i < player_refs.Length; i++)
            //{
                
            //    //if there is a player on earth
            //    if(player_refs[i].transform.position.x <= 30 && player_refs[i].transform.position.x >= 0 && player_refs[i].transform.position.y <= 30 && player_refs[i].transform.position.y >= 0)
            //    {
            //        num++;
            //        //testEarthUnload = false;
            //    }
            //}
            //if(num > 0)
            //{
            //    testEarthUnload = false;
            //}
            //else
            //{
            //    testEarthUnload = true;
            //}
            if(player_ref.GetComponentInChildren<Sc_Ship_Move>().onPlanet)
            {
                unload_boi = true;
            }
            else
            {
                unload_boi = false;
            }
            


            
            Debug.Log("scene is valid, loading...");

            //i dont trust unity
            player_refs = GameObject.FindGameObjectsWithTag("Player");
            for (int i = 0; i < player_refs.Length; i++)
            {
                SceneManager.MoveGameObjectToScene(player_refs[i], sceneToLoad);
                //    if (player_refs[i].GetComponent<NetworkBehaviour>().IsLocalPlayer)
                //    {
                //player_ref = player_refs[i];
                //    }
            }
            //***

            //SceneManager.MoveGameObjectToScene(player_ref, sceneToLoad);
            //

            //IF TWO SCENE MANAGERS GET CREATED SOMEHOW, UNCOMMENT THIS IF
            //ELSE, KEEP IT COMMENTED TO ALLOW THE GAMECONTROLLER TO BE MOVED BETWEEN SCENES
            //if(!player_ref.GetComponentInChildren<Sc_Ship_Move>().onPlanet)//trying this
            //{
            SceneManager.MoveGameObjectToScene(GameObject.FindGameObjectWithTag("GameController"), sceneToLoad);

            //}

            //look for player instances
            //Debug.Log("\n***************\n");
            //Debug.Log(GameObject.FindGameObjectsWithTag("Player").Length);
            //Debug.Log("\n***************\n");

            if (unload_boi)//&& testEarthUnload
            {
                Debug.Log("Scene unloading...\n");
                SceneManager.UnloadSceneAsync(scene_unload);
            }

            //***call event
            //sceneChanged();
            //***call event

            //
            //WILL NOT BE UNLOADING A SCENE
            //string scene_unload = "";
            //*****************************
            if (player_ref.scene.name.ToLower().Contains("planet"))
            {
                if(player_ref.scene.name.ToLower().Contains("earth"))
                {
                    prevPlanet = "earth";
                    //change player position to on planet position
                    foreach (Transform t in parts)
                    {
                        t.position = new Vector3(0, 0, 0);
                        t.rotation = Quaternion.identity;
                    }
                }
                else if(player_ref.scene.name.ToLower().Contains("orange"))
                {
                    prevPlanet = "orange";
                    //change player position to on planet position
                    foreach (Transform t in parts)
                    {
                        t.position = new Vector3(100, 100, 0);
                        t.rotation = Quaternion.identity;
                    }
                }
                else if (player_ref.scene.name.ToLower().Contains("vamia"))
                {
                    prevPlanet = "vamia";
                    //change player position to on planet position
                    foreach (Transform t in parts)
                    {
                        t.position = new Vector3(200, 200, 0);
                        t.rotation = Quaternion.identity;
                    }
                }


                //WILL NOT BE UNLOADING A SCENE
                //scene_unload = "SampleScene";
                //*****************************
                player_ref.GetComponentInChildren<Sc_Ship_Move>().onPlanet = true;
                localOnPlanet = true;
                //move pause_menu since it has the scene manager in it
                //SceneManager.MoveGameObjectToScene(GameObject.FindGameObjectWithTag("GameController"), sceneToLoad);
                //GameObject.FindGameObjectWithTag("GameController")
            }
            else
            {
                //find x y position of previous planet to spawn player

                float xCoord = 0;
                float yCoord = -1000;
                GameObject[] planets = GameObject.FindGameObjectsWithTag("Planet");

                if (prevPlanet.Equals(""))
                {
                    //do nothing
                }
                else if(prevPlanet.Equals("earth"))
                {

                    for(int i = 0; i < planets.Length; i++)
                    {
                        if(planets[i].name.Equals("Planet_Earth"))
                        {
                            xCoord = planets[i].transform.position.x;
                            yCoord = planets[i].transform.position.y;
                        }
                    }

                }
                else if (prevPlanet.Equals("orange"))
                {
                    for (int i = 0; i < planets.Length; i++)
                    {
                        if (planets[i].name.Equals("Planet_Orange"))
                        {
                            xCoord = planets[i].transform.position.x;
                            yCoord = planets[i].transform.position.y;
                        }
                    }
                }
                else if (prevPlanet.Equals("vamia"))
                {
                    for (int i = 0; i < planets.Length; i++)
                    {
                        if (planets[i].name.Equals("Planet_Purple"))
                        {
                            xCoord = planets[i].transform.position.x;
                            yCoord = planets[i].transform.position.y;
                        }
                    }
                }
                else if(prevPlanet.Equals("Shop_SpcaeShip"))
                {
                    for (int i = 0; i < planets.Length; i++)
                    {
                        if (planets[i].name.Equals("Shop_SpaceShip"))
                        {
                            xCoord = planets[i].transform.position.x;
                            yCoord = planets[i].transform.position.y;
                        }
                    }
                }

                //change player position to outer space
                foreach (Transform t in parts)
                {
                    t.position = new Vector3(xCoord, yCoord, 0);
                    t.rotation = Quaternion.identity;
                }

                //WILL NOT BE UNLOADING A SCENE
                //scene_unload = player_ref.transform.Find("Ship").GetComponent<player_last_collision>().get_last_planet_collide();
                //*****************************
                player_ref.GetComponentInChildren<Sc_Ship_Move>().onPlanet = false;
                localOnPlanet = false;

                //close pause menu
                pauseMenu = GameObject.FindGameObjectWithTag("pause");
                if(pauseMenu != null)
                {
                    pauseMenu.SetActive(false);
                }

                //deactivate leave button
                //GameObject leaveButton = GameObject.FindGameObjectWithTag("leaveButton");
                //leaveButton.SetActive(false);

            }
            //Debug.Log("SCENE TO LOAD: " + sceneToLoad);
            //Debug.Log("SCENE TO UNLOAD: " + scene_unload);

            SceneManager.SetActiveScene(sceneToLoad);

            //make this check through collision******************************************

            //WILL NOT BE UNLOADING A SCENE
            //SceneManager.UnloadSceneAsync(scene_unload);
            //*****************************

            Debug.Log("Scene loaded.");

            //manage ui

            //deactivate multiplayer menu
            multMenu = GameObject.FindGameObjectWithTag("mult");

            //if (multMenu.activeInHierarchy)
            //{
            //    Debug.Log("reached mult");
            if(multMenu != null)
                multMenu.SetActive(false);
            //}
            if(GameObject.Find("Camera"))
                GameObject.Find("Camera").SetActive(false);
            
            //if (!miniMenu.activeInHierarchy)
            //{
            //    Debug.Log("reached mini");
            //    miniMenu.SetActive(true);
            //}
           // Debug.Log(localOnPlanet);
           // if(localOnPlanet == false)
            //{
                //restore player's previous position
                //player_ref.transform.position = player_ref.transform.Find("Ship").GetComponent<player_last_collision>().get_last_player_pos().position;
                //player_ref.transform.rotation = player_ref.transform.Find("Ship").GetComponent<player_last_collision>().get_last_player_pos().rotation;
                //***
            //}
        }//if scene to load is valid
    }//enable scene

    private void Update()
    {
        //escape button functions as a 'leave planet' button
        //miniMenu = GameObject.FindGameObjectWithTag("mini");
        if(player_ref != null)
        {
            if (player_ref.GetComponentInChildren<Sc_Ship_Move>().onPlanet)
            {
                if (player_ref.GetComponent<NetworkBehaviour>().IsLocalPlayer)
                {
                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        startLoadScene();
                    }
                    //miniMenu = GameObject.FindGameObjectWithTag("mini");
                    //if (player_ref.GetComponentInChildren<Sc_Ship_Move>().onPlanet)
                    //{
                    // miniMenu.SetActive(false);
                    //}

                }
            }
        }

    }

}//class scene manager
