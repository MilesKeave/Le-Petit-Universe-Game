using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MilesTestScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Button goBackButton;
    void Awake()
    {
        goBackButton.onClick.AddListener(OnClickMenu);
    }

    // Update is called once per frame
    private void OnClickMenu(){

        MenuScene.Instance.ShowMenu();
    }
}
