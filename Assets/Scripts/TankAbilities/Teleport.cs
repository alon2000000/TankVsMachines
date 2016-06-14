using UnityEngine;
using System.Collections;

public class Teleport : Skill
{
    public override float Cost
    {
        get{ return _params.Get("TeleportCost"); }
    }

    public override float Resource 
    { 
        get{ return _params.Get("Energy"); }
        set{ _params.Set("Energy", value); }
    }

    public override float MaxActionTime
    { 
        get { return 0.0F; } 
    }

    public override float MaxCooldown
    {
        get{ return _params.Get("TeleportCooldown"); }
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
