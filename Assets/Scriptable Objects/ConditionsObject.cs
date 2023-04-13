using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "New Conditions Object", menuName = "Conditions")]

public class ConditionsObject : ScriptableObject
{
    public int TemperatureResistance;
    public int AtmosphereProtection;
    public int JetpackForce;
}
