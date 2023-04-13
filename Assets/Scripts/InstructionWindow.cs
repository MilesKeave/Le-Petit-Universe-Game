using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionWindow : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Button backButton;

    private void Awake()
    {
        continueButton.onClick.AddListener(() =>
        {
            MenuScene.Instance.ShowUpgrades();
        });
        backButton.onClick.AddListener(() =>
        {
            MenuScene.Instance.ShowSelect();
        });
    }
}
