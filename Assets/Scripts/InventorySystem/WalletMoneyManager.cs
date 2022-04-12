using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WalletMoneyManager : MonoBehaviour
{
    public int totalMoney = 0;
    public TMP_Text money_ui;
    public SaleValueDisplayController saleValueDisplayController;

    public void addValueToWallet()
    {
        Debug.Log("total money rn is: " + 0);
        totalMoney += saleValueDisplayController.displayedSaleValue;
        Debug.Log("total money after adding display is " + totalMoney);
        money_ui.text = "Total Money: " + totalMoney.ToString();
    }
}