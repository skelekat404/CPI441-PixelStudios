using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class shop_sceneChange_Update : MonoBehaviour
{
    public GameObject _cam;
    public GameObject _can;
    // Start is called before the first frame update
    void Start()
    {
        //_cam = GetComponent<MainCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(SceneManager.GetActiveScene());
        if (SceneManager.GetActiveScene().name.Equals("ShopTestplanet"))
        {
            _cam.SetActive(true);
            _can.SetActive(true);
        }
        else
        {
            _cam.SetActive(false);
            _can.SetActive(false);
        }
    }
}
