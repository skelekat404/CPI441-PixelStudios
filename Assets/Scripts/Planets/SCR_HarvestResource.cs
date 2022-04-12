using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_HarvestResource : MonoBehaviour
{
    private bool canHarvest;
    public int currentHits = 0;
    public int maxHits = 3;

    // *** Identifies which inventory looted items go to ***
    [SerializeField]
    private InventoryChannel m_InventoryChannel;
    // *** Identifies which item is to be looted ***
    [SerializeField]
    private InventorySystem.InventoryItem m_LootableItem;

    // *** Used to reference variables file with material counts ***
    public SCR_ImportantVariables importantVariables;

    // *** Used to access HealthBar script ***
    public SCR_HealthBar healthBar;
    public float health = 1f;

    void Awake()
    {
        importantVariables = GameObject.FindGameObjectWithTag("Player").GetComponent<SCR_ImportantVariables>();

        healthBar = gameObject.GetComponentInChildren<SCR_HealthBar>();
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

        health -= 0.33f;
        healthBar.SetSize(health);

        if (health <= 0.67f)
        {
            healthBar.SetColor(Color.yellow);
        }
        if (health <= 0.34f)
        {
            healthBar.SetColor(Color.red);
        }

        if (currentHits >= maxHits)
        {
            //Destroy(this.gameObject);

            // *** Materials ***

            // Earth
            if (gameObject.tag == "Tree")
            {
                importantVariables.numWood++;
                Destroy(this.gameObject);
                Debug.Log("Wood #: " + importantVariables.numWood);
                m_InventoryChannel?.RaiseLootItem(m_LootableItem);
            }
            if (gameObject.tag == "Rock")
            {
                importantVariables.numRock++;
                Destroy(this.gameObject);
                Debug.Log("Rock #: " + importantVariables.numRock);
                m_InventoryChannel?.RaiseLootItem(m_LootableItem);
            }

            // Marus
            if (gameObject.tag == "Coal")
            {
                importantVariables.numCoal++;
                Destroy(this.gameObject);
                Debug.Log("Coal #: " + importantVariables.numCoal);
                m_InventoryChannel?.RaiseLootItem(m_LootableItem);
            }
            if (gameObject.tag == "LavaCrystal")
            {
                importantVariables.numLavaCrystal++;
                Destroy(this.gameObject);
                Debug.Log("Lava Crystal #: " + importantVariables.numLavaCrystal);
                m_InventoryChannel?.RaiseLootItem(m_LootableItem);
            }

            // Vamia
            if (gameObject.tag == "PurpleCrystal")
            {
                importantVariables.numPurpleCrystal++;
                Destroy(this.gameObject);
                Debug.Log("Purple Crystal #: " + importantVariables.numPurpleCrystal);
                m_InventoryChannel?.RaiseLootItem(m_LootableItem);
            }
            if (gameObject.tag == "PurpleEssence")
            {
                importantVariables.numPurpleEssence++;
                Destroy(this.gameObject);
                Debug.Log("Purple Essence #: " + importantVariables.numPurpleEssence);
                m_InventoryChannel?.RaiseLootItem(m_LootableItem);
            }
        }
    }
}
