using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class shop_manager : MonoBehaviour
{
    public WalletMoneyManager walletMoneyManager;
    public int money;
    public TMP_Text money_ui;
    public shop_item_object[] shop_item_s_objects;
    public GameObject[] shop_panels_GO;
    public shop_template[] shop_panels;
    public Button[] purchase_buttons;
    public GameObject playerCursorSlot;

    // *** Identifies which inventory purchased items go to ***
    [SerializeField]
    private InventoryChannel m_InventoryChannel;

    //TODO
    //add inventory channel for shopkeeper inventory, to hold sold items

    // *** Player Object Instance ***
    //put this in shop manager
    //

    void Start()
    {
        money = walletMoneyManager.totalMoney;
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
        //find player instance
        GameObject playerInstance = FindPlayerInstance();
        

        if (money >= shop_item_s_objects[button_num].base_price) //if you can afford the item
        {
            money = money - shop_item_s_objects[button_num].base_price; //remove that item's cost from your wallet
            money_ui.text = "Total Money: " + money.ToString();         //update wallet total
            m_InventoryChannel?.RaiseLootItem(shop_item_s_objects[button_num].m_LootableItem);          //add the purchased item to your inventory


            switch(button_num)
            {
                case 0:
                    playerInstance.GetComponent<SCR_ImportantVariables>().setPlayerHealth(1); break;
                case 1:
                    playerInstance.GetComponent<SCR_ImportantVariables>().setJetpack(true); break;
                case 2:
                    playerInstance.GetComponent<SCR_ImportantVariables>().setLavaWalk(true); break;
                case 3:
                    playerInstance.GetComponent<SCR_ImportantVariables>().setRocketBoots(true); break;
                case 4:
                    playerInstance.GetComponent<SCR_ImportantVariables>().setScuba(true); break;
                case 5:
                    playerInstance.GetComponent<SCR_ImportantVariables>().setWarpDrive(true); break;
                default:
                break;
            }
            /*if(button_num == 1)
            {
                playerInstance.GetComponent<SCR_ImportantVariables>().setJetpack(true);
            }*/
           
            check_purchasable();                                        //then check if you can afford things
        }
    }

    public GameObject FindPlayerInstance()
    {
        GameObject player;
        player = GameObject.FindGameObjectWithTag("Player");
        //player.GetComponentInChildren();
       // SCR_ImportantVariables playerVariables = player.GetComponent<SCR_ImportantVariables>();
        return player;
    }
    
    public GameObject FindCursorSlotInstance()
    {
        GameObject inventoryInstance;
        inventoryInstance = GameObject.Find("PlayerInventoryCursorSlot");
       // Debug.Log("inv instance is: " + inventoryInstance);
        return inventoryInstance;
    }

    public void EnableCursorSlot()
    {
        //get instance of inventory
        GameObject cursorSlotInstance  = FindCursorSlotInstance();
        //set components of inventory instance to inactive
        cursorSlotInstance.GetComponent<InventorySlotUIController>().enabled = true;
        cursorSlotInstance.GetComponent<FollowMouse>().enabled = true;
        cursorSlotInstance.GetComponent<InventoryCursorController>().enabled = true;
    }

    public void DisableCursorSlot()
    {
        //get instance of inventory
        GameObject inventoryInstance  = FindCursorSlotInstance();
        //set components of inventory instance to inactive
        InventorySlotUIController slotUIController = inventoryInstance.GetComponent<InventorySlotUIController>();
        slotUIController.enabled = false;
        FollowMouse followMouse = inventoryInstance.GetComponent<FollowMouse>();
        slotUIController.enabled = false;
        InventoryCursorController cursorController = inventoryInstance.GetComponent<InventoryCursorController>();
        slotUIController.enabled = false;
    }
}