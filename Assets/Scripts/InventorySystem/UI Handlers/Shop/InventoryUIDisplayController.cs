using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIDisplayController : MonoBehaviour
{
    [SerializeField]
    private InventoryUIChannel InventoryUIChannel;
    
    private InventoryUIController m_InventoryUIController = null;

    private void Awake()
    {
        m_InventoryUIController = GetComponent<InventoryUIController>();
        
        InventoryUIChannel.OnInventoryToggle += OnInventoryToggle;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        InventoryUIChannel.OnInventoryToggle -= OnInventoryToggle;
    }

    private void OnInventoryToggle(InventoryHolder inventoryHolder)
    {
        if (m_InventoryUIController.DisplayedInventory == null)
        {
            gameObject.SetActive(true);
            m_InventoryUIController.PopulateInventoryUI(inventoryHolder.Inventory);
        }
        else if (m_InventoryUIController.DisplayedInventory == inventoryHolder.Inventory)
        {
            gameObject.SetActive(false);
            m_InventoryUIController.ClearInventoryUI();
        }
    }
}