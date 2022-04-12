using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaleValueDisplayController : MonoBehaviour
{
    public ShopSaleManager displaySaleManager;
    [SerializeField]
    public int displayedSaleValue;
    
    public TMP_Text sale_value_ui;

    // *** Identifies which inventory purchased items go to ***

    //TODO
    //add inventory channel for shopkeeper inventory, to hold sold items

    public void Awake()
    {
        displaySaleManager.OnValueUpdate += ValueUpdated; //subscribing to update event
    }
    private void OnDestroy()
    {
        displaySaleManager.OnValueUpdate -= ValueUpdated;
    }

    public void ValueUpdated(int updatedSaleValue)
    {
        if(updatedSaleValue != null)
        {
            Debug.Log("updated not null is: " + updatedSaleValue);
            displayedSaleValue = updatedSaleValue;
            Debug.Log("displayed value is: " + displayedSaleValue);
        }
        else
        {
            displayedSaleValue = updatedSaleValue;
            Debug.Log("theres a null, returning");
            return;
        }
        GameObject saleValueDisplay = GameObject.Find("SaleValueDisplay");
        Debug.Log("sale inventory set to : " + saleValueDisplay);
        displaySaleManager = saleValueDisplay.GetComponent<ShopSaleManager>();
        displaySaleManager.saleTotal = displayedSaleValue;
        sale_value_ui.text = "The value of these items is: " + displayedSaleValue.ToString();
    }
}