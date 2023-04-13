using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Audio : MonoBehaviour


{
    
    AudioSource audioSource;
    private bool PlayJetSound;

    // Start is called before the first frame update
    void Awake()
    {

        audioSource = GetComponent<AudioSource>();
        audioSource.mute = true;

        

        
       
    }

    // Update is called once per frame

    void Update ()
    {
        PlayJetSound = GetComponent<OnTerrainCheck>().IsGrounded;

        //if (PlayJetSound == true)
        if (Input.GetKey(KeyCode.W)){
            audioSource.mute = false;

        }

        else {
            audioSource.mute = true;

        }
    
    }
}
