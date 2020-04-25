using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievmentHandler : MonoBehaviour
{
    public AchievmentScripts[] achievmentScripts;
    public static Achievment[] achievments;
    public static Queue<Achievment> currentAchievments = new Queue<Achievment>();
    public GameObject popUp;
    private void Start()
    {
        achievmentScripts = Resources.LoadAll<AchievmentScripts>("Achievments");
        achievments = new Achievment[achievmentScripts.Length];
        popUp.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => 
            {currentAchievments.Dequeue();});
        for (int i = 0; i < achievmentScripts.Length; ++i)
        {
            achievments[i] = new Achievment(achievmentScripts[i]);
        }
        StartCoroutine(CheckAchievment());
        StartCoroutine(ClearStack());
    }

    private IEnumerator CheckAchievment()
    {
        while (true)
        {
            foreach (var achievment in achievments)
            {
                if (achievment.used) continue;
                if (achievment.needViews <= StatsController.views
                    && achievment.needSubcribers <= StatsController.subscribers
                    && achievment.needMoneys <= MoneyController.money)
                {
                    achievment.used = true;
                    achievment.Save();
                    currentAchievments.Enqueue(achievment);
                }
            }
            yield return new WaitForSeconds(2f);
        }
    }

    public IEnumerator ClearStack()
    {
        while (true)
        {
            while (currentAchievments.Count > 0)
            {
                Achievment current = currentAchievments.Peek();
                if (!popUp.active)
                {
                    popUp.active = true;
                    popUp.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = current.congratulations;
                    popUp.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = current.description;
                    popUp.transform.GetChild(2).GetComponent<Image>().sprite = current.image;
                }

                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(2f);
        }
    }
}
