using UnityEngine;
using System.Collections;

public class Teleport : Skill
{
    public override float BaseCost
    {
        get{ return _params.Get("TeleportCost"); }
    }

    public override float Resource 
    { 
        get{ return _params.Get("Energy"); }
        set{ _params.Set("Energy", value); }
    }

    public override float BaseMaxCooldown
    {
        get{ return _params.Get("TeleportCooldown"); }
    }

    public override float BaseFailureChance 
    { 
        get{ return _params.Get("TeleportFailChance"); }
    }

    /*public override string Description 
    { 
        get
        {
            return "Teleport V"+Version.ToString()+"\nCost: " + Cost.ToString() + "\nAction Time: " + MaxActionTime.ToString() + "\nCooldown: " + MaxCooldown.ToString() + "\nEffect Power: " + EffectPower.ToString() + "\nFailure Chance: " + FailureChance.ToString();
        } 
    }*/
    // ======================================================================================================================================== //
    protected override void addBonuses()
    {
        SkillBonuses.Add(SkillBonus.COST);
        SkillBonuses.Add(SkillBonus.COOLDOWN);
    }
    // ======================================================================================================================================== //
    protected override void beginAction()
    {
        GameObject tankObj = GameObject.Find("Tank");
        tankObj.transform.position = new Vector3(
            Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 
            Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 
            tankObj.transform.position.z);
    }
    // ======================================================================================================================================== //
}
