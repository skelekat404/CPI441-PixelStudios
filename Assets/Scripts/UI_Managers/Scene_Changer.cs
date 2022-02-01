using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


using UnityEngine.SceneManagement;

public class Scene_Changer : MonoBehaviour
{
    public void LoadScene(string scene_id)
    {
        SceneManager.LoadScene(scene_id);
    }

}
