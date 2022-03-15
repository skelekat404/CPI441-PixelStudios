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


     void Start()
     {
         for (int i = 0; i < shop_item_s_objects.Length; i++)
         {
            shop_panels_GO[i].SetActive(true);
         }

        money_ui.text = "Total Money: " + money.ToString();
        load_panels();
        check_purchasable();
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
            if (money >= shop_item_s_objects[i].base_price)
            {
                purchase_buttons[i].interactable = true;
            }
            else
            {
                purchase_buttons[i].interactable = false;
            }
        }
    }

    public void purchase_item(int button_num)
    {
        if (money >= shop_item_s_objects[button_num].base_price)
        {
            money = money - shop_item_s_objects[button_num].base_price;
            money_ui.text = "Total Money: " + money.ToString();
            check_purchasable();
        }
    }
}
