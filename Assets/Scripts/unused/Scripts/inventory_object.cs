using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[CreateAssetMenu(fileName = "New Default Object", menuName = "Inventory System/Inventory")]
public class inventory_object : ScriptableObject, ISerializationCallbackReceiver
{
    public string save_path;
    public item_database_object database;
    public inventory container;

    public void add_item(item_object _item, int _amount)
    {
        for (int i = 0; i < container.items.Count; i++)
        {
            if(container.items[i].item == _item)
            {
                container.items[i].add_amount(_amount);
                return;
            }
        }
        container.items.Add(new inventory_slot(database.get_id[_item], _item, _amount));
    }

    public void save()
    {
        string save_data = JsonUtility.ToJson(this, true);
        BinaryFormatter binary_formatter = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, save_path));
        binary_formatter.Serialize(file, save_data);
        file.Close();
    }

    public void load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, save_path)))
        {
            BinaryFormatter binary_formatter = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, save_path), FileMode.Open);
            JsonUtility.FromJsonOverwrite(binary_formatter.Deserialize(file).ToString(), this);
            file.Close();
        }
    }


    public void OnAfterDeserialize()
    {
        for (int i = 0; i < container.items.Count; i++)
            container.items[i].item = database.get_item[container.items[i].id];
    }
    public void OnBeforeSerialize()
    {  
    }
}

[System.Serializable]
public class inventory
{
    public List<inventory_slot> items = new List<inventory_slot>();
}
[System.Serializable]
public class inventory_slot
{
    public int id;
    public item_object item;
    public int amount;
    public inventory_slot(int _id, item_object _item, int  _amount)
    {
        id = _id;
        item = _item;
        amount = _amount;
    }
    public void add_amount(int value)
    {
        amount += value;
    }
}
