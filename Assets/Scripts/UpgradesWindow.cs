using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class UpgradesWindow : MonoBehaviour
{
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button backButton;
    [SerializeField] private UpgradesItemUI atmoSphere;
    [SerializeField] private UpgradesItemUI thermal;
    [SerializeField] private UpgradesItemUI jetpack;
    [SerializeField] private int remainderPoint = 5;
    [SerializeField] private Image pointImg;
    [SerializeField] private Sprite[] pointSprites;
    [SerializeField] private Image planetImg;
    [SerializeField] private Sprite[] planetSprites;
    [SerializeField] private CanvasGroup infoCanvasGroup;


    public int RemainderPoint 
    {
        get { return remainderPoint; } 
        set { remainderPoint = value; pointImg.sprite = pointSprites[remainderPoint]; } 
    }

    private void Awake()
    {
        confirmButton.onClick.AddListener(OnConfirm);
        backButton.onClick.AddListener(OnBack);
       

    }

    private void OnEnable()
    {
        LevelUpgradesInformation information = GameManager.Instance.UpgradesInfomation;
        atmoSphere.Initialize(this, information.AtmospherePoint, information.RightAtmospherePoint, information.AtmosphereGuideString,
            information.AtmosphereInfoTitleString, information.AtmosphereInfoString);

        thermal.Initialize(this, information.ThermalPoint, information.RightThermalPoint, information.ThermalGuideString,
            information.ThermalInfoTitleString, information.ThermalInfoString);

        jetpack.Initialize(this, information.JetpackPoint, information.RightJetpackPoint, information.JetpackGuideString,
            information.JetpackInfoTitleString, information.JetpackInfoString);

        remainderPoint = 5;
        pointImg.sprite = pointSprites[remainderPoint];
        infoCanvasGroup.alpha = 0;
        for (int i = 0; i < planetSprites.Length; i++)
        {
            if (planetSprites[i].name == GameManager.Instance.SelectedLevelName)
            {
                planetImg.sprite = planetSprites[i];
                break;
            }
        }
    }

    private void OnConfirm()
    {
        GameManager.Instance.LevelAtmospherePoint = atmoSphere.Point;
        GameManager.Instance.LevelThermalPoint = thermal.Point;
        GameManager.Instance.LevelJetpackPoint = jetpack.Point;
        GameManager.Instance.LoadSelectedLevel();
    }

    private void OnBack()
    {
        MenuScene.Instance.ShowInstruction();
    }

    public void ShowInfo()
    {
        infoCanvasGroup.DOFade(1, 0.3f);
    }

    public void HideInfo()
    {
        infoCanvasGroup.DOFade(0, 0.3f);
    }


}
