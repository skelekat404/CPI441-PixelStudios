using System;
using System.Linq;
using UnityEngine;

public class ShopInventoryUIController : InventoryUIController
{
    protected override void Awake()
    {
        gameObject.SetActive(true);
    }
}
