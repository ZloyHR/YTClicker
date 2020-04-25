using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    public void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
