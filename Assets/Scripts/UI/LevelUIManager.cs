using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUIManager : MonoBehaviour
{
    public static LevelUIManager Instance;
    [SerializeField] private MissionSucceedPanel succeedPanel;
    [SerializeField] private MissionFailedPanel failedPanel;
    [SerializeField] private PlayingPanel playingPanel;

    public bool isPlaying = true;

    private void Awake()
    {
        Instance = this;
    }


    /// <summary>
    /// ÔÝÍ£
    /// </summary>
    public void Pause()
    {
        playingPanel.Pause();
    }


    /// <summary>
    /// È¡ÏûÔÝÍ£
    /// </summary>
    public void Unpause()
    {
        playingPanel.Unpause();
    }


    public void MissionFailed()
    {
        isPlaying = false;
        failedPanel.gameObject.SetActive(true);
    }

    public void MissionSuccess()
    {
        isPlaying = false;
        succeedPanel.Show(playingPanel.CostTime);
    }
}
