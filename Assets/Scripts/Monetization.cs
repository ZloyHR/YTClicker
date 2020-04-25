using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class Monetization : MonoBehaviour
{
    public static Currency paidViews = Currency.zero;
    public static Currency moneyPerView = Currency.zero;

    private void Awake()
    {
        paidViews = SaveSystem.Get(paidViews,"paidViews");
        moneyPerView = SaveSystem.Get(moneyPerView,"moneyPerView");
    }

    public static void MoneyByViews()
    {
        Currency delta = StatsController.views - paidViews;
        paidViews = StatsController.views;
        SaveSystem.Save(paidViews,"paidViews");
        MoneyController.money += delta * moneyPerView;
        MoneyController.g.UpdateMoney();
    }
}
