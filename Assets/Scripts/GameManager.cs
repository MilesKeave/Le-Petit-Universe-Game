using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ��Ϸ������
/// ���������ȫ����Ϣ��
/// </summary>
public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    private Dictionary<string, int> levelRecord = new Dictionary<string, int>(); //�ؿ���¼
    [SerializeField]
    private LevelUpgradesInformation upgradesInfomation;

    public LevelUpgradesInformation UpgradesInfomation { get { return upgradesInfomation; }
        set
        {
            upgradesInfomation = value;
        }
    }
    public string SelectedLevelName { get { return upgradesInfomation.levelDefine.ToString(); } }

    public int LevelAtmospherePoint = 0;
    public int LevelThermalPoint = 0;
    public int LevelJetpackPoint = 0;

    public int PlayerRewardPoint = 0;


    public int LevelGemNumber = 0; //���ؿ��ڵ�gem����

    public Dictionary<LevelDefine, bool> LevelUnlockDict = new Dictionary<LevelDefine, bool>();
    public Dictionary<LevelDefine, float> LevelShortestTimeDict = new Dictionary<LevelDefine, float>();

    private int mercuryLevelPlayCount = 0;

    //������ʾ�ӵ����,����Ϸ���ڼӵ����ʧ�ܺ���Ϊtrue
    public bool RequestShowUpdates { get; set; }
    

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //Test code start, will delete
        upgradesInfomation = new LevelUpgradesInformation()
        {
            levelDefine = LevelDefine.Venus,
            AtmospherePoint = 2,
            JetpackPoint = 2,
            ThermalPoint = 1,
        };
        //Test code end
        LevelShortestTimeDict.Add(LevelDefine.Venus,0);
        LevelShortestTimeDict.Add(LevelDefine.Mercury, 0);
        LevelUnlockDict.Add(LevelDefine.Venus, true);
        LevelUnlockDict.Add(LevelDefine.Mercury, true);
    }



    public void LoadSelectedLevel()
    {
        if (upgradesInfomation.levelDefine == LevelDefine.Mercury) mercuryLevelPlayCount++;
        AudioManager.Instance.StopBGMImmediately();
        string sceneName = upgradesInfomation.levelDefine + "Game";
        if (upgradesInfomation.levelDefine == LevelDefine.Mercury && mercuryLevelPlayCount % 2 == 0)
        {
            sceneName += "Night";
        }
          
        SceneManager.LoadScene(sceneName);
    }


    public int GetStar(LevelDefine level)
    {
        float time = LevelShortestTimeDict[level];
        if (time == 0)
        {
            return 0;
        }
        return GetStarByTime(time);
    }

    public int GetStarByTime(float time)
    {
        if (time <= 120)
        {
            return 5;
        }
        else if (time <= 135)
        {
            return 4;
        }
        else if (time <= 150)
        {
            return 3;
        }
        else if (time <= 165)
        {
            return 2;
        }
        else if (time <= 180)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public void LevelSuccessSummary(float costTime)
    {
        if(upgradesInfomation.levelDefine == LevelDefine.Venus)
        {
            LevelUnlockDict[(LevelDefine)(upgradesInfomation.levelDefine + 1)] = true; //������һ��
        }
        PlayerRewardPoint += LevelGemNumber * 10;
        float shortestTime = LevelShortestTimeDict[upgradesInfomation.levelDefine]; //��ȡ��ǰ����ʱ��
        if (shortestTime == 0 || costTime < shortestTime)
        {
            LevelShortestTimeDict[upgradesInfomation.levelDefine] = costTime;
        }
    }
}
