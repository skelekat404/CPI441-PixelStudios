using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaleValueDisplayController : MonoBehaviour
{
    public int sale_value;
    public TMP_Text sale_value_ui;
    public Button[] purchase_buttons;

    // *** Identifies which inventory purchased items go to ***

    //TODO
    //add inventory channel for shopkeeper inventory, to hold sold items


    void Start()
    {
        sale_value_ui.text = "The value of these items is: " + sale_value.ToString();
     }


    public void add_value()
    {
        sale_value++;
        sale_value_ui.text = "The value of these items is: " + sale_value.ToString();
    }

        void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            sale_value_ui.text = "The value of these items is: 2";
        }
    }
}