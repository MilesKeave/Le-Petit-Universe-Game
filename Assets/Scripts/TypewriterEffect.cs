using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class TypewriterEffect : MonoBehaviour
{

    public float charsPerSecond = 0.17f;//打字时间间隔
   [TextArea]
    public string words = "";//需要显示的文字,可外部输入也可调用方法输入，建议调用StartEffect方法输入，因为外部输入后也需要调用StartEffect方法。

    private bool isActive = false;//是否开始打字效果
    private float timer;//计时器
    private Text myText;//显示的文本，start里查找
    private int currentPos = 0;//当前打字位置

    public UnityEvent PlayOver = new UnityEvent();


    public bool IsActive { get { return isActive; } }
    void Start()
    {
        timer = 0;
        charsPerSecond = Mathf.Max(0.02f, charsPerSecond); //控制时间间隔最小是0.1
        myText = GetComponent<Text>();
        myText.text = "";//初始化文本框

        StartEffect();
    }

    void Update()
    {
        OnStartWriter();
    }
    /// <summary>
    /// 外部调用此方法，开始实现效果
    /// </summary>
    /// <param name="word">需要输入的文字，在外部填写后可以不填此参数</param>
    public void StartEffect(string word = "")
    {
        isActive = true;
        if (words == "" && word != null)
        {
            words = word;
        }
        else
        {
            if (word == null)
            {
                Debug.LogError("字符为空");
            }
        }

    }
    /// <summary>
    /// 执行打字任务
    /// </summary>
    void OnStartWriter()
    {

        if (isActive)
        {
            timer += Time.deltaTime;
            if (timer >= charsPerSecond)
            {//判断计时器时间是否到达
                timer = 0;
                currentPos++;
                myText.text = words.Substring(0, currentPos);//刷新文本显示内容

                if (currentPos >= words.Length)
                {
                    OnFinish();
                }
            }

        }
    }
    /// <summary>
    /// 结束打字，初始化数据
    /// </summary>
    public void OnFinish()
    {
        isActive = false;
        timer = 0;
        currentPos = 0;
        myText.text = words;
        PlayOver?.Invoke();
    }




}