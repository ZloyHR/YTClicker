﻿﻿using System;
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

    public void PowerUpUse()
    {
        StartCoroutine(PowerUpReload(reloadTime,workTime));
    }
    
    public IEnumerator PowerUpReload(float seconds,float wait = 0)
    {
        if(wait > 0)yield return new WaitForSeconds(wait);
        gameObject.active = false;
        for (float i = 0; i < 1; i += Time.deltaTime / seconds)
        {
            bar.GetComponent<Image>().fillAmount = i;
            yield return null;
        }
        bar.GetComponent<Image>().fillAmount = 1;
        gameObject.active = true;
        
    }
}
