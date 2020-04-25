using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "SO/Product",order = 100)]
public class ProductsScripts : ScriptableObject
{
    public string cost = "0";
    public new string name;
    public string addViewsPerClick = "0";
    public string addViewsPerSecond = "0";
    public string addSubsPerSecond = "0";
    public string addMoneyPerView = "0";
    public string description;
}
public class Products
{
    public static string prefixName = "Products ";
    public Currency cost;
    public new string name;
    public string description;
    public Currency addViewsPerClick = Currency.zero;
    public Currency addViewsPerSecond =Currency.zero;
    public Currency addSubsPerSecond = Currency.zero;
    public Currency addMoneyPerView = Currency.zero;
    public bool used = false;

    public Products(ProductsScripts product)
    {
        cost = new Currency(product.cost);
        name = product.name;
        description = product.description;
        addViewsPerClick =  new Currency(product.addViewsPerClick);
        addViewsPerSecond =  new Currency(product.addViewsPerSecond);
        addSubsPerSecond =  new Currency(product.addSubsPerSecond);
        addMoneyPerView = new Currency(product.addMoneyPerView);
        used = SaveSystem.Get(used ? 1 : 0, prefixName + name) == 1;
    }

    public void Save()
    {
        SaveSystem.Save(used ? 1 : 0,prefixName + name);
    }
}