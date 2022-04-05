using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopToggleInventory : MonoBehaviour
{
    public bool inventoryToggle = false;
    [SerializeField]
    private InventoryChannel InventoryChannel;
    [SerializeField]
    private ShopUIChannel ShopUIChannel;
    [SerializeField]
    private InventoryHolder inventoryHolder;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        inventoryToggle = !inventoryToggle;
        
        if(Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("key pressed");
            ShopUIChannel.OnToggleShopUI(inventoryHolder);
        }
    }
}
