using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SkillState
{
    NOT_READY,
    READY,
    ACTION,
    COOLDOWN,
    FAILURE
}

public enum SkillBonus
{
    COST,
    COOLDOWN,
    ACTION_TIME,
    EFFECT_POWER
}

public abstract class Skill : MonoBehaviour 
{
    private bool _isActive = false;
    public bool IsActice
    {
        get { return _isActive; }
        set { _isActive = value; }
    }

    private SkillState _state = SkillState.NOT_READY;
    public SkillState State
    { 
        get{ return _state; } 
        set{ _state = value; } 
    }

    public List<SkillBonus> SkillBonuses = new List<SkillBonus>();
    private int _currentSkillBonusIndex = 0;
    public SkillBonus CurrentSkillBonus { get { return SkillBonuses[_currentSkillBonusIndex]; } }

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

    public abstract float BaseCost { get; }
    public float Cost 
    {
        get
        {
            float bonus = (CurrentSkillBonus != SkillBonus.COST) ? 1.0F : Mathf.Pow(0.95F, Mathf.Floor(Version));
            return BaseCost * bonus;
        }
    }

    public abstract float Resource { get; set; }

    public virtual float BaseMaxActionTime { get { return 0.0F; } } 
    public float MaxActionTime 
    {
        get
        {
            float bonus = (CurrentSkillBonus != SkillBonus.ACTION_TIME) ? 1.0F : Mathf.Pow(1.1F, Mathf.Floor(Version));
            return BaseMaxActionTime * bonus;
        }
    }

    private float _actionTime = 0.0F;
    public float ActionTime
    { 
        get { return _actionTime; } 
        set{ _actionTime = value; }
    }

    public abstract float BaseMaxCooldown { get; }
    public float MaxCooldown 
    {
        get
        {
            float bonus = (CurrentSkillBonus != SkillBonus.COOLDOWN) ? 1.0F : Mathf.Pow(0.9F, Mathf.Floor(Version));
            return BaseMaxCooldown * bonus;
        }
    }

    private float _Cooldown = 0.0F;
    public float Cooldown
    {
        get{ return _Cooldown; }
        set{ _Cooldown = value; }
    }

    public virtual float BaseEffectPower { get { return 0.0F; } }
    public float EffectPower 
    {
        get
        {
            float bonus = (CurrentSkillBonus != SkillBonus.EFFECT_POWER) ? 1.0F : Mathf.Pow(1.05F, Mathf.Floor(Version));
            return BaseEffectPower * bonus;
        }
    }

    public abstract float BaseFailureChance { get; }
    public float FailureChance{ get { return BaseFailureChance * Mathf.Pow(0.99F, (Version - 1.0F) / 0.1F); } } // 1% less chance of failure for each 0.1 version
    private float _maxFailureTime;

    //public abstract string Description { get; }
    public string Description 
    { 
        get
        {
            return this.GetType().Name+" V"+Version.ToString("F1")+"\nCost: " + Cost.ToString() + "\nAction Time: " + MaxActionTime.ToString() + "\nCooldown: " + MaxCooldown.ToString() + "\nEffect Power: " + EffectPower.ToString() + "\nFailure Chance: " + FailureChance.ToString("P1");
        } 
    }

    protected TankParams _params;
    // ======================================================================================================================================== //
	void Start () 
    {
        _params = Toolbox.Instance.TankParams;

        addBonuses();
        updateDecorationColor();
	}
    // ======================================================================================================================================== //
	void Update () 
    {
        if (!IsActice)
            return;

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
            if (Random.Range(0.0F, 1.0F) <= FailureChance) // if fail
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
    protected abstract void addBonuses();
    // ======================================================================================================================================== //
    public void ToggleSkillBonus() 
    {
        if (_currentSkillBonusIndex == SkillBonuses.Count - 1)
            _currentSkillBonusIndex = 0;
        else
            _currentSkillBonusIndex++;

        updateDecorationColor();
    }
    // ======================================================================================================================================== //
    private void updateDecorationColor()
    {
        // change color of decoration due to the cureent skill bonus
        Color decorationColor;
        if (CurrentSkillBonus == SkillBonus.COST)
            decorationColor = new Color(0.0F, 0.5F, 1.0F);
        else if (CurrentSkillBonus == SkillBonus.ACTION_TIME)
            decorationColor = Color.yellow;
        else if (CurrentSkillBonus == SkillBonus.EFFECT_POWER)
            decorationColor = Color.red;
        else if (CurrentSkillBonus == SkillBonus.COOLDOWN)
            decorationColor = Color.green;
        else
            decorationColor = Color.black;
        gameObject.GetComponent<Loot>().Decoration.GetComponent<SpriteRenderer>().color = decorationColor;
    }
    // ======================================================================================================================================== //
}
