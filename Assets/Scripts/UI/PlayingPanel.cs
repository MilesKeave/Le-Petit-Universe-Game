using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayingPanel : MonoBehaviour
{
    #region InspectorField
    [SerializeField]
    private GameObject pauseUI;
    [SerializeField]
    private Button abortMessionButton;
    [SerializeField]
    private Image o2Image;
    [Space]
    [Header("InfoItems")]
    [SerializeField] private Text cText;
    [SerializeField] private Text speedText;
    [SerializeField] private Text atmText;
    [SerializeField] private Text sulfuricAcidText;
    [SerializeField] private Text resistText;
    [SerializeField] private Text forceText;
    [SerializeField] private Text protectionText;
    [SerializeField] private Animation o2BarAnimation;
    [SerializeField] private StatusBar resistStatus;
    [SerializeField] private StatusBar forceStatus;
    [SerializeField] private StatusBar protectionStatus;

    [SerializeField] private TextMeshProUGUI gemNumberText;





    [SerializeField] private float time = 3 * 60;

    [SerializeField] private float remainderTime = 0;
    #endregion

    private float timer = 0;
    private bool isPause = false;
    
    public float CostTime { get { return time - remainderTime; } }


    private GemMining gemMining;


    #region Unity event func
    private void Awake()
    {
        abortMessionButton.onClick.AddListener(AbortMission);
        o2Image.fillAmount = 1;
        gemMining = GameObject.FindGameObjectWithTag("Player").GetComponent<GemMining>();

        
    }

    private void OnEnable()
    {
        resistStatus.SetValue(GameManager.Instance.LevelThermalPoint); //�¶�
        forceStatus.SetValue(GameManager.Instance.LevelJetpackPoint); //����
        protectionStatus.SetValue(GameManager.Instance.LevelAtmospherePoint); //����
        Debug.Log((GameManager.Instance.LevelThermalPoint, GameManager.Instance.UpgradesInfomation.RightThermalPoint));
        if (GameManager.Instance.LevelThermalPoint != GameManager.Instance.UpgradesInfomation.RightThermalPoint)
        {
            resistStatus.Error();
            time = 10;
        }
        if (GameManager.Instance.LevelAtmospherePoint != GameManager.Instance.UpgradesInfomation.RightAtmospherePoint)
        {
            protectionStatus.Error();
            time = 5;
        }
        if (GameManager.Instance.LevelJetpackPoint != GameManager.Instance.UpgradesInfomation.RightJetpackPoint)
        {
            forceStatus.Error();
        }
    }


    private void Update()
    {
        if (!LevelUIManager.Instance.isPlaying) return;

        timer += Time.deltaTime;
        remainderTime = time - timer;

        if (remainderTime <= 30 && !o2BarAnimation.isPlaying)
        {
            o2BarAnimation.Play();
        }
        if (remainderTime <= 0)
        {
            o2Image.fillAmount = 0;
            LevelUIManager.Instance.MissionFailed();
        }
        else
        {
            o2Image.fillAmount = remainderTime / time;
        }

        gemNumberText.text = "X" + GameManager.Instance.LevelGemNumber;
    }

    #endregion

    #region Private func
    private void AbortMission()
    {
        Pause();
    }

    #endregion

    #region Public func
    public void Pause()
    {
        Time.timeScale = 0;
        pauseUI.SetActive(true);
        isPause = true;
    }


    public void Unpause()
    {
        Time.timeScale = 1;
        pauseUI.SetActive(false);
        isPause = false;
    }

    public void RetrunHome()
    {
        GameManager.Instance.RequestShowUpdates = true;
        Unpause();
        SceneManager.LoadScene("Menu");
    }
  
    #endregion
}
