using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;


public class InstantiateMountains : MonoBehaviour
{
    public GameObject Mountain1;
    public GameObject Mountain2;
    public GameObject Mountain3;

    private List<GameObject> Mountains;
    private List<GameObject> ShuffledMountains;
    private int _rInt;
    // Start is called before the first frame update
    void Start()
    {
        Mountains = new List<GameObject>{Mountain1, Mountain2, Mountain3};

        _rInt = new System.Random().Next(0, 3); 

        GameObject temp = Instantiate(Mountains[_rInt], new Vector3(275, 0, 300), Quaternion.identity);
        temp.AddComponent<Shake>();
    }

}
