using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Inventory/InventoryItem")]
    public class InventoryItem : CompositeScriptableObject
    {
        public uint m_Uid;
        public string Name;
        public Sprite Sprite;
        public InventoryItemType ItemType;
        public int SaleValue; //Value of item when sold to vendors

        public uint Uid { get => m_Uid; set => m_Uid = value;}
    }
}
