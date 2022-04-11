using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem;

public class ShopSaleManager : MonoBehaviour
{
    [SerializeField]
    public InventoryChannel shopInventoryChannel;
    public InventoryHolder m_SaleInventoryHolder;
    //private InventorySystem.Inventory m_SaleInventory;
    private readonly List<InventorySlot> m_Slots = new List<InventorySlot>();

    private int saleTotal = 0;
    
    public void sumValues(InventoryHolder shopInventoryHolder)
    {
   /*     int saleTotal = 0;
    shopInventoryHolder.Inventory.ForEach(slot =>
    if (slot != null && slot.Item != null)
    {
        saleTotal += slot.Item.SaleValue * slot.Quantity;
        }
)*/
    }
    
    void Update()
    {
        
    }
}
