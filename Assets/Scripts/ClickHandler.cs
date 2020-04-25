using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class ClickHandler : MonoBehaviour
{
    public static Currency viewsPerClick = new Currency(1);
    public static Currency viewsPerSecond = Currency.zero;
    public static Currency subsPerSecond = Currency.zero;
    public static float secondsFromLastExit;

    private void Start()
    {
        DateTime nowTime = DateTime.UtcNow;
        DateTime prevTime = DateTime.UtcNow;
        prevTime = SaveSystem.Get(prevTime,"prevTime");
        TimeSpan delta = nowTime - prevTime;
        secondsFromLastExit = Mathf.Clamp((float)delta.TotalSeconds, 0f, 6f * 60 * 60);
        viewsPerClick = SaveSystem.Get(viewsPerClick,"viewsPerClick");
        viewsPerSecond = SaveSystem.Get(viewsPerSecond,"viewsPerSecond");
        subsPerSecond = SaveSystem.Get(subsPerSecond,"subsPerSecond");
        StatsController.subscribers += subsPerSecond * secondsFromLastExit;
        StatsController.views += viewsPerSecond * secondsFromLastExit + StatsController.subscribers * RandomCurrency(0f,60f) * secondsFromLastExit;
        StatsController.g.UpdateSubs(Currency.zero);
        StatsController.g.UpdateViews(Currency.zero);
        StartCoroutine(SleepAdd(false));
        StartCoroutine(SleepAdd(true));
    }

    public void AddViews()
    {
        StatsController.g.UpdateViews(viewsPerClick * PowerUp.multiply);
    }

    public static void Save()
    {
        SaveSystem.Save(viewsPerClick,"viewsPerClick");
        SaveSystem.Save(viewsPerSecond,"viewsPerSecond");
        SaveSystem.Save(subsPerSecond,"subsPerSecond");
    }

    public IEnumerator SleepAdd(bool isView)
    {
        while (true)
        {
            SaveSystem.Save(DateTime.UtcNow,"prevTime");
            if (isView)
            {
                StatsController.g.UpdateViews(viewsPerSecond + StatsController.subscribers * RandomCurrency(0f,60f));
            }
            else StatsController.g.UpdateSubs(subsPerSecond);
            yield return new WaitForSeconds(1f);
        }
    }

    public static float RandomCurrency(float leftBorder, float rightBorder)
    {
        return Random.Range(leftBorder, rightBorder) / rightBorder;
    }
}
