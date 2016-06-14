using UnityEngine;
using System.Collections;

public class EnergyBoost : Skill
{
    public override float Cost
    {
        get{ return _params.Get("EnergyBoostCost"); }
    }

    public override float Resource 
    { 
        get{ return _params.Get("Cash"); }
        set{ _params.Set("Cash", value); }
    }

    public override float MaxCooldown
    {
        get{ return _params.Get("EnergyBoostCooldown"); }
    }
    // ======================================================================================================================================== //
    protected override void beginAction()
    {
        float newEnergyAmount = Mathf.Clamp(_params.Get("Energy") + _params.Get("EnergyBoostValue"), 0.0F, _params.Get("MaxEnergy"));
        _params.Set("Energy", newEnergyAmount);
    }
    // ======================================================================================================================================== //
}
