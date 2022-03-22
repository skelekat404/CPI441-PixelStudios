using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class update_hudCleaner : MonoBehaviour
{
    public GameObject _cleaner;
    public string scene_name;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name.Equals(scene_name))
        {
            _cleaner.SetActive(true);
            _cleaner.SetActive(true);
        }
        else
        {
            _cleaner.SetActive(false);
            _cleaner.SetActive(false);
        }
    }
}
