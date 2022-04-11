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

    public InventoryHolder m_SaleInventoryHolder;

    private void Awake()
    {
        m_ShopUIChannel.OnShopToggle += OnToggleShopUI;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        m_ShopUIChannel.OnShopToggle -= OnToggleShopUI;
    }

    private void OnToggleShopUI(InventoryHolder playerInventoryHolder)
    {
        Debug.Log(playerInventoryHolder.Inventory);
        if (playerInventoryHolder.Inventory != null)
        {
            Debug.Log("the inventory is not null. setting active and populating UI for both.");
            gameObject.SetActive(true);
            m_PlayerInventoryUIController.PopulateInventoryUI(playerInventoryHolder.Inventory);
            m_SaleInventoryUIController.PopulateInventoryUI(m_SaleInventoryHolder.Inventory);
        }
        else
        {
            Debug.Log("the inventory is null. deactivating and clearing.");
            gameObject.SetActive(false);
            m_PlayerInventoryUIController.ClearInventoryUI();
            m_SaleInventoryUIController.ClearInventoryUI();
        }
    }

    public void OnToggleSaleUI(InventoryHolder saleInventoryHolder)
    {
        Debug.Log("sale inv is : " + saleInventoryHolder.Inventory);
        if(saleInventoryHolder.Inventory != null)
        {
            Debug.Log("sale is not null");
            gameObject.SetActive(true);
            m_SaleInventoryUIController.PopulateInventoryUI(saleInventoryHolder.Inventory);
        }
        else
        {
            Debug.Log("sale is null??");
        }
    }
}