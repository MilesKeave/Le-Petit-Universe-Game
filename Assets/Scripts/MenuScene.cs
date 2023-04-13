using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �˵�����
/// </summary>
public class MenuScene : MonoBehaviour
{
    [SerializeField] private GameObject menuUI; //��ʼ�˵�����
    [SerializeField] private GameObject customizationWindow; // �Զ���װ������
    [SerializeField] private GameObject instructionWindow; // ˵������
    [SerializeField] private GameObject upgradesWindow;  //��������
    [SerializeField] private GameObject levelInfos;  //�ؿ���Ϣ
    [SerializeField] private GameObject selectLevelTitle;  //ѡ��ؿ�����ı���
    [SerializeField] private GameObject goToMainMenu;

    public static MenuScene Instance; //��̬ʵ�� �����ⲿ����


    private void Awake()
    {
        Instance = this;

        HideAll();
        menuUI.SetActive(true);
        AudioManager.Instance.PlayBGM(AudioManager.BGM);
    }

    private void Start()
    {
        if (GameManager.Instance.RequestShowUpdates)
        {
            GameManager.Instance.RequestShowUpdates = false;
            ShowUpgrades();
        }
    }



    public void HideAll()
    {
        menuUI.SetActive(false);
        customizationWindow.SetActive(false);
        upgradesWindow.SetActive(false);
        levelInfos.SetActive(false);
        selectLevelTitle.SetActive(false);
        instructionWindow.SetActive(false);
        goToMainMenu.SetActive(false);

    }

    public void ShowMenu()
    {
        HideAll();
        menuUI.SetActive(true);
    }

    public void ShowSelect()
    {
        HideAll();
        levelInfos.SetActive(true);
        selectLevelTitle.SetActive(true);
        goToMainMenu.SetActive(true);
    }


    public void ShowCustomization()
    {
        HideAll();
        customizationWindow.SetActive(true);
    }

    public void ShowUpgrades()
    {
        HideAll();
        upgradesWindow.SetActive(true);
    }

    public void ShowInstruction()
    {
        HideAll();
        instructionWindow.SetActive(true);
    }
}
