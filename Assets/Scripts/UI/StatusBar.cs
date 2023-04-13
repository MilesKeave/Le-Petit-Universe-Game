using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    [SerializeField]
    private Image pointImage;
    [SerializeField]
    private Sprite[] pointsSprites;

    private Animation errorAnimation;

    private void Awake()
    {
        errorAnimation = GetComponent<Animation>();
    }


    public void SetValue(int value)
    {
        pointImage.sprite = pointsSprites[value];
    }

    public void Error()
    {
        errorAnimation.Play();
    }
}
