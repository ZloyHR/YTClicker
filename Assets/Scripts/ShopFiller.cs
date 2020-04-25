using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopFiller : MonoBehaviour
{
    private ProductsScripts[] productsScripts;
    public GameObject productPrefab;
    public GameObject popUp;
    private Button popUpButton;
    private void Start()
    {
        productsScripts = Resources.LoadAll<ProductsScripts>("Products");
        popUpButton = popUp.transform.GetChild(2).GetComponent<Button>();
        for(int i = 0;i < productsScripts.Length; ++i)
        {
            var product = new Products(productsScripts[i]);
            if (product.used) continue;
            GameObject current;
            current = Instantiate(productPrefab, transform);
            current.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = product.name;
            current.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = product.cost.ToString();
            var button = current.transform.GetChild(2).GetComponent<Button>();
            Products prod = product;
            button.onClick.AddListener(delegate { OnClicked(current,prod); });
        }
    }

    public void OnClicked(GameObject current,Products product)
    {
        if (MoneyController.money >= product.cost)
        {
            popUp.active = true;
            popUp.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = product.name;
            popUp.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = product.description;
            popUp.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = product.cost.ToString();
            popUpButton.onClick.AddListener(delegate { OnBuyClicked(current,product); });
        }
        else
        {    
            current.transform.GetChild(2).GetComponent<Animator>().Play("buttonTilt");
        }
    }
    
    public void OnBuyClicked(GameObject current,Products product)
    {
        if (MoneyController.g.BuyProduct(product.cost))
        {
            Destroy(current);
            product.used = true;
            ClickHandler.subsPerSecond += product.addSubsPerSecond;
            ClickHandler.viewsPerSecond += product.addViewsPerSecond;
            ClickHandler.viewsPerClick += product.addViewsPerClick;
            Monetization.moneyPerView += product.addMoneyPerView;
            SaveSystem.Save(Monetization.moneyPerView,"moneyPerView");
            ClickHandler.Save();
            product.Save();
            OnClose();
        }
    }

    public void OnClose()
    {
        popUpButton.onClick.RemoveAllListeners();
        popUp.active = false;
    }
}
