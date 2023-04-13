using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Splash : MonoBehaviour
{
    public GameObject splash1;
    public GameObject splash2;
    public TypewriterEffect splash1Text;
    public TypewriterEffect splash2Text;
    public Button yesButton;

    private void Awake()
    {
        splash1.SetActive(true);
        splash2.SetActive(false);
        yesButton.gameObject.SetActive(false);
        yesButton.onClick.AddListener(LoadMenu);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (splash1.activeSelf)
            {
                if (splash1Text.IsActive)
                {
                    splash1Text.OnFinish();
                }
                else
                {
                    ShowSplash2();
                }
            }
            else if (splash2.activeSelf)
            {
                if (splash2Text.IsActive)
                {
                    splash2Text.OnFinish();
                    yesButton.gameObject.SetActive(true);
                }
            }
        }
    }
    public void ShowSplash2()
    {
        splash1.SetActive(false);
        splash2.SetActive(true);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
