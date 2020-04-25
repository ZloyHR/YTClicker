﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour
{
    public GameObject bar;
    public float reloadTime;
    public float workTime;
    private void Awake()
    {
        StartCoroutine(PowerUpReload(reloadTime));
    }

    public void StartPowerUp()
    {
        StartCoroutine(PowerUpUse(workTime));
    }
    
    public IEnumerator PowerUpUse(float seconds)
    {
        GetComponent<Button>().interactable = false;
        Currency pviewsPerClick = ClickHandler.viewsPerClick;
        ClickHandler.viewsPerClick += pviewsPerClick;
        yield return new WaitForSeconds(seconds);
        ClickHandler.viewsPerClick -= pviewsPerClick;
        StartCoroutine(PowerUpReload(reloadTime));
    }
    
    public IEnumerator PowerUpReload(float seconds,float wait = 0)
    {
        GetComponent<Button>().interactable = false;
        if(wait > 0)yield return new WaitForSeconds(wait);
        for (float i = 0; i < 1; i += Time.deltaTime / seconds)
        {
            bar.GetComponent<Image>().fillAmount = i;
            yield return null;
        }
        bar.GetComponent<Image>().fillAmount = 1;
        GetComponent<Button>().interactable = true;
        
    }
}
