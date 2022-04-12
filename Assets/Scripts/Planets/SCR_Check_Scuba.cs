using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Check_Scuba : MonoBehaviour
{
    private BoxCollider2D boxCollide;
    public bool localHasScuba;

    // *** Used to reference important variables script ***
    public SCR_ImportantVariables importantVariables;

    // Start is called before the first frame update
    void Awake()
    {
        boxCollide = gameObject.GetComponent<BoxCollider2D>();
        importantVariables = GameObject.FindGameObjectWithTag("Player").GetComponent<SCR_ImportantVariables>();
    }

    void Start()
    {
        //importantVariables.setScuba(false); // *** NOTE *** Use setter to set variables to true once you buy the corresponding item in the shop
        localHasScuba = importantVariables.getScuba();
        Debug.Log("Value of localHasScuba: " + localHasScuba);
    }

    // Update is called once per frame
    void Update()
    {
        if (localHasScuba == false)
        {
            boxCollide.enabled = true;
        }
        else
        {
            boxCollide.enabled = false;
        }

        Debug.Log("Status of Box Collider: " + boxCollide.enabled);
        Debug.Log("Value of localHasScuba: " + localHasScuba);
    }
}
