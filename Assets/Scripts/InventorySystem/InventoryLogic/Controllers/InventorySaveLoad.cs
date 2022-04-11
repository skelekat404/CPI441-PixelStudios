using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using InventorySystem;

[RequireComponent(typeof(InventoryHolder))] // needed for serialization
public class InventorySaveLoad : MonoBehaviour
{
    public InventoryChannel inventoryChannel;
    [SerializeField] public InventoryHolder m_InventoryHolder;

    readonly string saveName = "SavedInventoryData.json";

    private void Awake()
    {
        m_InventoryHolder = GetComponent<InventoryHolder>();
        inventoryChannel.OnInventoryExport += OnInventoryExport;
        inventoryChannel.OnInventoryImport += OnInventoryImport;
    }

    public void OnInventoryExport()
    {
        string jsonData = m_InventoryHolder.Inventory.SerializeInventoryAsJSON();
        string fullPath = Application.dataPath + saveName;
        File.WriteAllText(fullPath, jsonData);
    }

    public void OnInventoryImport()
    {
        string fullPath = Application.dataPath + saveName;
        Debug.Log("we're in the import method");
        //if file exists
        if(File.Exists(fullPath))
        {
            Debug.Log("we're in the if statement");
            string jsonData = File.ReadAllText(fullPath);
            Debug.Log("we set jsondata up");
            Debug.Log("we're calling inventorychannel.clear next. right now inventory channel is " + inventoryChannel);
            inventoryChannel.RaiseInventoryClear();

            foreach(var item in m_InventoryHolder.Inventory.GetSlotsFromJSON(jsonData))
            {
                if(item.itemName != null && item.itemName != "")
                {
                    if(File.Exists($"{Application.dataPath}/Resources/ScriptableObjects/{item.itemName}.asset"))
                    {
                        InventoryItem invItem = Resources.Load<InventoryItem>("ScriptableObjects/" + item.itemName);
                        inventoryChannel.OnInventoryItemLoot(invItem, item.quantity);
                    }
                    else
                    {
                        Debug.LogWarning($"item: {item.itemName} cannot be found in resources folder. Please ensure you store all InventoryItem objects in resources!");
                    }
                }
            }
        }
    }
}
