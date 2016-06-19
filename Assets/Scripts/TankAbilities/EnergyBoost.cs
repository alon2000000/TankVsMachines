using UnityEngine;
using System.Collections;

public class EnergyBoost : Skill
{
    public override float BaseCost
    {
        get{ return _params.Get("EnergyBoostCost"); }
    }

    public override float Resource 
    { 
        get{ return _params.Get("Cash"); }
        set{ _params.Set("Cash", value); }
    }

    public override float BaseMaxCooldown
    {
        get{ return _params.Get("EnergyBoostCooldown"); }
    }

    public override float BaseFailureChance 
    { 
        get{ return _params.Get("EnergyBoostFailChance"); }
    }

    /*public override string Description 
    { 
        get
        {
            return "Energy Boost V"+Version.ToString()+"\nCost: " + Cost.ToString() + "\nAction Time: " + MaxActionTime.ToString() + "\nCooldown: " + MaxCooldown.ToString() + "\nEffect Power: " + EffectPower.ToString() + "\nFailure Chance: " + FailureChance.ToString();
        } 
    }*/
    // ======================================================================================================================================== //
    protected override void addBonuses()
    {
        SkillBonuses.Add(SkillBonus.COST);
    }
    // ======================================================================================================================================== //
    protected override void beginAction()
    {
        float newEnergyAmount = Mathf.Clamp(_params.Get("Energy") + _params.Get("EnergyBoostValue"), 0.0F, _params.Get("MaxEnergy"));
        _params.Set("Energy", newEnergyAmount);
    }
    // ======================================================================================================================================== //
}
