using UnityEngine;
using System.Collections;

public class TimeMaster : Skill
{
    public override float BaseCost
    {
        get{ return _params.Get("TimeMasterCost"); }
    }

    public override float Resource 
    { 
        get{ return _params.Get("Energy"); }
        set{ _params.Set("Energy", value); }
    }

    public override float BaseMaxActionTime
    { 
        get { return _params.Get("TimeMasterActionTime"); } 
    }

    public override float BaseMaxCooldown
    {
        get{ return _params.Get("TimeMasterCooldown"); }
    }

    public override float BaseFailureChance 
    { 
        get{ return _params.Get("TimeMasterFailChance"); }
    }

    /*public override string Description 
    { 
        get
        {
            return this.GetType().Name+" V"+Version.ToString()+"\nCost: " + Cost.ToString() + "\nAction Time: " + MaxActionTime.ToString() + "\nCooldown: " + MaxCooldown.ToString() + "\nEffect Power: " + EffectPower.ToString() + "\nFailure Chance: " + FailureChance.ToString();
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
        Time.timeScale /= _params.Get("TimeMasterTimeMultiplyer");
        _params.Set("TankSpeed", _params.Get("TankSpeed") * _params.Get("TimeMasterTimeMultiplyer"));
        _params.Set("TurretRotateSpeed",_params.Get("TurretRotateSpeed") * _params.Get("TimeMasterTimeMultiplyer"));
        _params.Set("TankTurnSpeed",_params.Get("TankTurnSpeed") * _params.Get("TimeMasterTimeMultiplyer"));
    }
    // ======================================================================================================================================== //
    protected override void endAction()
    {
        Time.timeScale *= _params.Get("TimeMasterTimeMultiplyer");
        _params.Set("TankSpeed", _params.Get("TankSpeed") / _params.Get("TimeMasterTimeMultiplyer"));
        _params.Set("TurretRotateSpeed",_params.Get("TurretRotateSpeed") / _params.Get("TimeMasterTimeMultiplyer"));
        _params.Set("TankTurnSpeed",_params.Get("TankTurnSpeed") / _params.Get("TimeMasterTimeMultiplyer"));
    }
    // ======================================================================================================================================== //
}
