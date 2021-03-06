﻿using UnityEngine;
using System.Collections;

public class TankParam
{
    public string Name;
    public float OriginalValue;
    public float Bonus = 0.0F;
    public float PercentBonus = 0.0F;

    // ======================================================================================================================================== //
    public TankParam(string name, float originalValue)
    {
        Name = name;
        OriginalValue = originalValue;
    }
    // ======================================================================================================================================== //
    public float Value
    {
        get { return (OriginalValue + Bonus) * (1 + PercentBonus / 100.0F); }
    }
    // ======================================================================================================================================== //
}
