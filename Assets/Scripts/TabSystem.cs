using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TabSystem : MonoBehaviour,IPointerClickHandler
{
    public int id;
    public void OnPointerClick(PointerEventData eventData)
    {
        TabListener.activeId = id;
    }
}
