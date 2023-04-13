using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindGeneration : MonoBehaviour
{
    
    //private List<float> WindTriggerOnLocations;
    private List<float> WindLocations;
   // private List<GameObject> WindsInstantiated;
    public GameObject R1;
    public GameObject Wind;
    //private GameObject NewWind;
    private GameObject WindInstantiated;
    //private int x;
    //public Component[] RBS;
    
    // Start is called before the first frame update
    void Start()
    {
    
        WindLocations = new List<float> {20f, 20f * 3, 20f * 6, 20f * 9, 20f * 12, 
                                            20f * 15, 20f * 18, 20f * 21, 20f * 24, 20f * 27};
   
        for(int i = 0; i < 10; i++)
        {
                WindInstantiated = Instantiate(Wind, new Vector3 (WindLocations[i], 8, 0), Wind.transform.rotation);
        }

    }


        
}




   

