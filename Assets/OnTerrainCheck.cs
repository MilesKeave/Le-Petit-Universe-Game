using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class OnTerrainCheck : MonoBehaviour
{

    public Rigidbody2D rb1;
    public Animator animator;
    public bool IsGrounded;
    public bool IsSlow;

    


   

    
    


    void OnCollisionEnter2D(Collision2D collision) 
    {
        if ((collision.gameObject.tag == "Terrain") || (collision.gameObject.tag == "TerrainWithGem") || (collision.gameObject.tag == "Gem")) {
            animator.SetBool("IsGrounded", true);
            IsGrounded = true;
            
        }



    }

    void OnCollisionStay2D(Collision2D collision) 
    {
        if ((collision.gameObject.tag == "Terrain") && (Math.Abs(rb1.velocity.x) >= 0.01f) && (Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.A)))
        {
            animator.SetBool("IsWalking", true);
        }
        else 
        {
            animator.SetBool("IsWalking", false);
        }
    }

    void OnCollisionExit2D(Collision2D collision) 
    {
        if (collision.gameObject.tag == "Terrain"|| collision.gameObject.tag == "TerrainWithGem"|| collision.gameObject.tag == "Gem") 
        {
            animator.SetBool("IsGrounded", false);
            IsGrounded = false;
        }
    }

    
}
