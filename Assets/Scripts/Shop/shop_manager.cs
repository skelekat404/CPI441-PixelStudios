using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class shop_manager : MonoBehaviour
{
    public int money;
    public TMP_Text money_ui;
    public shop_item_object[] shop_item_s_objects;
    public GameObject[] shop_panels_GO;
    public shop_template[] shop_panels;
    public Button[] purchase_buttons;

    // *** Identifies which inventory purchased items go to ***
    [SerializeField]
    private InventoryChannel m_InventoryChannel;

    //TODO
    //add inventory channel for shopkeeper inventory, to hold sold items


    void Start()
    {
        for (int i = 0; i < shop_item_s_objects.Length; i++)
        {
            shop_panels_GO[i].SetActive(true);
        }

        money_ui.text = "Total Money: " + money.ToString();
        load_panels();
        check_purchasable(); //on game start, check if you can afford things
     }


    public void add_money()
    {
        money ++;
        money_ui.text = "Total Money: " + money.ToString();
        check_purchasable();
    }

    public void load_panels()
    {
        for (int i = 0; i < shop_item_s_objects.Length; i++)
        {
            shop_panels[i].title_text.text = shop_item_s_objects[i].title;
            shop_panels[i].description_text.text = shop_item_s_objects[i].description;
            shop_panels[i].price_text.text =  shop_item_s_objects[i].base_price.ToString() + " Gems";
        }
    }

    public void check_purchasable()
    {
        for (int i = 0; i < shop_item_s_objects.Length; i++)
        {
            if (money >= shop_item_s_objects[i].base_price) //if you can afford the item
            {
                purchase_buttons[i].interactable = true;    //button lights up
            }
            else
            {
                purchase_buttons[i].interactable = false;   //otherwise button is deactivated
            }
        }
    }

    public void purchase_item(int button_num)
    {
        if (money >= shop_item_s_objects[button_num].base_price) //if you can afford the item
        {
            money = money - shop_item_s_objects[button_num].base_price; //remove that item's cost from your wallet
            money_ui.text = "Total Money: " + money.ToString();         //update wallet total
            m_InventoryChannel?.RaiseLootItem(shop_item_s_objects[button_num].m_LootableItem);          //add the purchased item to your inventory
           
            check_purchasable();                                        //then check if you can afford things
        }
    }
}
