using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory System/Items/Equipment")]
public class equipment_object : item_object
{
    public void Awake()
    {
        type = item_type.Equipment;
    }
}
