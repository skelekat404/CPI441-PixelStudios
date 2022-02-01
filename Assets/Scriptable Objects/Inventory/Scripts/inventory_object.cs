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
    private item_database_object database;
    public List<inventory_slot> container = new List<inventory_slot>();

    private void OnEnable()
    {
#if UNITY_EDITOR
        database = (item_database_object)AssetDatabase.LoadAssetAtPath("Assets/Resources/Database.asset", typeof(item_database_object));
#else
        database = Resources.Load<item_database_object>("Assets/Resources");
#endif
    }

    public void add_item(item_object _item, int _amount)
    {
        for (int i = 0; i < container.Count; i++)
        {
            if(container[i].item == _item)
            {
                container[i].add_amount(_amount);
                return;
            }
        }
        container.Add(new inventory_slot(database.get_id[_item], _item, _amount));
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
        for (int i = 0; i < container.Count; i++)
            container[i].item = database.get_item[container[i].id];
    }
    public void OnBeforeSerialize()
    {  
    }
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
