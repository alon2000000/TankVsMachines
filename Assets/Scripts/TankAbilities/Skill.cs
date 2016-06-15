using UnityEngine;
using System.Collections;

public enum SkillState
{
    NOT_READY,
    READY,
    ACTION,
    COOLDOWN,
    FAILURE
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

    private float _version = 1.0F;
    public float Version
    {
        get{ return _version; }
        set{ _version = value; }
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

    public abstract float FailChance { get; }
    private float _maxFailureTime;

    protected TankParams _params;
    // ======================================================================================================================================== //
	void Start () 
    {
        _params = Toolbox.Instance.TankParams;
	}
    // ======================================================================================================================================== //
	void Update () 
    {
        if (State != SkillState.ACTION && State != SkillState.COOLDOWN && State != SkillState.FAILURE)
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
            if (Random.Range(0.0F, 1.0F) <= FailChance) // if fail
            {
                State = SkillState.FAILURE;
                _maxFailureTime = 2.0F;
            }
            else
            {
                State = SkillState.ACTION;
                ActionTime = MaxActionTime;

                beginAction();
            }

            Resource = Resource - Cost; // pay cost also if failed
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

        if (State == SkillState.FAILURE)
        {
            if (_maxFailureTime > 0.0F)
            {
                _maxFailureTime -= Time.deltaTime;
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
