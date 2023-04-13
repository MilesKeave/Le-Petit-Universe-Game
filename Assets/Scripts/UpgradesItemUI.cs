using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UpgradesItemUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Button increaseButton;
    [SerializeField] private Button reduceButton;
    [SerializeField] private Image curPointImg;
    [SerializeField] private Sprite[] pointSprites;
    [SerializeField] private int point;
    [SerializeField] private CanvasGroup guideCanvasGroup;
    [SerializeField] private TextMeshProUGUI guideText;
    [SerializeField] private TextMeshProUGUI guideTitleText;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private TextMeshProUGUI infoTitleText;

    private UpgradesWindow upgradesWindow;
    private string infoString = "";
    private string infoTitleString = "";


    public int Point { get { return point; } }

    public void Initialize(UpgradesWindow window, int point,int rightPoint, string guideString, string infoTitle, string infoString)
    {
        upgradesWindow = window;
        increaseButton.onClick.AddListener(Increase);
        reduceButton.onClick.AddListener(Reduce);
        this.point = point;
        curPointImg.sprite = pointSprites[point];
        guideCanvasGroup.alpha = 0;
        guideText.text = guideString;
        this.infoString = infoString;
        string guideTitleString = "";
        if (rightPoint == point)
        {
            guideTitleString = "Stay";
        }
        else if (rightPoint > point)
        {
            guideTitleString += (rightPoint - point) + " Levels Up";
        }
        else
        {
            guideTitleString += (point - rightPoint) + " Levels Down";
        }
        guideTitleText.text = guideTitleString;
        infoTitleString = infoTitle;
    }

    private void OnDisable()
    {
        increaseButton.onClick.RemoveListener(Increase);
        reduceButton.onClick.RemoveListener(Reduce);
    }

    //ºı…Ÿ
    private void Reduce()
    {
        if (upgradesWindow.RemainderPoint < 5 && point - 1 >= 0)
        {
            point--;
            curPointImg.sprite = pointSprites[point];
            upgradesWindow.RemainderPoint++;
        }
    }

    //‘ˆº”
    private void Increase()
    {
        if (upgradesWindow.RemainderPoint > 0 && point + 1 <= 5)
        {
            point++;
            curPointImg.sprite = pointSprites[point];
            upgradesWindow.RemainderPoint--;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        guideCanvasGroup.DOFade(1, 0.3f);
        infoText.text = infoString;
        infoTitleText.text = infoTitleString;
        upgradesWindow.ShowInfo();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        guideCanvasGroup.DOFade(0, 0.3f);
        upgradesWindow.HideInfo();
    }
}
