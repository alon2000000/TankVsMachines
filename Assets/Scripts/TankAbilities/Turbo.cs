using UnityEngine;
using System.Collections;

public class Turbo : Skill
{
    public override float BaseCost
    {
        get{ return _params.Get("TurboCost"); }
    }

    public override float Resource 
    { 
        get{ return _params.Get("Energy"); }
        set{ _params.Set("Energy", value); }
    }

    public override float BaseMaxActionTime
    { 
        get { return _params.Get("TurboActionTime"); } 
    }

    public override float BaseMaxCooldown
    {
        get{ return _params.Get("TurboCooldown"); }
    }

    public override float BaseFailureChance 
    { 
        get{ return _params.Get("TurboFailChance"); }
    }

    /*public override string Description 
    { 
        get
        {
            return "Turbo V"+Version.ToString()+"\nCost: " + Cost.ToString() + "\nAction Time: " + MaxActionTime.ToString() + "\nCooldown: " + MaxCooldown.ToString() + "\nEffect Power: " + EffectPower.ToString() + "\nFailure Chance: " + FailureChance.ToString();
        } 
    }*/
    // ======================================================================================================================================== //
    protected override void addBonuses()
    {
        SkillBonuses.Add(SkillBonus.COST);
        SkillBonuses.Add(SkillBonus.ACTION_TIME);
        SkillBonuses.Add(SkillBonus.COOLDOWN);
        SkillBonuses.Add(SkillBonus.EFFECT_POWER);
    }
    // ======================================================================================================================================== //
    protected override void beginAction()
    {
        _params.Set("TankSpeed", _params.Get("TankSpeed") * _params.Get("TurboSpeedMultiplyer"));
    }
    // ======================================================================================================================================== //
    protected override void endAction()
    {
        _params.Set("TankSpeed", _params.Get("TankSpeed") / _params.Get("TurboSpeedMultiplyer"));
    }
    // ======================================================================================================================================== //
}
