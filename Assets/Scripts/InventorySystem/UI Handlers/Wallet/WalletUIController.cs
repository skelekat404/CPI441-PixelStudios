using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WalletUIController : MonoBehaviour
{
    [SerializeField]
    private InventoryUIChannel InventoryUIChannel;
    [SerializeField]
    private int m_Wallet;
    [SerializeField]
    private Text walletUIData;

    private InventorySystem.Inventory m_DisplayedWallet;

    private void Awake()
    {
        InventoryUIChannel.OnInventoryToggle += OnInventoryToggle;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        InventoryUIChannel.OnInventoryToggle -= OnInventoryToggle;
    }

    private void OnInventoryToggle(InventoryHolder inventoryHolder)
    {
        InventorySystem.Inventory equippedItemInventory = inventoryHolder.GetComponent<InventoryHolder>().Inventory;

        if (m_DisplayedWallet == null)
        {
            gameObject.SetActive(true);
            m_DisplayedWallet = equippedItemInventory;
        }
        else if (m_DisplayedWallet == equippedItemInventory)
        {
            gameObject.SetActive(false);
            m_DisplayedWallet = null;
        }
    }

    public void UpdateWallet(int value)
    {
        m_Wallet += value;         //First(x => x.WatchedStat == stat).StatUIField.text = string.Format("{0:0}", value);
        walletUIData.text = string.Format("{0:0", m_Wallet);
    }
}
