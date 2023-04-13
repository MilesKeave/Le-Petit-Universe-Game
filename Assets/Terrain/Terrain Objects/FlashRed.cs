using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Image))]
public class FlashRed : MonoBehaviour
{
    // Start is called before the first frame update
    Image _image = null;
    Coroutine _currentFlashRoutine = null;

    void Start(){

        _image = GetComponent<Image>();
        Debug.Log(_image);
        Debug.Log("??");

    }

    public void StartFlash(float secondsForOneFlash, float maxAlpha, Color newColor){

        _image = GetComponent<Image>();

        _image.color = newColor;
        maxAlpha = Mathf.Clamp(maxAlpha, 0, 1);

        if(_currentFlashRoutine != null){
            Debug.Log("called!");
            StopCoroutine(_currentFlashRoutine);
        }
        _currentFlashRoutine = StartCoroutine(Flash(secondsForOneFlash, maxAlpha));

    }
    IEnumerator Flash(float secondsForOneFlash, float maxAlpha){

        float flashInDuration = secondsForOneFlash / 2;
        for (float t = 0; t <= flashInDuration; t += Time.deltaTime){

            Color colorThisFrame = _image.color;
            colorThisFrame.a = Mathf.Lerp(0, maxAlpha, t/ flashInDuration);
            _image.color = colorThisFrame;
            yield return null;
            Debug.Log("called2!");
        }

        float flashOutDuration  = secondsForOneFlash / 2;
        for (float t = 0; t <= flashOutDuration; t+= Time.deltaTime){

            Color colorThisFrame = _image.color;
            colorThisFrame.a = Mathf.Lerp(maxAlpha, 0, t/ flashOutDuration);
            _image.color = colorThisFrame;
            yield return null;
            Debug.Log("called3!");
    
            
        }

        _image.color= new Color32(0,0,0,0);



    }

}
