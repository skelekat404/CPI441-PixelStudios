using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Inventory/ShopUIChannel")]
public class ShopUIChannel : ScriptableObject
{
    public delegate void InventoryToggleCallback(InventoryHolder inventoryHolder);
    public InventoryToggleCallback OnInventoryToggle;

    public delegate void ShopToggleCallback(InventoryHolder inventoryHolder);
    public ShopToggleCallback OnShopToggle;

    public void RaiseToggle(InventoryHolder inventoryHolder)
    {
        OnInventoryToggle?.Invoke(inventoryHolder);
    }

    public void OnToggleShopUI(InventoryHolder playerInventoryHolder)
    {
        if(playerInventoryHolder != null)
        {
            playerInventoryHolder.Inventory.Clear();
            OnShopToggle?.Invoke(playerInventoryHolder);
        }
        else
        {
            Debug.Log("it is [" + playerInventoryHolder.Inventory + "] with holder [" + playerInventoryHolder);
        }
    }
}
