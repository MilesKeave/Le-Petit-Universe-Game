using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterMovement : MonoBehaviour
{  

    private bool CharacterSticks;
    private bool CanGoRight;
    private bool CanGoLeft;
    private bool FacingBack;
    private bool TooSteep;
    private bool IsHovering;
    private bool IsSlow;
    private bool Dead;
    private bool CanMove;
    private bool AsteroidDead;
    private bool WeakJetpack;
    private bool NoProtection;

    private Rigidbody2D rb2d;

    private float AngleRight;
    private float AngleLeft;

    private ParticleSystem JetpackHovering;
    private ParticleSystem JetpackJumping;

    private RaycastHit2D HitInfoDownLeft;
    private RaycastHit2D HitInfoDownRight;

    private RaycastHit2D HitInfoLeft;
    private RaycastHit2D HitInfoRight;

    private RigidbodyConstraints2D OriginalConstraints;
    private float HoverTime;
    private bool Hovering;

    const string IDLE = "Idle";
    const string WALK = "Walk";
    const string TAKE_OFF = "Take Off";
    const string IN_AIR = "In Air";
    const string LAND = "Land";
    const string LAVA_DEATH = "Lava Death";
    const string ASTEROID_DEATH = "Asteroid Death";

    public float JumpVelocity;
    public float FailedJumpVelocity;

    public float LevelNoDistFromGround;

    public bool Venus;



    public AudioSource JPSound;




    void GoRight()
    {
        float speed = 4f;
        Debug.Log(IsSlow);
        if(IsSlow)
        {
            speed = 1.5f;
            
        }
        Vector2 TempVelocity = rb2d.velocity;
        TempVelocity.x = speed;
        rb2d.velocity = TempVelocity;
    }
    

    void GoLeft()
    {
        float speed = 4f;
        if(IsSlow)
        {
            speed = 1.5f;
            
        }
        Vector2 TempVelocity = rb2d.velocity;
        TempVelocity.x = -(speed);
        rb2d.velocity = TempVelocity;
    }

    void TurnAround()
    {
        Quaternion TempRotation = transform.rotation;
        TempRotation.y *= -1;
        transform.rotation = TempRotation;
    }




    void Stick(float DistFromGround)
    {
        /*
        float TempYPosition = transform.position.y;
        Vector3 TempPosition = transform.position;
        TempPosition.y = TempYPosition - DistFromGround;
        transform.position = TempPosition;
        */

        rb2d.AddForce(new Vector2(0f,-500f), ForceMode2D.Impulse);
    }

    void Jump()
    {
        float speed = JumpVelocity;
        if(WeakJetpack)
        {
            speed = FailedJumpVelocity;
        }
        StartCoroutine(PlayJetpackEffectFor(.3f));
        Vector2 _tempVelocity = rb2d.velocity;
        _tempVelocity.y = speed;
        rb2d.velocity = _tempVelocity;
    }

    IEnumerator PlayJetpackEffectFor(float JumpDuration)
    {
        JetpackJumping.Play();
        yield return new WaitForSeconds(JumpDuration);
        JetpackJumping.Stop();
    }

    void CheckIfCharacterSticks()
    {
        


        float RaycastSourceLeftY = transform.Find("RaycastLocationLeft").transform.position.y;
        float RaycastSourceLeftX = transform.Find("RaycastLocationLeft").transform.position.x;
        Vector2 RaycastSourceLeft = new Vector2(RaycastSourceLeftX, RaycastSourceLeftY);

        RaycastHit2D[] HitInfosDownLeft = Physics2D.RaycastAll(RaycastSourceLeft, Vector2.down);
        

        foreach(RaycastHit2D i in HitInfosDownLeft)
        {
            if (i.collider.tag == "Terrain" || i.collider.tag == "TerrainWithGem")
            {
                HitInfoDownLeft = i;
                AngleLeft = Mathf.Atan2(HitInfoDownLeft.normal.x, HitInfoDownLeft.normal.y )*Mathf.Rad2Deg;
                break;
            }
        }

        Debug.DrawRay(RaycastSourceLeft, Vector3.down*HitInfoDownLeft.distance, Color.green);

        float RaycastSourceRightY = transform.Find("RaycastLocationRight").transform.position.y;
        float RaycastSourceRightX = transform.Find("RaycastLocationRight").transform.position.x;
        Vector2 RaycastSourceRight = new Vector2(RaycastSourceRightX, RaycastSourceRightY);

        RaycastHit2D[] HitInfosDownRight = Physics2D.RaycastAll(RaycastSourceRight, Vector2.down);

        foreach(RaycastHit2D a in HitInfosDownRight)
        {
            if (a.collider.tag == "Terrain"||a.collider.tag == "TerrainWithGem")
            {
                HitInfoDownRight = a;
                AngleRight = Mathf.Atan2(HitInfoDownRight.normal.x,HitInfoDownRight.normal.y )*Mathf.Rad2Deg;
                break;
            }
        }

        Debug.DrawRay(RaycastSourceRight, Vector3.down*HitInfoDownRight.distance, Color.blue);

        bool TooSteepRight = (AngleRight > 70f || AngleRight < -70f);
        bool TooSteepLeft = (AngleLeft < -70f || AngleLeft > 70f);

        TooSteep = (AngleRight > 70f && AngleLeft > 70f) || (AngleRight< -70f && AngleLeft < -70f);

        float LeftDistFromGround = HitInfoDownLeft.distance;
        float RightDistFromGround = HitInfoDownRight.distance;
        float NoDistFromGround = LevelNoDistFromGround;
        //1.7f

        //Debug.Log((LeftDistFromGround,RightDistFromGround));
        
       
       /*
        else if ((!TooSteepRight || !TooSteepLeft) && (LeftDistFromGround <= NoDistFromGround || RightDistFromGround <= NoDistFromGround))
        {
            CharacterSticks = true;
        }
        */
        
/*
        else if (LeftDistFromGround > NoDistFromGround && !(RightDistFromGround > NoDistFromGround)|| !(LeftDistFromGround > NoDistFromGround) && (RightDistFromGround > NoDistFromGround)){

            CharacterSticks = true;
        }
        */
        
        if (LeftDistFromGround > NoDistFromGround && RightDistFromGround > NoDistFromGround)
        {
            /*
            if(LeftDistFromGround>NoDistFromGround && !TooSteepRight && RightDistFromGround<= NoDistFromGround) 
            {
                CharacterSticks = true;
            }
            else if(RightDistFromGround>NoDistFromGround && !TooSteepLeft && LeftDistFromGround <= NoDistFromGround) 
            {
                CharacterSticks = true;
            }
            else
            {
                CharacterSticks=false;
            }
            */
            
            CharacterSticks = false;
            //Debug.Log((LeftDistFromGround, NoDistFromGround));
            //Debug.Log((RightDistFromGround, NoDistFromGround));
            
        }
        else if (TooSteep)
        {
            
            CharacterSticks = false;
            HoverTime = 0f;
        }
        



        else
        {
            CharacterSticks = true;
            float MinDistFromGround = 0f;

            if (LeftDistFromGround < RightDistFromGround)
            {
                MinDistFromGround = LeftDistFromGround -NoDistFromGround;
            }
            
            else
            {
                MinDistFromGround = RightDistFromGround - NoDistFromGround;
            }
            if (Venus){
            Stick(MinDistFromGround);
            }
            HoverTime = 0f;
        }







        // if both raycasters are giving "too steep" in the same slope, CharacterSticks = false

        // elif either of the raycaster is giving a distance greater than threshold, CharacterSticks = false

        // else CharacterSticks = true, also, HoverReset = true
    }


    void UpdateMovementRestrictions()
    {
        
        float RaycastSourceY = transform.Find("RaycastLocationMiddle").transform.position.y;
        float RaycastSourceX = transform.Find("RaycastLocationMiddle").transform.position.x;
        Vector2 RaycastSource = new Vector2(RaycastSourceX, RaycastSourceY);

        RaycastHit2D[] HitInfosRight = Physics2D.RaycastAll(RaycastSource, Vector2.right);
        RaycastHit2D[] HitInfosLeft = Physics2D.RaycastAll(RaycastSource, Vector2.left);

        foreach(RaycastHit2D j in HitInfosRight)
        {
            if (j.collider.tag == "Terrain"|| j.collider.tag == "TerrainWithGem")
            {
                HitInfoRight = j;
                
                break;
            }
        }

        foreach(RaycastHit2D k in HitInfosLeft)
        {
            if (k.collider.tag == "Terrain"||k.collider.tag == "TerrainWithGem")
            {
                HitInfoLeft = k;
               
                break;
            }
        }

        Debug.DrawRay(RaycastSource, Vector2.right, Color.red);
        Debug.DrawRay(RaycastSource, Vector2.left, Color.red);

        if (TooSteep)
        {
            if (HitInfoLeft.distance > HitInfoRight.distance)
            {
                CanGoLeft = true;
                CanGoRight = false;
            }
            else
            {
                CanGoLeft = false;
                CanGoRight = true;
            }
        }

        else
        {
            float MinDistToWall = 0.4f;
            if (HitInfoLeft.distance <= MinDistToWall)
            {
                CanGoLeft = false;
            }
            else
            {
                CanGoLeft = true;
            }

            if (HitInfoRight.distance <= MinDistToWall)
            {
                CanGoRight = false;
            }
            else
            {
                CanGoRight = true;
            }

        }
            
    }



    void Start()
    {
        FacingBack = false;

        WeakJetpack = GameObject.Find("ConditionChecker").GetComponent<CheckCondition>().WeakJetpack;
        NoProtection = GameObject.Find("ConditionChecker").GetComponent<CheckCondition>().NoProtection;
        
        rb2d = GetComponent<Rigidbody2D>();
        OriginalConstraints = rb2d.constraints;
        rb2d.isKinematic = false;
        HoverTime = 0f;
        Hovering = false;
        
        GameObject temp = gameObject.transform.Find("polySurface130_lambert12_0").gameObject;
        JetpackHovering = temp.transform.Find("Hovering_Jetpack").GetComponent<ParticleSystem>();
        JetpackJumping = temp.transform.Find("Jumping_Jetpack").GetComponent<ParticleSystem>();
        JetpackHovering.Stop();
        JetpackJumping.Stop();
        
    }


    void Update()
    {
        CheckIfCharacterSticks();

        
        
        IsSlow = false;
        if (Venus)
        {
            GameObject[] Terrains = GameObject.FindGameObjectsWithTag("Terrain");

            foreach (GameObject terrain in Terrains)
            {
        
                if (terrain.GetComponentInChildren<ParticleCollision>())
                {
                    if (terrain.GetComponentInChildren<ParticleCollision>().IsSlow)
                    {

                        IsSlow=true;
                        break;
                    }
                }
            }
        }
        else 
        {
            GameObject[] Winds = GameObject.FindGameObjectsWithTag("Wind");

            foreach (GameObject wind in Winds)
            {
                if (wind.GetComponentInChildren<ParticleCollision>().IsSlow)
                {
                    IsSlow = true;
                    break;
                }
            }   
        }
        

        //HANDLE JUMPING//
        Dead = GameObject.Find("Jump Anima").GetComponentInChildren<HandleLavaCollision>().Dead;
        AsteroidDead = GameObject.Find("Jump Anima").GetComponentInChildren<HandleAsteroidCollision>().AsteroidDead;
        CanMove = GameObject.Find("Jump Anima").GetComponent<GemMining>().NotFrozen;
        
        if (NoProtection)
        {
            rb2d.constraints = ~RigidbodyConstraints2D.FreezeAll;            
        }
        else if (CanMove == true && !Dead && !AsteroidDead)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {   
                Debug.Log(TooSteep);
                if (CharacterSticks)
                {
                    Jump();

                }
            }
            



            //HANDLE HOVERING//


            if (Input.GetKey(KeyCode.Space))
            {
            
                if (!CharacterSticks && HoverTime < 4f)
                {
                    rb2d.constraints = ~RigidbodyConstraints2D.FreezePositionX;
                    HoverTime += 0.017f;
                    JPSound.mute = false;
                    JetpackHovering.Play();
                    Hovering = true;

                          
                }
                else
                {
                    rb2d.constraints = OriginalConstraints;
                    rb2d.WakeUp();
                    JPSound.mute = true;
                    JetpackHovering.Stop();
                    Hovering = false;

                }
            }
            else
            {
                rb2d.constraints = OriginalConstraints;
                rb2d.WakeUp();
                JPSound.mute = true;
                JetpackHovering.Stop();
                Hovering = false;

            }


            //HANDLE RIGHT AND LEFT MOVEMENT//

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                UpdateMovementRestrictions();
                if (CanGoRight)
                {
                    GoRight();

                    if (FacingBack)
                    {
                        TurnAround();
                        FacingBack = false;
                    }
                }
            }
            else if ((CharacterSticks || Hovering) && !(Input.GetKey(KeyCode.A)) && !(Input.GetKey(KeyCode.LeftArrow)))
            {
                Vector2 TempVelocity = rb2d.velocity;
                TempVelocity.x = 0;
                rb2d.velocity = TempVelocity;
            }




            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                UpdateMovementRestrictions();
                if (CanGoLeft)
                {
                    GoLeft();

                    if (!FacingBack)
                    {
                        TurnAround();
                        FacingBack = true;
                    }
                }
            }
            else if ((CharacterSticks || Hovering) && !(Input.GetKey(KeyCode.D)) && !(Input.GetKey(KeyCode.RightArrow)))
            {
                Vector2 TempVelocity = rb2d.velocity;
                TempVelocity.x = 0;
                rb2d.velocity = TempVelocity;
            }
        }  
    }





































































































































































    
     /*
    public Rigidbody2D rb1;
    private Vector3 _tempVelocity;
    [SerializeField] private bool JumpedOnce;
    [SerializeField] private bool JumpedTwice;
    private float TimeSinceFirstJump;
    private bool IsJumping;
    private bool IsFalling;
    private bool IsGrounded;
    private bool IsWalking;
    private Animator animator;

    private Quaternion _tempRotation;
    private bool _facingBack;
    private ParticleSystem JetsOn;
    private float PositionNow;
    private float PositionOnJump;
    private bool CanMove;

    private bool isTooSteep;
    private bool isSlopeOnRight;
    private bool isGrounded;
    private bool Dead;
    private bool CanJumpTwice;
    private bool AsteroidDead;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        JumpedOnce = false; 
        JumpedTwice = false;
        
        _facingBack = false;
        JetsOn = GetComponentInChildren<ParticleSystem>();
        JetsOn.Stop();
        PositionNow = -100;
        CanMove = true;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*
        //movement script below
        PositionNow = gameObject.transform.position.y;


        if (!JumpedOnce) 
        {
            rb1.AddForce(new Vector2(0f,-500f), ForceMode2D.Impulse);
        }

        
        
        _tempVelocity = rb1.velocity;

        Dead = GameObject.Find("Jump Anima").GetComponentInChildren<HandleLavaCollision>().Dead;
        AsteroidDead = GameObject.Find("Jump Anima").GetComponentInChildren<HandleAsteroidCollision>().AsteroidDead;
        CanMove = GameObject.Find("Jump Anima").GetComponent<GemMining>().NotFrozen;
        isTooSteep = GameObject.Find("Jump Anima").GetComponentInChildren<RaycastInformation>().isTooSteep;
        isSlopeOnRight = GameObject.Find("Jump Anima").GetComponentInChildren<RaycastInformation>().isSlopeOnRight;
        isGrounded = GameObject.Find("Jump Anima").GetComponentInChildren<OnTerrainCheck>().IsGrounded;
        Dead = GameObject.Find("Jump Anima").GetComponentInChildren<HandleLavaCollision>().Dead;
        AsteroidDead = GameObject.Find("Jump Anima").GetComponentInChildren<HandleAsteroidCollision>().AsteroidDead;
        

        if (CanMove == true && !Dead && !AsteroidDead){
            if ((PositionNow != -100 )&&(PositionNow - PositionOnJump >= 1)){

                JetsOn.Stop();

            }

            if (Input.GetKey(KeyCode.W)) 
        
            {
                Debug.Log("JumpedOnce");
                Debug.Log(JumpedOnce);
                Debug.Log("JumpedTwice");
                Debug.Log(JumpedTwice);
                if (isGrounded && !isTooSteep)
                {
                    PositionOnJump = gameObject.transform.position.y;

                    _tempVelocity = rb1.velocity;
                    _tempVelocity.y = 7;
                    rb1.velocity = _tempVelocity;
                    JumpedOnce = true;
                    JumpedTwice = false;
                    CanJumpTwice= false;

                    animator.SetBool("IsJumping", true);
                    StartCoroutine(TimeDelay2());
                    
                    JetsOn.Play();
                }
                else if (!isGrounded && JumpedOnce && !JumpedTwice && CanJumpTwice)
                {
                    PositionOnJump = gameObject.transform.position.y;
                    Debug.Log("Should have jumped twice");
                    _tempVelocity = rb1.velocity;
                    _tempVelocity.y = 7;
                    rb1.velocity = _tempVelocity;
                    JumpedTwice = true;

                    animator.SetBool("IsJumping", true);
                    
                    JetsOn.Play();
                }

            /*discuss this with enes!! not sure we need it...
                else if (!IsGrounded && !JumpedOnce && !JumpedTwice){

                    PositionOnJump = gameObject.transform.position.y;
                    JumpedOnce = true;
                    _tempVelocity.y = 7;
                    rb1.velocity = _tempVelocity;
                    JumpedOnce = true;
                    JumpedTwice = true;
                    CanJumpTwice= true;
                    animator.SetBool("IsJumping", true);
                    JetsOn.Play();
                    
                    


                }
            

            }




            if (Input.GetKeyDown(KeyCode.S))
            {
                if (JumpedOnce == true)
                {
                    _tempVelocity = rb1.velocity;
                    _tempVelocity.y = -5;
                    rb1.velocity = _tempVelocity;     
                }           
            }


            
            if (Input.GetKey(KeyCode.D)) 
            {
                Debug.Log("Is it too steep?");
                Debug.Log(isTooSteep);
                Debug.Log("Is slope on right?");
                Debug.Log(isSlopeOnRight);
                if (!isTooSteep || (isTooSteep && !isSlopeOnRight))
                {
                    _tempVelocity = rb1.velocity;
                    _tempVelocity.x = 4;
                    rb1.velocity = _tempVelocity;

                    if (_facingBack)
                    {
                            _tempRotation = transform.rotation;
                            _tempRotation.y *= -1;
                            transform.rotation = _tempRotation;
                            _facingBack = false;
                    } 
                }
            }

            else if ((JumpedOnce == false) && (Input.GetKey(KeyCode.A) == false)) 
            {    
                _tempVelocity = rb1.velocity;
                _tempVelocity.x = 0;
                rb1.velocity = _tempVelocity;
            }


            if (Input.GetKey(KeyCode.A)) 
            {
                if (!isTooSteep || (isTooSteep && isSlopeOnRight))
                {
                    _tempVelocity = rb1.velocity;
                    _tempVelocity.x = -4;
                    rb1.velocity = _tempVelocity;
                
                    if (!(_facingBack))
                    {
                            _tempRotation = transform.rotation;
                            _tempRotation.y *= -1;
                            transform.rotation = _tempRotation;
                            _facingBack = true;
                    } 
                }
                
                
            }

            else if ((JumpedOnce == false) && (Input.GetKey(KeyCode.D) == false)) 
            {
                _tempVelocity = rb1.velocity;
                _tempVelocity.x = 0;
                rb1.velocity = _tempVelocity;
                
            }


        }
            
        else 
        {
            rb1.velocity = new Vector2(0, 0);

            
            StartCoroutine(TimeDelay3());
        }
            
    }

    void OnCollisionEnter2D(Collision2D collision) {
        JumpedOnce = false;
        JumpedTwice = false;
        animator.SetBool("IsJumping", false);
        animator.SetBool("IsJumpingTwice", false);
    }





    IEnumerator TimeDelay2(){

        yield return new WaitForSeconds(0.3f);
        CanJumpTwice = true;

    }
    IEnumerator TimeDelay3(){

        yield return new WaitForSeconds(2f);
        animator.SetBool("AsteroidDead", false);

    }

    */
}

