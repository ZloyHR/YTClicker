using System;
using UnityEngine;

public static class SaveSystem
{
    public static void Save(int saveObject,string name = "")
    {
        PlayerPrefs.SetInt(name,saveObject);
    }
    public static void Save(DateTime saveObject,string name = "")
    {
        PlayerPrefs.SetString(name,saveObject.ToString());
    }
    public static void Save<T>(T saveObject,string name = "")
    {
        PlayerPrefs.SetString(name == "" ? saveObject.GetHashCode().ToString() : name,JsonUtility.ToJson(saveObject));
    }
    
    public static int Get(int saveObject,string name = "")
    {
        if (PlayerPrefs.HasKey(name))
            return PlayerPrefs.GetInt(name);
        else
            return saveObject;
    }
    public static DateTime Get(DateTime saveObject,string name = "")
    {
        if (PlayerPrefs.HasKey(name))
            return DateTime.Parse(PlayerPrefs.GetString(name));
        else
            return saveObject;
    }
    public static T Get<T>(T saveObject,string name = "")
    {
        if (name != "" && PlayerPrefs.HasKey(name))
        {
            return JsonUtility.FromJson<T>(PlayerPrefs.GetString(name));
        }else if (saveObject != null && PlayerPrefs.HasKey(saveObject.GetHashCode().ToString()))
            return JsonUtility.FromJson<T>(PlayerPrefs.GetString(saveObject.GetHashCode().ToString()));
        else
            return saveObject;
    }
    // public static void Save(float saveObject,string name = "")
    // {
    //     PlayerPrefs.SetFloat(name,saveObject);
    // }
    // public static float Get(float saveObject,string name = "")
    // {
    //     if (PlayerPrefs.HasKey(name))
    //         return PlayerPrefs.GetFloat(name);
    //     else
    //         return saveObject;
    // }
}
