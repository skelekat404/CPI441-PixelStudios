using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUIDisplayController : MonoBehaviour
{
    [SerializeField]
    private ShopUIChannel m_ShopUIChannel;
    [SerializeField]
    private InventoryUIController m_PlayerInventoryUIController;
    [SerializeField]
    private InventoryUIController m_SaleInventoryUIController;
    
    private InventoryHolder m_SaleInventoryHolder = null;

    private void Awake()
    {
        m_SaleInventoryHolder = GetComponent<InventoryHolder>();
        
        m_ShopUIChannel.OnShopToggle += OnToggleShopUI;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        m_ShopUIChannel.OnShopToggle -= OnToggleShopUI;
    }

    private void OnToggleShopUI(InventoryHolder playerInventoryHolder)
    {
        if (playerInventoryHolder.Inventory != null)
        {
            gameObject.SetActive(true);
            m_PlayerInventoryUIController.PopulateInventoryUI(playerInventoryHolder.Inventory);
            m_SaleInventoryUIController.PopulateInventoryUI(m_SaleInventoryHolder.Inventory);
        }
        else
        {
            gameObject.SetActive(false);
            m_PlayerInventoryUIController.ClearInventoryUI();
            m_SaleInventoryUIController.ClearInventoryUI();
        }
    }
}