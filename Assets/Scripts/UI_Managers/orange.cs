using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orange : MonoBehaviour
{
    private GameObject _target;

    // Update is called once per frame
    void Update()
    {
        if (_target == null)
        {
            if (GameObject.FindGameObjectWithTag("GameController") != null)
            {
                _target = GameObject.FindGameObjectWithTag("orangeBandaid");
            }
        }
        else
        {
            _target.SetActive(false);
        }
    }
}
