using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_PersistingInfoManager : MonoBehaviour
{
    static SCR_PersistingInfoManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
