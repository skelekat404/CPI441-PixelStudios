using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Resource Object", menuName = "Inventory System/Items/Resource")]
public class resource_object : item_object
{
    public void Awake()
    {
        type = item_type.Resource;
    }
}
