using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class used to spy on DontDestroyOnLoad and get all root game objects from inside it
public class DontDestroyOnLoadAccessor : MonoBehaviour
{
    private static DontDestroyOnLoadAccessor _instance;

    public static DontDestroyOnLoadAccessor Instance { get { return _instance; }}
 
    void Awake()
    {
        if (_instance != null) Destroy(this);
        this.gameObject.name = this.GetType().ToString();
        _instance = this;
        DontDestroyOnLoad(this);
    }
 
    public GameObject[] GetAllRootsOfDontDestroyOnLoad() {
        return this.gameObject.scene.GetRootGameObjects();
    }
}
 
// Example to access the dontdestroy-objects from anywhere
public class FindDontDestroyOnLoad : MonoBehaviour
{
    public GameObject[] rootsFromDontDestroyOnLoad;
    void Start()
    {
        rootsFromDontDestroyOnLoad = DontDestroyOnLoadAccessor.Instance.GetAllRootsOfDontDestroyOnLoad();
    }
}
