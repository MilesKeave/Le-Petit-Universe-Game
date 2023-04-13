using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    private bool NoProtection;
    private bool MoveBack;
    private Vector3 OriginalPosition;

    void Start()
    {
        OriginalPosition = transform.position;
        MoveBack = false;
        NoProtection = GameObject.Find("ConditionChecker").GetComponent<CheckCondition>().NoProtection;
    }

    void FixedUpdate()
    {

        if (NoProtection)
        {
            if (!MoveBack)
            {
                transform.position = new Vector3(OriginalPosition.x + 0.1f, (OriginalPosition.y * 1.5f) - 0.5f, OriginalPosition.z);
                MoveBack = true;
            }
            else 
            {
                transform.position = new Vector3(OriginalPosition.x, OriginalPosition.y, OriginalPosition.z);
                MoveBack = false;
            }
        }
    }   
}
