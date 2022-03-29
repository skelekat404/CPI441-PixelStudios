using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MLAPI;

public class sc_scene_unload : MonoBehaviour
{
    //bool sweep = false;
    // Start is called before the first frame update
    void Awake()
    {
        //SceneManager.activeSceneChanged += sweep;
    }
    private void Update()
    {
        //look for player instances
        if (GameObject.FindGameObjectsWithTag("Player").Length == 0)
        {
            Debug.Log("Scene unloading...\n");
            SceneManager.UnloadSceneAsync(gameObject.scene);
        }
        else
        {
            //Debug.Log("Players still in the scene\n");
        }
    }
    //void sweep(Scene current, Scene next)
    //{
        

    //}
}
