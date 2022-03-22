using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MLAPI;

public class Scene_Manager : NetworkBehaviour
{
    private GameObject[] player_refs;
    private GameObject player_ref;

    private GameObject multMenu;
    private GameObject miniMenu;

    private Transform[] parts;

    private AsyncOperation sceneAsync;
    private bool localOnPlanet = false;
    
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

        
        Scene sceneToLoad = SceneManager.GetSceneByName(load_name);
        //error check + move player to scene
        Debug.Log(sceneToLoad.IsValid());
        if(sceneToLoad.IsValid())
        {
            Debug.Log("scene is valid, loading...");
            SceneManager.MoveGameObjectToScene(player_ref, sceneToLoad);
            //
            SceneManager.MoveGameObjectToScene(GameObject.FindGameObjectWithTag("GameController"), sceneToLoad);

            //
            //WILL NOT BE UNLOADING A SCENE
            //string scene_unload = "";
            //*****************************
            if (player_ref.scene.name.ToLower().Contains("planet"))
            {
                //change player position to on planet position
                foreach (Transform t in parts)
                {
                    t.position = Vector3.zero;
                    t.rotation = Quaternion.identity;
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
                //change player position to outer space
                foreach (Transform t in parts)
                {
                    t.position = new Vector3(0, -1000, 0);
                    t.rotation = Quaternion.identity;
                }

                //WILL NOT BE UNLOADING A SCENE
                //scene_unload = player_ref.transform.Find("Ship").GetComponent<player_last_collision>().get_last_planet_collide();
                //*****************************
                player_ref.GetComponentInChildren<Sc_Ship_Move>().onPlanet = false;
                localOnPlanet = false;
                

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
            //miniMenu = GameObject.FindGameObjectWithTag("mini");

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
        }
    }
}
