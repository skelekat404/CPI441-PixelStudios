using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem;
using Firebase;
using Firebase.Extensions;
using Firebase.Database;

[RequireComponent(typeof(InventoryHolder))]
public class InventoryDatabaseUpdateManager : MonoBehaviour
{
    [SerializeField] InventoryChannel inventoryChannel;
    InventoryHolder m_inventoryHolder;
    //public string savedInventoryData;
    InventorySaveLoad m_inventorySaveLoad;
    public string savedInventory;

    private void Awake()
    {
        
        m_inventoryHolder = GetComponent<InventoryHolder>();
        Debug.Log("IDUM holder: " + m_inventoryHolder);
        m_inventorySaveLoad = GetComponent<InventorySaveLoad>();
        Debug.Log("IDUM data: " + m_inventorySaveLoad);

         Debug.Log("current user id is: " + FirebaseManager.Singleton.DBreference.Child("users").Child(FirebaseManager.Singleton.User.UserId));
        FirebaseManager.Singleton.DBreference.Child("users").Child(FirebaseManager.Singleton.User.UserId).Child("inventory_contents").GetValueAsync().ContinueWithOnMainThread(task => {
        if (task.IsFaulted) {
          // Handle the error...
        }
        else if (task.IsCompleted) {
          DataSnapshot snapshot = task.Result;
          savedInventory = snapshot.GetValue(true).ToString();
          Debug.Log("string is: " + savedInventory);
          savedInventory = FirebaseManager.Singleton.userInventory;
          m_inventorySaveLoad.OnSpecificInventoryImport(savedInventory);
        }
      });
        
        //savedInventory = FirebaseManager.Singleton.userInventory;
        //m_inventorySaveLoad.OnSpecificInventoryImport(savedInventory);

       // savedInventoryData = FirebaseManager.Singleton.userInventory;
       // Debug.Log("IDUM saveed inv data: " + savedInventoryData);
       // m_inventorySaveLoad.OnSpecificInventoryImport(savedInventoryData);
        Debug.Log("IDUM spec inv imp");
        inventoryChannel.OnInventoryExport += UpdateFirebaseInventoryData;
        inventoryChannel.OnInventoryImport += UpdateFirebaseInventoryData;
       // m_inventorySlot.OnItemChange += UpdateFirebaseInventoryData;
    }
    
    public void UpdateFirebaseInventoryData()
    {
        string newInventoryData = InventoryConverterHelper.ExportInventory(m_inventoryHolder);
        FirebaseManager.Singleton.userInventory = newInventoryData;
        FirebaseManager.Singleton.SaveInventory();
    }
}
