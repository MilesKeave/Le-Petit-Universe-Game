using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelUpgradesInformation
{
    public LevelDefine levelDefine;
    public int AtmospherePoint = 0;
    public int ThermalPoint = 0;
    public int JetpackPoint = 0;

    public int RightAtmospherePoint = 0;
    public int RightThermalPoint = 0;
    public int RightJetpackPoint = 0;

    [TextArea]
    public string AtmosphereGuideString = "";
    [TextArea]
    public string ThermalGuideString = "";
    [TextArea]
    public string JetpackGuideString = "";
    [Space]
    [Space]
    [Space]
    [Space]
    [TextArea]
    public string AtmosphereInfoTitleString = "";
    [TextArea]
    public string ThermalInfoTitleString = "";
    [TextArea]
    public string JetpackInfoTitleString = "";
    [Space]
    [Space]
    [Space]
    [Space]
    [TextArea]
    public string AtmosphereInfoString = "";
    [TextArea]
    public string ThermalInfoString = "";
    [TextArea]
    public string JetpackInfoString = "";
}
