using UnityEngine;
using System.Collections;

public class Turbo : Skill
{
    public override float Cost
    {
        get{ return _params.Get("TurboCost"); }
    }

    public override float MaxActionTime
    { 
        get { return _params.Get("TurboActionTime"); } 
    }

    public override float MaxCooldown
    {
        get{ return _params.Get("TurboCooldown"); }
    }

    public override bool IsCanPayCost
    {
        get{ return _params.Get("Energy") >= Cost; }
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
