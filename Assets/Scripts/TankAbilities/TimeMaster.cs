using UnityEngine;
using System.Collections;

public class TimeMaster : MonoBehaviour, ISkill
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
        get{ return _params.Get("TimeMasterCost"); }
    }

    public bool IsReady
    {
        get{ return (_params.Get("Energy") >= Cost); }
    }

    public float MaxActionTime{ get { return _params.Get("TimeMasterActionTime"); } }

    private float _actionTime = 0.0F;
    public float ActionTime
    { 
        get { return _actionTime; } 
        set{ _actionTime = value; }
    }

    public float MaxCooldown
    {
        get{ return _params.Get("TimeMasterCooldown"); }
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
            State = SkillState.ACTION;
            _params.Add("Energy", -Cost);
            ActionTime = MaxActionTime;

            Time.timeScale /= _params.Get("TimeMasterTimeMultiplyer");
            _params.Set("TankSpeed", _params.Get("TankSpeed") * _params.Get("TimeMasterTimeMultiplyer"));
            _params.Set("TurretRotateSpeed",_params.Get("TurretRotateSpeed") * _params.Get("TimeMasterTimeMultiplyer"));
            _params.Set("TankTurnSpeed",_params.Get("TankTurnSpeed") * _params.Get("TimeMasterTimeMultiplyer"));
        }

        if (State == SkillState.ACTION)
        {
            if (ActionTime > 0.0F)
            {
                ActionTime -= Time.deltaTime;
            }
            else
            {
                State = SkillState.COOLDOWN;
                Cooldown = MaxCooldown;

                Time.timeScale *= _params.Get("TimeMasterTimeMultiplyer");
                _params.Set("TankSpeed", _params.Get("TankSpeed") / _params.Get("TimeMasterTimeMultiplyer"));
                _params.Set("TurretRotateSpeed",_params.Get("TurretRotateSpeed") / _params.Get("TimeMasterTimeMultiplyer"));
                _params.Set("TankTurnSpeed",_params.Get("TankTurnSpeed") / _params.Get("TimeMasterTimeMultiplyer"));
            }
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
