using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MLAPI;

public class Scene_Manager : MonoBehaviour
{
    private GameObject[] player_refs;
    private GameObject player_ref;

    private GameObject multMenu;
    private GameObject miniMenu;

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
        //Debug.Log(planetToLoad);

        //store last player position
        //if (player_ref.GetComponentInChildren<Sc_Ship_Move>().onPlanet == false)
        //{
            //Debug.Log("Reached");
            //player_ref.transform.Find("Ship").GetComponent<player_last_collision>().set_last_player_pos(player_ref.transform.Find("Ship").transform);
        //}
        //Debug.Log("onPlanet: " + player_ref.GetComponentInChildren<Sc_Ship_Move>().onPlanet);
        //Debug.Log(player_ref.transform.Find("Ship").GetComponent<player_last_collision>().get_last_player_pos().position);

        StartCoroutine(loadScene(planetToLoad));
    }

    IEnumerator loadScene(string in_name)
    {
        //load scene async
        AsyncOperation scene = SceneManager.LoadSceneAsync(in_name, LoadSceneMode.Additive);
        //scene.allowSceneActivation = false;
        sceneAsync = scene;

        

        //wait until scene is completely finished loading
        while(scene.progress < 0.999f)
        {
            Debug.Log("Loading scene " + " [][] Progress: " + scene.progress);
            yield return null;
        }

        //reset player position
        Transform[] parts = player_ref.GetComponentsInChildren<Transform>();
        foreach (Transform t in parts)
        {
            t.position = Vector3.zero;
        }

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

            string scene_unload = "";

            if (player_ref.scene.name.ToLower().Contains("planet"))
            {
                scene_unload = "SampleScene";
                player_ref.GetComponentInChildren<Sc_Ship_Move>().onPlanet = true;
                localOnPlanet = true;
                //move pause_menu since it has the scene manager in it
                SceneManager.MoveGameObjectToScene(GameObject.FindGameObjectWithTag("GameController"), sceneToLoad);
                //GameObject.FindGameObjectWithTag("GameController")
            }
            else
            {
                scene_unload = player_ref.transform.Find("Ship").GetComponent<player_last_collision>().get_last_planet_collide();
                player_ref.GetComponentInChildren<Sc_Ship_Move>().onPlanet = false;
                localOnPlanet = false;
                

            }
            //Debug.Log("SCENE TO LOAD: " + sceneToLoad);
            //Debug.Log("SCENE TO UNLOAD: " + scene_unload);

            SceneManager.SetActiveScene(sceneToLoad);
            
            //make this check through collision******************************************
            SceneManager.UnloadSceneAsync(scene_unload);
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
