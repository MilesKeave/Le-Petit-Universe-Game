using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MissionSucceedPanel : MonoBehaviour
{
    #region Inspector Field
    [SerializeField] private Button tryAgainButton;
    [SerializeField] private Button nextMissionButton;
    [SerializeField] private Image[] stars;
    [SerializeField] private TextMeshProUGUI finalTimeText;
    [SerializeField] private TextMeshProUGUI collectedText;
    [SerializeField] private TextMeshProUGUI rewardText;
    #endregion

    private const string FinalTimeString = "Mission completed in <color=#00aaaa>{TIME}</color> s";
    private const string CollectedString = "Venus sample collected <color=#00ffff>{NUMBER}</color>x";
    private const string RewardString = "Reward: <color=#00ffff>{NUMBER}</color> points for suit customization";

    #region Unity Func
    private void Awake()
    {
        tryAgainButton.onClick.AddListener(TryAgain);
        nextMissionButton.onClick.AddListener(NextMission);

        HideAllStars();
    }

    private void Start()
    {
        //��������Ч��
        //ShowStars(3);
    }

    #endregion

    #region Public
    public void Show(float costTime)
    {
        gameObject.SetActive(true);
        int collected = GameManager.Instance.LevelGemNumber;
        finalTimeText.text = FinalTimeString.Replace("{TIME}", Mathf.RoundToInt(costTime).ToString());
        collectedText.text = CollectedString.Replace("{NUMBER}", collected.ToString());
        rewardText.text = RewardString.Replace("{NUMBER}", (collected * 10).ToString());
        GameManager.Instance.LevelSuccessSummary(costTime);
        HideAllStars();
        ShowStars(GameManager.Instance.GetStarByTime(costTime));
    }
    #endregion

    #region Private

    // �����һ������
    private void NextMission()
    {
        SceneManager.LoadScene("Menu");
    }

    //����ٴγ���
    private void TryAgain()
    {
        Time.timeScale = 1;
        GameManager.Instance.LoadSelectedLevel();
    }


    //��������

    //�������е����Ƕ���
    private void InactiveAllStars()
    {
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].gameObject.SetActive(false);
        }
    }

    //�������е�����
    private void HideAllStars()
    {
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].color = new Color(1, 1, 1, 0);
        }
    }

    //��ʾ����
    private void ShowStars(int num)
    {
        StartCoroutine(nameof(ShowStarsCoroutine),num);
    }

    //��ʾ����Э��
    private IEnumerator ShowStarsCoroutine(int num)
    {
        InactiveAllStars();
        for (int i = 0; i < stars.Length; i++)
        {
            if(i < num)
            {
                stars[i].gameObject.SetActive(true);
                stars[i].color = new Color(1, 1, 1, 0);
            }
        }

        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].color = Color.white;
            yield return new WaitForSeconds(0.5f);
        }
    }

    #endregion
}
