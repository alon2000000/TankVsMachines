using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour, ISkill
{
    private SkillState _state = SkillState.NOT_READY;
    public SkillState State
    { 
        get{ return _state; } 
        set{ _state = value; } 
    }

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

    public float MaxActionTime{ get { return 0.0F; } }

    private float _actionTime = 0.0F;
    public float ActionTime
    { 
        get { return _actionTime; } 
        set{ _actionTime = value; }
    }

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
        if (State != SkillState.ACTION && State != SkillState.COOLDOWN)
        {
            if (_params.Get("Energy") < Cost)
                State = SkillState.NOT_READY;
            else
                State = SkillState.READY;
        }

        if (State == SkillState.NOT_READY)
            return;

        if (Input.GetKeyDown(Key) && State == SkillState.READY)
        {
            State = SkillState.COOLDOWN;
            _params.Add("Energy", -Cost);
            ActionTime = MaxActionTime;

            GameObject tankObj = GameObject.Find("Tank");
            tankObj.transform.position = new Vector3(
                Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 
                Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 
                tankObj.transform.position.z);

            Cooldown = MaxCooldown;
        }

        if (State == SkillState.COOLDOWN)
        {
            if (Cooldown > 0.0F)
            {
                Cooldown -= Time.deltaTime;
            }
            else
            {
                State = SkillState.NOT_READY;
            }
        }
	}
    // ======================================================================================================================================== //
}
