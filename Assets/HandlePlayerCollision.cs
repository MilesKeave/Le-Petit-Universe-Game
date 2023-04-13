using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePlayerCollision : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource AsteroidSound;

    void Start()
    {
        AsteroidSound = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        GetComponent<ParticleSystem>().Play();
        AsteroidSound.Play();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<TrailRenderer>().enabled = false;
        GetComponent<TrailRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().Sleep();
        
    }

   
}
