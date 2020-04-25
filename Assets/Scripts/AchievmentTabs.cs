using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AchievmentTabs : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public GameObject achievmentPrefab;
    public GameObject noAchievment;
    public GameObject offSecreen;
    private RectTransform _rect;
    private Vector3 _startPosition;
    private Vector2 _delta;
    private int panelId;

    private void Start()
    {
        _rect = GetComponent<RectTransform>();
        int id = 0;
        if (AchievmentHandler.achievments == null)
        {
            return;
        }
        foreach (var achievment in AchievmentHandler.achievments)
        {
            if (!achievment.used) continue;
            GameObject current = Instantiate(achievmentPrefab, transform);
            current.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = achievment.congratulations;
            current.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = achievment.description;
            current.transform.GetChild(2).gameObject.GetComponent<Image>().sprite = achievment.image;
            current.transform.localPosition += new Vector3(_rect.rect.width * id++,0,0);
        }
        if (transform.childCount != 0) noAchievment.active = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startPosition = _rect.localPosition;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        _delta.x = eventData.position.x - eventData.pressPosition.x;
        _rect.localPosition = _startPosition + (Vector3)_delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        offSecreen.active = true;
        float percent = Mathf.Abs(_delta.x) / _rect.rect.width;
        Vector3 finishPosition;
        if (percent < 0.2f)
        {
            finishPosition = _startPosition;
        }
        else
        {
            int deltaSign = _delta.x < 0 ? 1 : -1;
            panelId += deltaSign;
            if (panelId < 0 || panelId >= transform.childCount)
            {
                finishPosition = _startPosition;
                panelId -= deltaSign;
            }
            else
            {
                finishPosition = _startPosition - new Vector3(_rect.rect.width, 0, 0) * deltaSign;
            }

        }
        StartCoroutine(SmoothSwap(finishPosition, .5f));
    }

    IEnumerator SmoothSwap(Vector3 finishPosition,float second)
    {
        Vector3 startPosition = _rect.localPosition;
        for (float i = 0; i < 1; i += Time.deltaTime / second)
        {
            _rect.localPosition = Vector3.Lerp(startPosition, finishPosition, Mathf.SmoothStep(0,1,i));
            yield return null;
        }
        _rect.localPosition = finishPosition;
        offSecreen.active = false;
    }
}
