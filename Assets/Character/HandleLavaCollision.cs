using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleLavaCollision : MonoBehaviour
{
    private AudioSource DeathSound;
    public bool Dead;
    public Animator animator;
    void Start()
    {
        Dead = false;
        animator.SetBool("IsDying", false);

        DeathSound = GetComponent<AudioSource>();
        
        //make grab dead in Chara termovement script update and check if Dead before any movement allowed
    }

    // Update is called once per frame
    void Update()
    {
        if (!Dead && transform.position.y <= -10)
        {
            Dead = true;
            animator.SetBool("IsDying", true);
            DeathSound.Play();
            StartCoroutine(DelayGameOver());
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Lava")
        {
            Debug.Log("You are dead! Stop playing please!");
            Dead = true;
            animator.SetBool("IsDying", true);
            DeathSound.Play();
            StartCoroutine(DelayGameOver());
        }
    }


    private IEnumerator DelayGameOver()
    {
        yield return new WaitForSeconds(3.0f);
        LevelUIManager.Instance.MissionFailed();
    }
    
}
