using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sc_scene_controls : MonoBehaviour
{
    public GameObject player_ref;
    private AsyncOperation sceneAsync;
    
    //stick this method onto a button
    void load_planet_scene()
    {
        AsyncOperation scene = SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
        scene.allowSceneActivation = false;
        sceneAsync = scene;

        finish();
    }

    //helper methods don't even worry about it
    void enable_scene()
    {
        sceneAsync.allowSceneActivation = true;

        Scene sceneToLoad = SceneManager.GetSceneByBuildIndex(2);
        if(sceneToLoad.IsValid())
        {
            SceneManager.MoveGameObjectToScene(player_ref, sceneToLoad);
            SceneManager.SetActiveScene(sceneToLoad);
        }
    }

    //I said don't worry about it
    void finish()
    {
        enable_scene();
    }
}
