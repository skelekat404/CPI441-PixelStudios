using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public inventory_object inventory;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var item = collision.GetComponent<Item>();
        if (item)
        {
            inventory.add_item(item.item, 1);
            Destroy(collision.gameObject);
            Debug.Log("Bonk! The item you collected was: " + item.item);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            inventory.save();
        }
        if(Input.GetKeyDown(KeyCode.B))
        {
            inventory.load();
        }
    }
    private void OnApplicationQuit()
    {
        inventory.container.Clear();
    }
    
}
