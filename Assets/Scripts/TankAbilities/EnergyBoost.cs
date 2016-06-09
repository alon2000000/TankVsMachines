using UnityEngine;
using System.Collections;

public class EnergyBoost : MonoBehaviour, ISkill
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
        get{ return _params.Get("EnergyBoostCost"); }
    }

    public bool IsReady
    {
        get{ return (_params.CashChips >= Cost); }
    }

    public float MaxActionTime{ get { return 0.0F; } }
    public float ActionTime{ get { return 0.0F; } }

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
        if (Cooldown > 0.0F)
        {
            Cooldown -= Time.deltaTime;
            return;
        }

        if (Key == KeyCode.None)
            return;
        if (!Input.GetKeyDown(Key))
            return;

        int level = Mathf.RoundToInt(_params.Get("EnergyBoostLevel"));
        if (level <= 0)
            return;

        if (!IsReady)
            return;

        _params.CashChips -= Mathf.RoundToInt(Cost);

        float newEnergyAmount = Mathf.Clamp(_params.Get("Energy") + _params.Get("EnergyBoostValue"), 0.0F, _params.Get("MaxEnergy"));

        _params.Set("Energy", newEnergyAmount);

        Cooldown = MaxCooldown;
	}
    // ======================================================================================================================================== //
}
