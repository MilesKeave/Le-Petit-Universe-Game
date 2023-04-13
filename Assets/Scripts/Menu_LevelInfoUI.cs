using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �˵����� �ؿ���Ϣ  UI
/// </summary>
public class Menu_LevelInfoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelNameText; //�ؿ�����
    [SerializeField] private Button levelButton; //�ؿ���ť
    [SerializeField] private RectTransform starParent; //���Ǹ�����
    [SerializeField] private Image[] stars; //�ؿ���������
    [SerializeField] private float starWidth = 40; //����ͼƬ���
    [SerializeField] private Image lockImage; //����ͼ��
    [SerializeField] private bool unlock = false; //�ؿ��Ƿ����
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
        if (!unlock) return; //���δ��������ִ�к����߼�
        GameManager.Instance.UpgradesInfomation = upgradesInfomation;
        MenuScene.Instance.ShowInstruction();
    }


    //��ʾָ������������
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
