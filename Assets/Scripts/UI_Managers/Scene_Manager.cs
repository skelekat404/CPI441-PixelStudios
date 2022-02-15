using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MLAPI;

public class Scene_Manager : MonoBehaviour
{
    private GameObject[] player_refs;
    private GameObject player_ref;
    private AsyncOperation sceneAsync;
    
    public void startLoadScene()
    {
        Debug.Log("Started Load Scene...");
        StartCoroutine(loadScene(1));
    }

    IEnumerator loadScene(int index)
    {
        //load scene async
        AsyncOperation scene = SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);
        //scene.allowSceneActivation = false;
        sceneAsync = scene;

        //look for local player
        player_refs = GameObject.FindGameObjectsWithTag("Player");
        for(int i = 0; i < player_refs.Length; i++)
        {
            if(player_refs[i].GetComponent<NetworkBehaviour>().IsLocalPlayer)
            {
                player_ref = player_refs[i];
            }
        }

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
        OnFinishedLoadingScene();
    }

    //it's okay just go with it
    void OnFinishedLoadingScene()
    {
        enableScene(1);
    }

    //finalize
    void enableScene(int index)
    {
        Debug.Log("allowing scene activation...");
        //sceneAsync.allowSceneActivation = true;
        
        Scene sceneToLoad = SceneManager.GetSceneByBuildIndex(1);

        //error check + move player to scene
        if(sceneToLoad.IsValid())
        {
            Debug.Log("scene is valid, loading...");
            SceneManager.MoveGameObjectToScene(player_ref, sceneToLoad);


            if (player_ref.scene.name.ToLower().Contains("planet"))
                player_ref.GetComponentInChildren<Sc_Ship_Move>().onPlanet = true;
            else
                player_ref.GetComponentInChildren<Sc_Ship_Move>().onPlanet = false;
            
            SceneManager.SetActiveScene(sceneToLoad);
            SceneManager.UnloadSceneAsync("SampleScene");
            Debug.Log("Scene loaded.");
        }
    }
}
