using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MissionFailedPanel : MonoBehaviour
{
    #region Inspector Field
    [SerializeField] private Button abortMissionButton;
    [SerializeField] private Button startOverButton;
    [SerializeField] private TextMeshProUGUI faileReasonText;
    #endregion


    #region Unity Func
    private void Awake()
    {
        abortMissionButton.onClick.AddListener(AbortMission);
        startOverButton.onClick.AddListener(StartOver);
    }

    private void OnEnable()
    {
        GameManager.Instance.RequestShowUpdates = true;
        int n = 0;
        if (GameManager.Instance.LevelThermalPoint != GameManager.Instance.UpgradesInfomation.RightThermalPoint)
        {
            n++;
            faileReasonText.text = "Double check your Thermal Protection upgrade";
            GameManager.Instance.RequestShowUpdates = true;
        }
        if (GameManager.Instance.LevelAtmospherePoint != GameManager.Instance.UpgradesInfomation.RightAtmospherePoint)
        {
            n++;
            faileReasonText.text = "Double check your Atmosphere Protection upgrade";
            GameManager.Instance.RequestShowUpdates = true;
        }

        if(n == 2)
            faileReasonText.text = "Double check your Thermal Protection and Atmosphere Protection upgrade";
    }
    #endregion

    #region Private Func


    //Button event callback
    private void StartOver()
    {
        Time.timeScale = 1;
        GameManager.Instance.LoadSelectedLevel();
    }

    private void AbortMission()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    #endregion
}
