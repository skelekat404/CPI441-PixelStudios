using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shop Item", menuName = "Shop System/Items/Default")]
public class shop_item_object : ScriptableObject
{
    public string title;
    public string description;
    public int base_price;

     // *** Sets which game item goes into inventory on purchase (shop) ***
    public InventorySystem.InventoryItem m_LootableItem;
}
