using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    private Animator animator;
    private float timer = 0;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        timer = 3;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 3)
        {
            timer = 0;
            animator.Play("Die", 0, 0.1f);
        }
     
    }
}
