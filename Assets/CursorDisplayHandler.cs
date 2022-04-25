using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorDisplayHandler : MonoBehaviour
{
    public GameObject inventoryInstance;
    public GameObject cursorSlotInstance;

    public GameObject FindInventoryInstance()
    {
        GameObject inventoryInstance;
        inventoryInstance = GameObject.Find("InventoryFull");
        //Debug.Log("inv instance is: " + inventoryInstance);
        return inventoryInstance;
    }

    public void EnableCursorSlot()
    {
        //get instance of inventory
      //  GameObject inventoryInstance  = FindInventoryInstance();
        //set components of inventory instance to inactive
        InventorySlotUIController slotUIController = inventoryInstance.GetComponent<InventorySlotUIController>();
        slotUIController.enabled = true;
        FollowMouse followMouse = inventoryInstance.GetComponent<FollowMouse>();
        slotUIController.enabled = true;
        InventoryCursorController cursorController = inventoryInstance.GetComponent<InventoryCursorController>();
        slotUIController.enabled = true;
    }

    public void DisableCursorSlot()
    {
        //get instance of inventory
      //  GameObject inventoryInstance  = FindInventoryInstance();
        //set components of inventory instance to inactive
        /*InventorySlotUIController slotUIController = inventoryInstance.GetComponent<InventorySlotUIController>();
        slotUIController.enabled = false;
        FollowMouse followMouse = inventoryInstance.GetComponent<FollowMouse>();
        slotUIController.enabled = false;
        InventoryCursorController cursorController = inventoryInstance.GetComponent<InventoryCursorController>();
        slotUIController.enabled = false;*/

        //disable the scripts on cursorslot
        cursorSlotInstance.GetComponent<InventorySlotUIController>().enabled = false;
        cursorSlotInstance.GetComponent<FollowMouse>().enabled = false;
        cursorSlotInstance.GetComponent<InventoryCursorController>().enabled = false;
    }
}
