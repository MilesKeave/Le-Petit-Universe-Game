using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomizationWindow : MonoBehaviour
{
    [SerializeField] private Button confirmButton;
    [SerializeField] private TextMeshProUGUI pointText;

    private void Awake()
    {
        confirmButton.onClick.AddListener(OnConfirm);
    }

    private void OnEnable()
    {
        pointText.text = GameManager.Instance.PlayerRewardPoint.ToString();
    }

    private void OnConfirm()
    {
        MenuScene.Instance.ShowMenu();
    }
}
