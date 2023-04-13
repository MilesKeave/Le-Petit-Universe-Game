using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private Button embarkButton;
    [SerializeField] private Button customizationButton;
    [SerializeField] private Button scoreBoardButton;

    private void Awake()
    {
        embarkButton.onClick.AddListener(OnClickEmbark);
        customizationButton.onClick.AddListener(OnClickCustomization);
        scoreBoardButton.onClick.AddListener(OnClickScoreBoard);

    }



    //��ʾ�ؿ�
    private void OnClickEmbark()
    {
        MenuScene.Instance.ShowSelect(); //��ʾѡ��ؿ�
    }

    //��ʾ�Զ���װ��
    private void OnClickCustomization()
    {
        MenuScene.Instance.ShowCustomization(); //��ʾװ������
    }

    //��ʾ������
    private void OnClickScoreBoard()
    {
        
    }

}
