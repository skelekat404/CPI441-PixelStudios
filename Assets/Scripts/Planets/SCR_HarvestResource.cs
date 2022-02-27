using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_HarvestResource : MonoBehaviour
{
    private bool canHarvest;
    public int currentHits = 0;
    public int maxHits = 3;

    // *** Used to reference variables file with material counts ***
    public SCR_ImportantVariables importantVariables;

    public int numWood = 0;
    public int numRock = 0;

    void Awake()
    {
        importantVariables = GameObject.FindGameObjectWithTag("Player").GetComponent<SCR_ImportantVariables>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canHarvest && Input.GetKeyDown(KeyCode.F))
        {
            HarvestMaterial();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name.Equals("Ship"))
        {
            canHarvest = true;
            Debug.Log("Can harvest");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Ship"))
        {
            canHarvest = false;
            Debug.Log("Can NOT harvest");
        }
    }
    private void HarvestMaterial()
    {
        currentHits++;

        if (currentHits >= maxHits)
        {
            //Destroy(this.gameObject);

            if (gameObject.tag == "Tree")
            {
                importantVariables.numWood++;
                Destroy(this.gameObject);
                Debug.Log("Wood #: " + importantVariables.numWood);
            }
            if (gameObject.tag == "Rock")
            {
                importantVariables.numRock++;
                Destroy(this.gameObject);
                Debug.Log("Rock #: " + importantVariables.numRock);
            }
        }
    }
}
