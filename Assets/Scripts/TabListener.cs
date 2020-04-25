using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TabListener : MonoBehaviour
{
    public static int activeId = 0;

    private void Update()
    {
        int id = 0;
        foreach (Transform content in transform)
        {
            if (id == activeId)
            {
                content.gameObject.active = true;
            }
            else
            {
                content.gameObject.active = false;
            }
            ++id;
        }
    }
}
