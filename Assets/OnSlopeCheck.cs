using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSlopeCheck : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb1;
    public bool IsTouchingWall;
    
    void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.tag == "Terrain") {
            
            
            IsTouchingWall = true;
            
        }
    }
    void OnCollisionExit2D(Collision2D collision) 
    {
        if (collision.gameObject.tag == "Terrain") {
            
            
            IsTouchingWall = false;
            
        }
    }
}
