using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Transform bar = transform.Find("HealthBar_Bar");
        bar.localScale = new Vector3(.4f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
