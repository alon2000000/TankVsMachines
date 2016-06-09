using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour, ISkill
{
    public SkillState State{ get; set; }

    private KeyCode _key = KeyCode.None;
    public KeyCode Key
    {
        get{ return _key; }
        set{ _key = value; }
    }

    public float Cost
    {
        get{ return _params.Get("TeleportCost"); }
    }

    public bool IsReady
    {
        get{ return (_params.Get("Energy") >= Cost); }
    }

    public float MaxActionTime{ get { return 0.0F; } }
    public float ActionTime{ get { return 0.0F; } }

    public float MaxCooldown
    {
        get{ return _params.Get("TeleportCooldown"); }
    }

    private float _Cooldown = 0.0F;
    public float Cooldown
    {
        get{ return _Cooldown; }
        set{ _Cooldown = value; }
    }

    private TankParams _params;
    // ======================================================================================================================================== //
	void Start () 
    {
        _params = Toolbox.Instance.TankParams;
	}
    // ======================================================================================================================================== //
	void Update () 
    {
        if (Cooldown > 0.0F)
        {
            Cooldown -= Time.deltaTime;
            return;
        }

        if (Key == KeyCode.None)
            return;
        if (!Input.GetKeyDown(Key))
            return;

        int level = Mathf.RoundToInt(_params.Get("TeleportLevel"));
        if (level <= 0)
            return;

        if (!IsReady)
            return;

        GameObject tankObj = GameObject.Find("Tank");
        tankObj.transform.position = new Vector3(
            Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 
            Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 
            tankObj.transform.position.z);

        _params.Add("Energy", -Cost);

        Cooldown = MaxCooldown;
	}
    // ======================================================================================================================================== //
}
