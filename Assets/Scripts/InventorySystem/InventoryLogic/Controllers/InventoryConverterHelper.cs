using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public static class InventoryConverterHelper
{
    public enum ConversionType
    {
        JSON
    }

    static ScriptableObjectLocator scriptableObjectLocator = null;

    public static void ImportInventory(string json, InventoryChannel inventoryChannel, ConversionType conversionType = ConversionType.JSON, bool additive = false)
    {
        if (json != null)
        {
            if (!additive) // Clear inventory if not being added
            {
                inventoryChannel.OnInventoryClear();
            }

            switch (conversionType) // Switch between conversion types
            {
                case ConversionType.JSON:
                    ImportInventoryFromJSON(json, inventoryChannel);
                    break;
            }
        }
    }

    private static void ImportInventoryFromJSON(string json, InventoryChannel inventoryChannel)
    {
        JArray obj = JArray.Parse(json);

        foreach (var item in obj)
        {
            uint uid = (uint)item["Uid"]; // Get item Uid
            InventorySystem.InventoryItem inventoryItem = FindItem(uid); // Find corresponding item
            uint quantity = (uint)item["Quantity"]; // Get quantity of item
            inventoryChannel.RaiseLootItem(inventoryItem, quantity); // Add it to inventory
        }
    }

    public static string ExportInventory(InventoryHolder inventoryHolder, ConversionType conversionType = ConversionType.JSON)
    {
        switch (conversionType)
        {
            case ConversionType.JSON:
                Debug.Log("in export switch case, holder is: " + inventoryHolder);
                return ExportInventoryToJSON(inventoryHolder);
        }
        return "broke";
    }

    private static string ExportInventoryToJSON(InventoryHolder inventoryHolder)
    {
        string jsonData = "";

        List<ItemSlot> inventory = new List<ItemSlot>();

        inventoryHolder.Inventory.ForEach
        ((slot) =>
        {
            if (slot.Item != null)
            {
                inventory.Add(new ItemSlot(slot.Item.Uid, slot.Quantity));
            }
        });
        jsonData = JsonConvert.SerializeObject(inventory);

        return jsonData;
    }

    struct ItemSlot
    {
        public ItemSlot(uint Uid, uint Quantity)
        {
            this.Uid = Uid;
            this.Quantity = Quantity;
        }
        public uint Uid { get; }
        public uint Quantity { get; }
    }

    public static InventorySystem.InventoryItem FindItem(uint uid)
    {
        if (scriptableObjectLocator == null) // Set if not set yet
        {
            scriptableObjectLocator = Resources.Load<ScriptableObjectLocator>("ScriptableObjects/SOLocator");
        }

        scriptableObjectLocator.ScriptableObjects.TryGetValue(uid, out ScriptableObject scriptableObject);
        if (scriptableObject != null)
        {
            InventorySystem.InventoryItem item = scriptableObject as InventorySystem.InventoryItem;
            if (item != null)
            {
                return item;
            }
        }
        return null;
    }
}