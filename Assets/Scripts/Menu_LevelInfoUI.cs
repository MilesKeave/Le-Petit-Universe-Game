using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 菜单场景 关卡信息  UI
/// </summary>
public class Menu_LevelInfoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelNameText; //关卡名称
    [SerializeField] private Button levelButton; //关卡按钮
    [SerializeField] private RectTransform starParent; //星星父对象
    [SerializeField] private Image[] stars; //关卡奖励星星
    [SerializeField] private float starWidth = 40; //星星图片宽度
    [SerializeField] private Image lockImage; //锁定图标
    [SerializeField] private bool unlock = false; //关卡是否解锁
    [SerializeField] private LevelUpgradesInformation upgradesInfomation;

    private Color hidenColor = new Color(1, 1, 1, 0);
    private Color showColor = new Color(1, 1, 1, 1);

    private void Awake()
    {
        levelButton.onClick.AddListener(OnClick);
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].color = hidenColor;
        }
        
    }


    private void Start()
    {
        unlock = GameManager.Instance.LevelUnlockDict[upgradesInfomation.levelDefine];
        lockImage.enabled = !unlock;
        levelButton.interactable = unlock;
        if (unlock)
        {
;            ShowStar(GameManager.Instance.GetStar(upgradesInfomation.levelDefine));
        }
    }

    private void OnClick()
    {
        if (!unlock) return; //如果未解锁，不执行后续逻辑
        GameManager.Instance.UpgradesInfomation = upgradesInfomation;
        MenuScene.Instance.ShowInstruction();
    }


    //显示指定数量个星星
    private void ShowStar(int num)
    {
        starParent.sizeDelta = new Vector2(num * starWidth, starParent.sizeDelta.y);
        for (int i = 0; i < stars.Length; i++)
        {
            if (i < num)
            {
                stars[i].gameObject.SetActive(true);
                stars[i].color = showColor;
            }
            else
                stars[i].gameObject.SetActive(false);
        }
    }
}
