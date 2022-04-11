using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem;

public class ShopSaleManager : MonoBehaviour
{
    [SerializeField]
    private InventorySlotUIController shopUISlotController;
    
    public InventoryChannel shopInventoryChannel;
    private int saleTotal = 0;
    private InventorySlot slot;

    public Action<InventorySystem.InventorySlot> itemAdded;
    
    public void Start()
    {
    }

    private void OnItemsUpdated(InventorySystem.InventorySlot slot)
    {

    }
    
    public void sumValues(InventoryHolder shopInventoryHolder)
    {
        /*
        shopInventoryHolder.Inventory.ForEach(slot =>
        {
            saleTotal += slot.Item.SaleValue++;
        });*/
        /*Array.ForEach(GetComponentsInChildren<InventorySlotUIController>(shopUISlotController), slot =>
            if (slot != null && slot.Item != null)
            {
                saleTotal
            }*/
    }
    
    void Update()
    {
        
    }
}
