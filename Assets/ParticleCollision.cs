using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    // Start is called before the first frame update
    private FlashRed _flashImage;
    public Color _newColor;
    
    public bool IsSlow;
    public bool Venus;
    void Start()
    {
        IsSlow=  false;
        _flashImage = GameObject.Find("FlashRed").GetComponent<FlashRed>();
    }

    void OnParticleCollision(GameObject collision){

        if ((collision.gameObject.tag == "Player")) {
            if (!IsSlow)
            {
                IsSlow= true;
                _flashImage.StartFlash(.25f, .5f, _newColor);
                if (Venus){
                    StartCoroutine(LavaSlow());
                    StartCoroutine(Flash());
                    StartCoroutine(Flash2());
                    StartCoroutine(Flash3());
                    StartCoroutine(Flash4());
                    Debug.Log("Slow");
                }
                else {
                    StartCoroutine(SolarSlow());
                    StartCoroutine(SolarSlow2());
                }
            }
            
        }
    }
    IEnumerator LavaSlow()
    {
        yield return new WaitForSeconds(4);
        IsSlow=false;
        Debug.Log("Unslow");
        _flashImage.StartFlash(.25f, .5f, _newColor);
        
    }
    
    IEnumerator Flash(){
        yield return new WaitForSeconds(0.8f);
        Debug.Log("Flash!");
         _flashImage.StartFlash(.25f, .5f, _newColor);
    }
    IEnumerator Flash2(){
        yield return new WaitForSeconds(1.6f);
         _flashImage.StartFlash(.25f, .5f, _newColor);
    }
    IEnumerator Flash3(){
        yield return new WaitForSeconds(2.4f);
         _flashImage.StartFlash(.25f, .5f, _newColor);
    }
    IEnumerator Flash4(){
        yield return new WaitForSeconds(3.2f);
         _flashImage.StartFlash(.25f, .5f, _newColor);
    }
    IEnumerator SolarSlow(){

        yield return new WaitForSeconds(0.8f);
         _flashImage.StartFlash(.25f, .5f, _newColor);
        
    }
    IEnumerator SolarSlow2(){

        yield return new WaitForSeconds(1.6f);
        IsSlow= false;
        
    }
    
}
