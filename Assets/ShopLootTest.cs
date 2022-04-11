using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopLootTest : MonoBehaviour
{
    public bool inventoryToggle = false;
    [SerializeField]
    private InventoryChannel InventoryChannel;
    [SerializeField]
    private ShopUIChannel ShopUIChannel;
    [SerializeField]
    private InventoryHolder inventoryHolder;
    [SerializeField]
    private InventorySystem.InventoryItem itemToGet;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
       // inventoryToggle = !inventoryToggle;
        
        if(Input.GetKeyDown(KeyCode.B))
        {
            InventoryChannel?.RaiseLootItem(itemToGet);
        }
    }
}
