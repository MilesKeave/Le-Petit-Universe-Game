using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InstantiateClouds : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Cloud1;
    public GameObject Cloud2;
    public GameObject Cloud3;
    public GameObject Cloud4;
    public GameObject Cloud5;
    public GameObject Cloud6;
    public GameObject Cloud7;
    public GameObject Cloud8;
    public GameObject Cloud9;

    private List<GameObject> Clouds;
    private List<GameObject> ShuffledClouds;
    private float Location;
    // Start is called before the first frame update
    void Start()
    {
        Location = 70;
        Clouds = new List<GameObject>{Cloud1, Cloud2, Cloud3, Cloud4, Cloud5, Cloud6, Cloud7, Cloud8, Cloud9 };
        ShuffledClouds = new List<GameObject>(Clouds.OrderBy( x => Random.value ).ToList( ));

        for(int i = 0; i < 2; i++)
        {
            
            GameObject temp = Instantiate(ShuffledClouds[i], new Vector3(Location, -30, 150), Quaternion.identity);
            if (i == 0)
            {
                temp.AddComponent<Shake>();
            }
            Location += 560;
        }


        
    }
}
