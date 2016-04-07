using System.Collections;

public class TankParam
{
    public string Name;
    public float OriginalValue;
    public float Bonus = 0.0F;
    public float MaxBonus;
    public float PercentBonus = 0.0F;
    public float MaxPercentBonus;

    // ======================================================================================================================================== //
    public TankParam(string name, float originalValue, float maxBonus, float maxPercentBonus)
    {
        Name = name;
        OriginalValue = originalValue;
        MaxBonus = maxBonus;
        MaxPercentBonus = maxPercentBonus;
    }
    // ======================================================================================================================================== //
    public TankParam(string name, float bonus, float percentBonus)
    {
        Name = name;
        Bonus = bonus;
        PercentBonus = percentBonus;
    }
    // ======================================================================================================================================== //
    public float CalculatedValue
    {
        get { return (OriginalValue + Bonus) * (1 + PercentBonus / 100.0F); }
    }
    // ======================================================================================================================================== //
}
