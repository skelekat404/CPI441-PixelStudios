using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Default Object", menuName = "Inventory System/Items/Default")]
public class default_object : item_object
{
    public void awake()
    {
        type = item_type.Default;
    }

}
