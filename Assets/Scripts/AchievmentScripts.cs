using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Achievment",fileName = "New Achievment",order = 101)]
public class AchievmentScripts : ScriptableObject
{
    public new string name;
    public string congratulations;
    public string description;
    public string needSubcribers = "0";
    public string needViews = "0";
    public string needMoneys = "0";
    public Sprite image;
}

public class Achievment
{
    public static string prefixName = "Achievment ";
    public string name;
    public string congratulations;
    public string description;
    public Currency needSubcribers;
    public Currency needViews;
    public Currency needMoneys;
    public Sprite image;
    public bool used = false;

    public Achievment(AchievmentScripts achievment)
    {
        name = achievment.name;
        congratulations = achievment.congratulations;
        description = achievment.description;
        needSubcribers = new Currency(achievment.needSubcribers);
        needViews = new Currency(achievment.needViews);
        needMoneys = new Currency(achievment.needMoneys);
        image = achievment.image;
        used = SaveSystem.Get(used ? 1 : 0, prefixName + name) == 1;
    }

    public void Save()
    {
        SaveSystem.Save(used ? 1 : 0,prefixName + name);
    }
}
