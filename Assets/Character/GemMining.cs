using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemMining : MonoBehaviour
{

    private int TotalGems;
    private GameObject GemTerrain;
    private List<GameObject> TerrainList;
    private GameObject TerrainGemIsOn;
    public Animator animator;
    private bool IsMining;
    public Rigidbody2D rb1;
    public bool NotFrozen;

    private GameObject Drill;
    
    

    // Start is called before the first frame update
    void Start()
    {
    
    NotFrozen = true;
    IsMining = false;
    TotalGems = 0;
    Drill = transform.Find("MiningDrill").gameObject;
    }
    
    void Update()
    {
        GameManager.Instance.LevelGemNumber = TotalGems;
        //TerrainGemIsOn = collision.transform.parent.gameObject;
        if (Input.GetKeyDown(KeyCode.X)){

            Collider2D[] CollidersNearby = Physics2D.OverlapCircleAll(transform.position, 0.5f);


            foreach(Collider2D i in CollidersNearby){

            
                if (i.gameObject.tag == ("Gem")){

                    // trigger animation
                    // make character stay still while animation plays
                    animator.SetBool("IsFalling", false);
                    animator.SetBool("IsJumpingTwice", false);
                    animator.SetBool("IsGrounded", false);
                
                    animator.SetBool("IsJumping", false);
                    animator.SetBool("IsWalking", false);
                    animator.SetBool("IsMining", true);
                    Drill.SetActive(true);
                    IsMining = true;
                    TotalGems += 1;

                    NotFrozen = false;
                    //time.time
                    StartCoroutine(TimeDelay());
                    
                    
                    
                    break;


                }
            }

                

        }

        else {
                    animator.SetBool("IsMining", false);
                    IsMining = false;

                }
    }

    IEnumerator TimeDelay(){

        yield return new WaitForSeconds(3f);

        NotFrozen = true;
        Collider2D[] TerrainCollidersNearby = Physics2D.OverlapCircleAll(transform.position, 3.0f);

            foreach(Collider2D a in TerrainCollidersNearby){
                if (a.gameObject.tag == ("TerrainWithGem")){
                    Destroy(a.transform.parent.gameObject);
                    Drill.SetActive(false);

                }
            }
        
    }


    
    





}
      






