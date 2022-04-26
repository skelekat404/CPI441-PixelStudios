using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WalletMoneyManager : MonoBehaviour
{
    public int totalMoney;
    public TMP_Text money_ui;
    public SaleValueDisplayController saleValueDisplayController;

    void Awake()
    {
        Debug.Log("total money before start: " + totalMoney);
        //set initial money total to value in database
        totalMoney = FirebaseManager.Singleton.userMoney;
        Debug.Log("total after update: " + totalMoney);
        money_ui.text = "Total Money: " + totalMoney.ToString();
    }

    public void addValueToWallet()
    {
        Debug.Log("total money rn is: " + 0);
        totalMoney += saleValueDisplayController.displayedSaleValue;
        //update database value to reflect sold items
        updateFirebaseMoneyTotal(totalMoney);
        Debug.Log("total money after adding display is " + totalMoney);
        money_ui.text = "Total Money: " + totalMoney.ToString();
    }

    public void updateFirebaseMoneyTotal(int newMoney)
    {
        Debug.Log("cloud money = " + FirebaseManager.Singleton.userMoney);
        FirebaseManager.Singleton.userMoney = newMoney;
        FirebaseManager.Singleton.SaveMoney();
    }
}