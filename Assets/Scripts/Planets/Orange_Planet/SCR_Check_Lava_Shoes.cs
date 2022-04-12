using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Check_Lava_Shoes : MonoBehaviour
{
    private BoxCollider2D boxCollide;
    public bool localHasLavaWalk;

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
        //importantVariables.setLavaWalk(false); // *** NOTE *** Use setter to set variables to true once you buy the corresponding item in the shop
        localHasLavaWalk = importantVariables.getLavaWalk();
        Debug.Log("Value of localHasScuba: " + localHasLavaWalk);
    }

    // Update is called once per frame
    void Update()
    {
        if (localHasLavaWalk == false)
        {
            boxCollide.enabled = true;
        }
        else
        {
            boxCollide.enabled = false;
        }

        Debug.Log("Status of Box Collider: " + boxCollide.enabled);
        Debug.Log("Value of localHasScuba: " + localHasLavaWalk);
    }
}
