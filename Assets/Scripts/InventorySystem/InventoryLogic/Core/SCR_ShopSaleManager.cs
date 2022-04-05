using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem;

public class SCR_ShopSaleManager : MonoBehaviour
{
    public InventoryChannel shopInventoryChannel;
    public InventoryHolder shopInventoryHolder;
    public InventorySlot slot;
    private int saleTotal = 0;

    public Action<InventorySystem.InventorySlot> itemAdded;
    
    public void Start()
    {
    }

    private void OnItemsUpdated(InventorySystem.InventorySlot slot)
    {

    }
    
    public void sumValues()
    {
        shopInventoryHolder.Inventory.ForEach(slot =>
        {
            saleTotal += slot.Item.SaleValue++;
        });

        Debug.Log(saleTotal);
    }
    
    void Update()
    {
        
    }
}
