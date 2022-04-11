using System;
using System.Linq;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    [SerializeField]
    private InventorySlotUIController SlotControllerPrefab;
    
    private InventorySystem.Inventory m_DisplayedInventory;
    public InventorySystem.Inventory DisplayedInventory => m_DisplayedInventory;
    public InventoryHolder m_SaleInventoryHolder;

    private void Start()
    {
        //Debug.Log("the holder on " + GetComponent<InventoryHolder>().ToString() + "is " + m_SaleInventoryHolder);
    }
    
    public void PopulateInventoryUI(InventorySystem.Inventory inventory)
    {
        inventory = m_SaleInventoryHolder.Inventory;
        m_DisplayedInventory = inventory; //TODO : FIGURE OUT WHY THIS BROKE. WAS ORIGINALLY inventoryHolder.Inventory;
        m_DisplayedInventory.OnSlotAdded += CreateSlotController;
        m_DisplayedInventory.OnSlotRemoved += DestroyInventorySlot;
        m_DisplayedInventory.ForEach(CreateSlotController);
    }
    
    public void ClearInventoryUI()
    {
        Array.ForEach(GetComponentsInChildren<InventorySlotUIController>(), slot => Destroy(slot.gameObject));
        m_DisplayedInventory.OnSlotRemoved -= DestroyInventorySlot;
        m_DisplayedInventory.OnSlotAdded -= CreateSlotController;
        m_DisplayedInventory = null;
    }

    public void ClearSpecificInventoryUI(InventoryHolder inventoryHolder)
    {
        Array.ForEach(GetComponentsInChildren<InventorySlotUIController>(), slot => Destroy(slot.gameObject));
        m_DisplayedInventory.OnSlotRemoved -= DestroyInventorySlot;
        m_DisplayedInventory.OnSlotAdded -= CreateSlotController;
        m_DisplayedInventory = null;
        Debug.Log("UI for [" + inventoryHolder + "] cleared and nulled");
    }

    private void CreateSlotController(InventorySystem.InventorySlot slot)
    {
        InventorySlotUIController newSlotController = Instantiate(SlotControllerPrefab, transform);
        newSlotController.InventorySlot = slot;
    }

    private void DestroyInventorySlot(InventorySystem.InventorySlot slot)
    {
        InventorySlotUIController foundController = GetComponentsInChildren<InventorySlotUIController>().FirstOrDefault(slotController => slotController.InventorySlot == slot);
        if (foundController != null)
        {
            Destroy(foundController.gameObject);
        }
    }
}
