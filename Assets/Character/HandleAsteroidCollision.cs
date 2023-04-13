using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleAsteroidCollision : MonoBehaviour
{
    // Start is called before the first frame update
    
    public bool AsteroidDead;
    public Animator animator;
    private ParticleSystem JetsOn;
    private AudioSource DeathSound;

    void Start()
    {
        JetsOn = transform.parent.GetComponentInChildren<ParticleSystem>();
        AsteroidDead = false;
        DeathSound = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Asteroid")
        {
            Debug.Log("You were hit by an asteroid.");
            AsteroidDead = true;
            DeathSound.Play();
            transform.parent.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            transform.parent.GetComponent<Rigidbody2D>().constraints =  RigidbodyConstraints2D.None;
            JetsOn.Stop();
            StartCoroutine(DelayGameOver());
        }
    }

    private IEnumerator DelayGameOver()
    {
        yield return new WaitForSeconds(3.0f);
        LevelUIManager.Instance.MissionFailed();
    }


}
