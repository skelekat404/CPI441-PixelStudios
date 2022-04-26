using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem;

[RequireComponent(typeof(InventoryHolder))]
public class InventoryDatabaseUpdateManager : MonoBehaviour
{
    [SerializeField] InventoryChannel inventoryChannel;
    InventoryHolder m_inventoryHolder;

    private void Awake()
    {
        m_inventoryHolder = GetComponent<InventoryHolder>();
        inventoryChannel.OnInventoryExport += UpdateFirebaseInventoryData;
        inventoryChannel.OnInventoryImport += UpdateFirebaseInventoryData;
    }
    
    public void UpdateFirebaseInventoryData()
    {
        string newInventoryData = InventoryConverterHelper.ExportInventory(m_inventoryHolder);
        FirebaseManager.Singleton.userInventory = newInventoryData;
        FirebaseManager.Singleton.SaveInventory();
    }
}
