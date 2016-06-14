using UnityEngine;
using System.Collections;

public enum SkillState
{
    NOT_READY,
    READY,
    ACTION,
    COOLDOWN
}

public abstract class Skill : MonoBehaviour 
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

    public abstract float Cost { get; }
    public abstract float Resource { get; set; }

    public virtual float MaxActionTime { get { return 0.0F; } } 

    private float _actionTime = 0.0F;
    public float ActionTime
    { 
        get { return _actionTime; } 
        set{ _actionTime = value; }
    }

    public abstract float MaxCooldown { get; }

    private float _Cooldown = 0.0F;
    public float Cooldown
    {
        get{ return _Cooldown; }
        set{ _Cooldown = value; }
    }

    protected TankParams _params;
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
            if (Cost <= Resource)
                State = SkillState.READY;
            else
                State = SkillState.NOT_READY;
        }

        if (State == SkillState.NOT_READY)
            return;

        if (Input.GetKeyDown(Key) && State == SkillState.READY)
        {
            State = SkillState.ACTION;
            Resource = Resource - Cost;
            ActionTime = MaxActionTime;

            beginAction();
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

                endAction();
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
    protected virtual void beginAction(){}
    protected virtual void endAction(){}
    // ======================================================================================================================================== //
}
