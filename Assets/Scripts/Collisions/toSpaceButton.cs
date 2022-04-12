using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toSpaceButton : MonoBehaviour
{
    private GameObject _pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_pauseMenu == null)
        {
           _pauseMenu = GameObject.FindGameObjectWithTag("GameController");

        }
    }

    public void backToSpace()
    {
        if (_pauseMenu != null)
        {
            _pauseMenu.GetComponent<Scene_Manager>().startLoadScene();
        }
    }
}
