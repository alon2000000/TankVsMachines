using UnityEngine;
using System.Collections;

public class EnergyBoost : Skill
{
    public override float Cost
    {
        get{ return _params.Get("EnergyBoostCost"); }
    }

    public override float MaxActionTime
    { 
        get { return 0.0F; } 
    }

    public override float MaxCooldown
    {
        get{ return _params.Get("EnergyBoostCooldown"); }
    }

    public override bool IsCanPayCost
    {
        get{ return _params.CashChips >= Cost; }
    }
    // ======================================================================================================================================== //
    protected override void beginAction()
    {
        _params.CashChips -= Mathf.RoundToInt(Cost);
        float newEnergyAmount = Mathf.Clamp(_params.Get("Energy") + _params.Get("EnergyBoostValue"), 0.0F, _params.Get("MaxEnergy"));
        _params.Set("Energy", newEnergyAmount);
    }
    // ======================================================================================================================================== //
}
