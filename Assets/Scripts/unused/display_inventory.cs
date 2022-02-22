using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class display_inventory : MonoBehaviour
{
    //public GameObject inventory_prefab;
    public inventory_object inventory;
    public int x_start;
    public int y_start;

    public int x_space_between_items;
    public int y_space_between_items;
    public int number_of_columns;
    Dictionary<inventory_slot, GameObject> items_displayed = new Dictionary<inventory_slot, GameObject>();

   /* void Start()
    {
        create_display();
    }

    void Update()
    {
        for (int i = 0; i < inventory.container.Count; i++)
        {
            if (items_displayed.ContainsKey(inventory.container[i]))
            {
                items_displayed[inventory.container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.container[i].amount.ToString("n0");
            }
            else
            {
                var obj = Instantiate(inventory_prefab, Vector3.zero, Quaternion.identity, transform);
                obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite
                obj.GetComponent<RectTransform>().localPosition = get_position(i);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.container[i].amount.ToString("n0");
                items_displayed.Add(inventory.container[i], obj);
            }
        }
    }
    public void create_display()
    {
        for (int i = 0; i < inventory.container.Count; i++)
        {
            var obj = Instantiate(inventory.container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = get_position(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.container[i].amount.ToString("n0");
        }
    }
    public Vector3 get_position(int i)
    {
        return new Vector3(x_start + (x_space_between_items * (i % number_of_columns)), y_start + ((-y_space_between_items * (i / number_of_columns))), 0f);
    }*/
}
