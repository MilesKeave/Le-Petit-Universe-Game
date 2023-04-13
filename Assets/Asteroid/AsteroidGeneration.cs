using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidGeneration : MonoBehaviour
{
    
    private List<float> AsteroidTriggerOnLocations;
    private List<float> AsteroidLocations;
    private List<GameObject> AsteroidsInstantiated;
    public GameObject R1;
    public GameObject Asteroid;
    private GameObject NewAsteroid;
    private GameObject AsteroidInstantiated;
    private int x;
    public Component[] RBS;
    
    // Start is called before the first frame update
    void Start()
    {
    AsteroidsInstantiated = new List<GameObject> {};
    AsteroidTriggerOnLocations = new List<float> {};
    AsteroidLocations = new List<float> {30f, 30f * 3, 30f * 6, 30f * 9, 30f * 12, 
                                        30f * 15};

    for(int i = 0; i < 6; i++)
    {
            AsteroidTriggerOnLocations.Add(AsteroidLocations[i] - 25f);      
    }

    for(int i = 0; i < 6; i++)
    {
            AsteroidInstantiated = Instantiate(Asteroid, new Vector3 (AsteroidLocations[i], 10, 0), Asteroid.transform.rotation);
            AsteroidsInstantiated.Add(AsteroidInstantiated);      
    }

    x = 0;

    }

    // Update is called once per frame
    void Update()
    {
       
        if (x < 5)
        {
            if (R1.transform.position.x >= AsteroidTriggerOnLocations[x]) 
            {
                    NewAsteroid = AsteroidsInstantiated[x];
                    RBS = NewAsteroid.GetComponentsInChildren<Rigidbody2D>();
                    
                    foreach (Rigidbody2D rb in RBS){
                        rb.velocity = new Vector2(-5,-5);
                    }
                    

                    x += 1;

            }
        }
            
        
           
        



            
        
    }
        
}




   

