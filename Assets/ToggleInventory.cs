using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleInventory : MonoBehaviour
{
    public bool inventoryToggle = false;
    [SerializeField]
    private InventoryChannel InventoryChannel;
    [SerializeField]
    private InventoryUIChannel InventoryUIChannel;
    [SerializeField]
    private InventoryHolder inventoryHolder;
    [SerializeField]
    private InventorySystem.InventoryItem itemToGet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            inventoryToggle = !inventoryToggle;
            InventoryUIChannel.RaiseToggle(inventoryHolder);
        }
        if(Input.GetKeyDown(KeyCode.B)) //Press B to generate beans -- meant as a test for "button press to get items"
        {
            InventoryChannel?.RaiseLootItem(itemToGet);
        }
    }
}
