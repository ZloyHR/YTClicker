using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MoneyController : MonoBehaviour
{
    public string startMoney = "1K";
    public static Currency money;
    public static MoneyController g;
    public TextMeshProUGUI[] moneyText;

    private void Awake()
    {
        g = this;
        money = new Currency(startMoney);
        money = SaveSystem.Get(money, "money");
        UpdateMoney();
    }

    public bool BuyProduct(Currency cost)
    {
        if (money >= cost)
        {
            money -= cost;
            UpdateMoney();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void UpdateMoney()
    {
        SaveSystem.Save(money,"money");
        foreach (var text in moneyText)
        {
            text.text = money.ToString();
        }
    }
}
