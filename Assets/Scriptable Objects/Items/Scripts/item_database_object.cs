using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Items/Database")]
public class item_database_object : ScriptableObject, ISerializationCallbackReceiver
{
    public item_object[] items;
    public Dictionary<item_object, int> get_id = new Dictionary<item_object, int>();
    public Dictionary<int, item_object> get_item = new Dictionary<int, item_object>();

    public void OnAfterDeserialize()
    {
        get_id = new Dictionary<item_object, int>();
        get_item = new Dictionary<int, item_object>();
        for (int i = 0; i < items.Length; i++)
        {
            get_id.Add(items[i], i);
            get_item.Add(i, items[i]);
        }
    }
    public void OnBeforeSerialize()
    {

    }
}
