using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class StatsController : MonoBehaviour
{
    public static Currency views;
    public static Currency subscribers;
    public static StatsController g;
    public TextMeshProUGUI viewText;
    public TextMeshProUGUI subText;
    public GameObject pref;
    public Transform floatingTextParent;
    public float windowXSize;
    public float windowYSize;
    private Camera mainCamera;
    private void Start()
    {
        mainCamera = Camera.main;
        g = this;
        views = SaveSystem.Get(views,"views");
        subscribers = SaveSystem.Get(subscribers,"subscribers");
        UpdateSubs(new Currency());
        UpdateViews(new Currency());
    }

    public void UpdateSubs(Currency add)
    {
        windowYSize = mainCamera.orthographicSize;
        windowXSize = windowYSize * mainCamera.aspect;
        if (add.money > 0)
        {
            GameObject curent = Instantiate(pref, floatingTextParent);
            curent.transform.position = new Vector3(Random.Range(-windowXSize + 0.25f,windowXSize - 0.25f),Random.Range(-windowYSize + 0.25f,windowXSize - 0.25f),curent.transform.position.z);
            curent.GetComponent<TextMeshProUGUI>().text = "+ " + add + " subs";
            StartCoroutine(GoUp(curent,Random.Range(0.3f,5f),Random.Range(1f,3f)));
        }
        subscribers += add;
        SaveSystem.Save(subscribers,"subscribers");
        subText.text = subscribers + " subscribers";
    }
    
    public void UpdateViews(Currency add)
    {
        windowYSize = mainCamera.orthographicSize;
        windowXSize = windowYSize * mainCamera.aspect;
        if (add.money > 0)
        {
            GameObject curent = Instantiate(pref, floatingTextParent);
            curent.transform.position = new Vector3(Random.Range(-windowXSize + 0.25f,windowXSize - 0.25f),Random.Range(-windowYSize + 0.25f,windowXSize - 0.25f),curent.transform.position.z);
            curent.GetComponent<TextMeshProUGUI>().text = "+ " + add + " views";
            StartCoroutine(GoUp(curent,Random.Range(0.3f,5f),Random.Range(1f,3f)));
        }
        views += add;
        //if(views - Monetization.paidViews >= 1000)
            Monetization.MoneyByViews();
        SaveSystem.Save(views,"views");
        viewText.text = views + " views";
    }

    public IEnumerator GoUp(GameObject current,float y,float time)
    {
        Vector3 start = current.transform.position;
        Vector3 finish = start;
        finish.y += y;
        for (float i = 0; i < 1; i += Time.deltaTime / time)
        {
            current.transform.position = Vector3.Lerp(start, finish, i);
            yield return null;
        }
        Destroy(current);
    }
}
