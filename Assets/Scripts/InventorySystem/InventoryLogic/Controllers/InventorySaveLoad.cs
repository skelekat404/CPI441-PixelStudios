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
        Debug.Log("data serialized");
        string fullPath = Application.dataPath + saveName;
        Debug.Log("file name assigned");
        File.WriteAllText(fullPath, jsonData);
        Debug.Log(m_InventoryHolder + " data serialized to file: " + fullPath);
    }

    public void OnInventoryImport()
    {
        string fullPath = Application.dataPath + saveName;
        Debug.Log("we're in the import method");
        //if file exists
        if(File.Exists(fullPath))
        {
            //Debug.Log("we're in the if statement");
            string jsonData = File.ReadAllText(fullPath);
            Debug.Log("we set jsondata up");
            //Debug.Log("clearing inventory, prepping for import. right now inventory channel is " + inventoryChannel);
            inventoryChannel.RaiseInventoryClear();
            Debug.Log("inventory cleared");

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
            Debug.Log("successfully imported " + fullPath);
        }
    }
}
