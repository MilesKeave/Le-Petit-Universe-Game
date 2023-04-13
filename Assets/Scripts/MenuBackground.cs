using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 菜单背景
/// </summary>
public class MenuBackground : MonoBehaviour, IDragHandler 
{
    public float minX = 0;
    public float maxX = 0;

    private RectTransform selfRectTransform; //自身的变换组件

    private void Awake()
    {
        selfRectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 moveDelta = Vector2.right * eventData.delta.x;
        selfRectTransform.anchoredPosition += moveDelta;
        if (selfRectTransform.anchoredPosition.x <= minX) selfRectTransform.anchoredPosition = new Vector2(minX, selfRectTransform.anchoredPosition.y);
        if (selfRectTransform.anchoredPosition.x >= maxX) selfRectTransform.anchoredPosition = new Vector2(maxX, selfRectTransform.anchoredPosition.y);
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }
}
