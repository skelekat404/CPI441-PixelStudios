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

    public delegate void ValueUpdateCallback(int valueTotal); //declare!
    public ValueUpdateCallback OnValueUpdate; //create event variable

    public int saleTotal = 0;

    public void sumValues(InventoryHolder shopInventoryHolder)
    {
        saleTotal = 0; // reset the value to get accurate count
        shopInventoryHolder = m_SaleInventoryHolder;
        shopInventoryHolder.Inventory.ForEach(slot =>
        {
            if (slot != null)
            {
                if(slot.Item != null)
                {
                    Debug.Log("there's an item in a slot, it is: " + slot.Item.ToString() + "with a quantity: " + slot.Quantity + " and value: " + slot.Item.SaleValue);
                    saleTotal += (slot.Item.SaleValue * (int)slot.Quantity);
                    if(OnValueUpdate != null)
                    {
                        Debug.Log("update event not null");
                        RaiseValueUpdate(saleTotal);
                        Debug.Log("event called");
                    }
                }
                else
                {
                    //Debug.Log("null slot: " + slot);
                }
            }
            else
            {
               Debug.Log("null slots exist");
            }  
        });
        //return saleTotal;
    }

    public void clearSlotsAfterSale(InventoryHolder shopInventoryHolder)
    {
        shopInventoryHolder = m_SaleInventoryHolder;
        shopInventoryHolder.Inventory.ForEach(slot =>
        {
            slot.Clear();
        });
    }

    public void RaiseValueUpdate(int updatedSaleValue)
    {
        OnValueUpdate?.Invoke(updatedSaleValue);
    }
}
