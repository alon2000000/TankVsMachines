using UnityEngine;
using System.Collections;

public class TimeMaster : Skill
{
    public override float Cost
    {
        get{ return _params.Get("TimeMasterCost")/* * Mathf.Pow(0.97F, Mathf.Floor(Version))*/; }
    }

    public override float Resource 
    { 
        get{ return _params.Get("Energy"); }
        set{ _params.Set("Energy", value); }
    }

    public override float MaxActionTime
    { 
        get { return _params.Get("TimeMasterActionTime"); } 
    }

    public override float MaxCooldown
    {
        get{ return _params.Get("TimeMasterCooldown"); }
    }

    public override float FailChance 
    { 
        get{ return _params.Get("TimeMasterFailChance"); }
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
