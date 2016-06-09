using UnityEngine;
using System.Collections;

public class EnergyBoost : MonoBehaviour, ISkill
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
        get{ return _params.Get("EnergyBoostCost"); }
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
        get{ return _params.Get("EnergyBoostCooldown"); }
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
            if (_params.CashChips < Cost)
                State = SkillState.NOT_READY;
            else
                State = SkillState.READY;
        }

        if (State == SkillState.NOT_READY)
            return;

        if (Input.GetKeyDown(Key) && State == SkillState.READY)
        {
            State = SkillState.COOLDOWN;
            _params.CashChips -= Mathf.RoundToInt(Cost);
            ActionTime = MaxActionTime;

            float newEnergyAmount = Mathf.Clamp(_params.Get("Energy") + _params.Get("EnergyBoostValue"), 0.0F, _params.Get("MaxEnergy"));
            _params.Set("Energy", newEnergyAmount);

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
