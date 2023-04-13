using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpSpace : MonoBehaviour
{
    private Animator animator;
    private float timer = 0;
    private int index = 0;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 3)
        {
            timer = 0;
            index++;
            if (index >= 2)
            {
                index = 0;
            }
        }
        animator.SetInteger("id", index);
    }
}
