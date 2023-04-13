using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class TypewriterEffect : MonoBehaviour
{

    public float charsPerSecond = 0.17f;//����ʱ����
   [TextArea]
    public string words = "";//��Ҫ��ʾ������,���ⲿ����Ҳ�ɵ��÷������룬�������StartEffect�������룬��Ϊ�ⲿ�����Ҳ��Ҫ����StartEffect������

    private bool isActive = false;//�Ƿ�ʼ����Ч��
    private float timer;//��ʱ��
    private Text myText;//��ʾ���ı���start�����
    private int currentPos = 0;//��ǰ����λ��

    public UnityEvent PlayOver = new UnityEvent();


    public bool IsActive { get { return isActive; } }
    void Start()
    {
        timer = 0;
        charsPerSecond = Mathf.Max(0.02f, charsPerSecond); //����ʱ������С��0.1
        myText = GetComponent<Text>();
        myText.text = "";//��ʼ���ı���

        StartEffect();
    }

    void Update()
    {
        OnStartWriter();
    }
    /// <summary>
    /// �ⲿ���ô˷�������ʼʵ��Ч��
    /// </summary>
    /// <param name="word">��Ҫ��������֣����ⲿ��д����Բ���˲���</param>
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
                Debug.LogError("�ַ�Ϊ��");
            }
        }

    }
    /// <summary>
    /// ִ�д�������
    /// </summary>
    void OnStartWriter()
    {

        if (isActive)
        {
            timer += Time.deltaTime;
            if (timer >= charsPerSecond)
            {//�жϼ�ʱ��ʱ���Ƿ񵽴�
                timer = 0;
                currentPos++;
                myText.text = words.Substring(0, currentPos);//ˢ���ı���ʾ����

                if (currentPos >= words.Length)
                {
                    OnFinish();
                }
            }

        }
    }
    /// <summary>
    /// �������֣���ʼ������
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