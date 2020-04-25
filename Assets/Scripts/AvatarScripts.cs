using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "SO/Avatar",fileName = "new Avatar",order = 102)]
public class AvatarScripts : ScriptableObject
{
    public string cost = "0";
    public new string name;
    public string description;
    public Sprite image;
}
public class Avatar
{
    public static string prefixName = "Avatar ";
    public Currency cost;
    public new string name;
    public string description;
    public Sprite image;
    public bool used = false;

    public Avatar(AvatarScripts avatar)
    {
        cost = new Currency(avatar.cost);
        name = avatar.name;
        description = avatar.description;
        image = avatar.image;
        used = SaveSystem.Get(used ? 1 : 0, prefixName + name) == 1;

    }

    public void Save()
    {
        SaveSystem.Save(used ? 1 : 0,prefixName + name);
    }
}