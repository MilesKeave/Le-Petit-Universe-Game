using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCondition : MonoBehaviour
{
    public ConditionsObject Planet;
    public Rigidbody2D rb2d;

    public bool WeakJetpack;
    public bool NoProtection;

    private FlashRed _flashImage;
    public Color _newColor = Color.red;

    void Awake()
    {

        if (GameManager.Instance.LevelJetpackPoint < Planet.JetpackForce)
        {
            WeakJetpack = true; 
            
            //here flash the Jetpack Icon in HUD (for the entirety of the gameplay)!
        }
        else
        {
            WeakJetpack = false;
        }

        
        if (GameManager.Instance.LevelAtmospherePoint < Planet.AtmosphereProtection)
        {
            //here flash Athmosphere Icon in HUD
            //also, make Oxygen Bar last 5 seconds (instead of 3 minutes)

            //shake the camera
            //unfreeze z axis
            NoProtection = true;
        }

        else if (GameManager.Instance.LevelThermalPoint < Planet.TemperatureResistance)
        {
            //here flash Thermal Icon in HUD
            //also, make Oxygen Bar last 10 seconds (instead of 3 minutes)

            //below we are flashing the entire screen red for 10 seconds
            _flashImage = GameObject.Find("FlashRed").GetComponent<FlashRed>();
            float[] Durations = {0f,0.8f,1.6f,2.4f,3.2f,4f,4.8f,5.6f,6.4f,7.2f,8f,8.2f,9f,9.2f,10f};

            foreach(float Duration in Durations)
            {
                StartCoroutine(FlashAfter(Duration));
            }

            //Atmosphere Protection is fine
            NoProtection = false;
        }
        else
        {
            //Atmosphere Protection is fine
            NoProtection = false;
        }
    }

    IEnumerator FlashAfter(float Duration)
    {
        yield return new WaitForSeconds(Duration);
        Debug.Log("Flash!");
        _flashImage.StartFlash(.25f, .5f, _newColor);
    }
}
