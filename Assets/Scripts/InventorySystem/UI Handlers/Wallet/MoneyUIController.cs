using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class MoneyUIData
{
    public Stat WatchedStat = null;
    public Text StatUIField = null;
}

public class MoneyUIController : MonoBehaviour
{
    [SerializeField]
    private InventoryUIChannel InventoryUIChannel;
    [SerializeField]
    private StatUIData[] m_Stats;

    private InventorySystem.Inventory m_DisplayedEquippedItemsInventory;

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
        InventorySystem.Inventory equippedItemInventory = inventoryHolder.GetComponent<EquippedItemsHolder>().EquippedItemsInventory;

        if (m_DisplayedEquippedItemsInventory == null)
        {
            gameObject.SetActive(true);
            m_DisplayedEquippedItemsInventory = equippedItemInventory;
        }
        else if (m_DisplayedEquippedItemsInventory == equippedItemInventory)
        {
            gameObject.SetActive(false);
            m_DisplayedEquippedItemsInventory = null;
        }
    }
}
