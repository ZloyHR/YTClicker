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
    public static float multiply = 1;
    /*TODO
     *Make normal power up system 
     */
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
        SaveSystem.Save(0, "secondsSpent");
        multiply = 2;
        yield return new WaitForSeconds(seconds);
        multiply = 1;
        StartCoroutine(PowerUpReload(reloadTime));
    }
    
    public IEnumerator PowerUpReload(float seconds)
    {
        
        GetComponent<Button>().interactable = false;
        for (float i = SaveSystem.Get(0f, "secondsSpent") + ClickHandler.secondsFromLastExit / seconds; i < 1; i += Time.deltaTime / seconds)
        {
            ClickHandler.secondsFromLastExit = 0;//TODO it's bug
            bar.GetComponent<Image>().fillAmount = i;
            SaveSystem.Save(i, "secondsSpent");
            yield return null;
        }
        bar.GetComponent<Image>().fillAmount = 1;
        SaveSystem.Save(1, "secondsSpent");
        GetComponent<Button>().interactable = true;
        
    }
}
