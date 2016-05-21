using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Loot : MonoBehaviour 
{
    public enum LootState
    {
        ON_GROUND,
        INSIDE_BAG,
        ATTACHED
    }

    public enum LootType
    {
        BURNT,
        TURRET_CHIP,
        BARREL_CHIP,
        SHELL_CHIP,
        ENGINE_CHIP,
        TELEPORT_CHIP,
        SHIELD_CHIP
    }

    public enum LootRarity
    {
        NORMAL = 1,                 // gray
        SPECIAL = 3,                // silver glow
        RARE = 5,                   // glow gold
        EXTREMELY_RARE = 10,          // glow color
        UNIQUE                  // blinked random colors r/g/b
    }

    public List<TankParamReward> Rewards = new List<TankParamReward>();

    public LootState State;
    public LootType Type;
    public LootRarity Rarity;

    public Sprite GroundTexture;
    public Sprite BodyTexture;
    public Sprite FrameTexture;
    public Sprite SkillTexture;

    // colors
    public Color SilverStartColor;
    public Color SilverEndColor;
    public Color GoldStartColor;
    public Color GoldEndColor;
    public Color GreenStartColor;
    public Color GreenEndColor;

    public Gradient UniqueGradient;

    // skills sprites
    public Sprite RepairTexture;
    public Sprite TurretTexture;
    public Sprite ShellTexture;
    public Sprite TeleportTexture;
    public Sprite ShieldTexture;

    // the parts of the chip
    public GameObject Body;
    public GameObject Frame;
    public GameObject Logo;

    // chances
    public float BurntChance = 75.0F;
    public float SpecialChance = 20.0F;
    public float RareChance = 4.0F;
    public float ExtremelyRareChance = 0.7F;
    public float UniqueChance = 0.2F;
    // ======================================================================================================================================== //
	void Start () 
	{
        // set init state to be on ground
        State = LootState.ON_GROUND;
        // rotate randomly on ground
        this.transform.RotateAround (this.transform.position, this.transform.forward, Random.Range(0, 360)); 

        // burnt chance
        bool isBurnt = setBurnt();
        if (isBurnt)
            return;

        // rarity
        setLootRarity();

        // due to rarity make the chip body & reward
        if (Rarity == LootRarity.UNIQUE)
        {
            setChipUnique();
        }
        else
        {
            setLootSize();
            setLootType();
            setRewards();
        }
	}
    // ======================================================================================================================================== //
    void Update () 
	{
        updateColor();
	}
    // ======================================================================================================================================== //
    private bool setBurnt()
    {
        float rand = Random.Range(0.0F, 100.0F);
        if (rand <= BurntChance)
        {
            Type = LootType.BURNT;
            return true;
        }
        return false;
    }
    // ======================================================================================================================================== //
    private void setLootRarity()
    {
        float rand = Random.Range(0.0F, 100.0F);


        if (rand <= SpecialChance)
        {
            //Debug.Log("SPECIAL CHIP");
            Rarity = LootRarity.SPECIAL;
        }
        else if (rand <= SpecialChance + RareChance)
        {
            //Debug.Log("RARE CHIP");
            Rarity = LootRarity.RARE;
        }
        else if (rand <= SpecialChance + RareChance + ExtremelyRareChance)
        {
            //Debug.Log("EXTREMLY RARE CHIP");
            Rarity = LootRarity.EXTREMELY_RARE;
        }
        else if (rand <= SpecialChance + RareChance + ExtremelyRareChance + UniqueChance)
        {
            //Debug.Log("UNIQUE CHIP");
            Rarity = LootRarity.UNIQUE;
        }
        else
        {
            //Debug.Log("NORMAL CHIP");
            Rarity = LootRarity.NORMAL;
        }
    }
    // ======================================================================================================================================== //
    private void setLootSize()
    {
        int rand = Random.Range(0, 5);
        if (rand == 0)
        {
            BodyTexture = Toolbox.Instance.ChipsResources.BodyTexture1on1;
            FrameTexture = Toolbox.Instance.ChipsResources.FrameTexture1on1;
        }
        else if (rand == 1)
        {
            BodyTexture = Toolbox.Instance.ChipsResources.BodyTexture1on2;
            FrameTexture = Toolbox.Instance.ChipsResources.FrameTexture1on2;
        }
        else if (rand == 2)
        {
            BodyTexture = Toolbox.Instance.ChipsResources.BodyTexture1on3;
            FrameTexture = Toolbox.Instance.ChipsResources.FrameTexture1on3;
        }
        else if (rand == 3)
        {
            BodyTexture = Toolbox.Instance.ChipsResources.BodyTexture2on2;
            FrameTexture = Toolbox.Instance.ChipsResources.FrameTexture2on2;
        }
        else
        {
            BodyTexture = Toolbox.Instance.ChipsResources.BodyTexture2on3;
            FrameTexture = Toolbox.Instance.ChipsResources.FrameTexture2on3;
        }
    }
    // ======================================================================================================================================== //
    private void setLootType()
    {
        int rand = Random.Range(0,5);
        if (rand == 0)
        {
            //Reward = new TankParamReward("TurretRotateSpeed", 5.0F, TankParamReward.RewardType.ADDITION);
            SkillTexture = TurretTexture;
            Type = LootType.TURRET_CHIP;
        }
        else if (rand == 1)
        {
            //Reward = new TankParamReward("TeleportLevel", 1.0F, TankParamReward.RewardType.ADDITION);
            SkillTexture = TeleportTexture;
            Type = LootType.TELEPORT_CHIP;
        }
        else if (rand == 2)
        {
            //Reward = new TankParamReward("MagFireRate", 0.2F, TankParamReward.RewardType.ADDITION);
            SkillTexture = RepairTexture;
            Type = LootType.BARREL_CHIP;
        }
        else if (rand == 3)
        {
            //Reward = new TankParamReward("MagAccuracy", 0.1F, TankParamReward.RewardType.ADDITION);
            SkillTexture = ShellTexture;
            Type = LootType.ENGINE_CHIP;
        }
        else if (rand == 4)
        {
            //Reward = new TankParamReward("MaxShield", 5.0F, TankParamReward.RewardType.ADDITION);
            SkillTexture = ShieldTexture;
            Type = LootType.SHIELD_CHIP;
            //Reward = Toolbox.Instance.ShieldReward.GetReward(LootRarity.NORMAL);
        }
    }
    // ======================================================================================================================================== //
    private void updateColor()
    {
        // set the changing color object
        GameObject objectChangingColor;
        if (State == LootState.ON_GROUND)
            objectChangingColor = gameObject;
        else
            objectChangingColor = Body;

        // update color
        if (Type == LootType.BURNT)
        {
            objectChangingColor.GetComponent<SpriteRenderer>().color = Color.black;
        }
        else if (Rarity == LootRarity.NORMAL)
        {
            objectChangingColor.GetComponent<SpriteRenderer>().color = Color.gray;
        }
        else if (Rarity == LootRarity.SPECIAL)
        {
            objectChangingColor.GetComponent<SpriteRenderer>().color = Color.Lerp(SilverStartColor, SilverEndColor, Mathf.PingPong(Time.realtimeSinceStartup, 1.0F));
        }
        else if (Rarity == LootRarity.RARE)
        {
            objectChangingColor.GetComponent<SpriteRenderer>().color = Color.Lerp(GoldStartColor, GoldEndColor, Mathf.PingPong(Time.realtimeSinceStartup, 1.0F));
        }
        else if (Rarity == LootRarity.EXTREMELY_RARE)
        {
            objectChangingColor.GetComponent<SpriteRenderer>().color = Color.Lerp(GreenStartColor, GreenEndColor, Mathf.PingPong(Time.realtimeSinceStartup, 1.0F));
        }
        else if (Rarity == LootRarity.UNIQUE)
        {
            float t = Mathf.PingPong(Time.realtimeSinceStartup / 2.0F, 1f);
            objectChangingColor.GetComponent<SpriteRenderer>().color = UniqueGradient.Evaluate(t);
        }
    }
    // ======================================================================================================================================== //
    public void Rotate90Deg()
    {
        // return if on ground or attached to mother board
        if (State != Loot.LootState.INSIDE_BAG)
            return;

        // return if square
        SpriteRenderer  chipSpriteRenderer  = gameObject.GetComponent<SpriteRenderer> ();
        int chipWidth = Mathf.RoundToInt(10 * chipSpriteRenderer.sprite.bounds.size.x);
        int chipHeight = Mathf.RoundToInt(10 * chipSpriteRenderer.sprite.bounds.size.y);
        //Debug.Log(chipWidth+","+chipHeight);
        if (chipWidth == chipHeight)
            return;
        
        float ZRotation = (transform.rotation.eulerAngles.z == 0) ? -90.0F : 90.0F;
        transform.Rotate(new Vector3(0.0F, 0.0F, ZRotation));
        Body.transform.Rotate(new Vector3(180.0F, 0.0F, 0.0F));
    }
    // ======================================================================================================================================== //
    private void setChipUnique()
    {
        
    }
    // ======================================================================================================================================== //
    private void setRewards()
    {
        //########################################################################### //
        // SHIELD
        //########################################################################### //
        if (Type == LootType.SHIELD_CHIP)
        {
            for (int i = 0; i < (int)Rarity; ++i)
            {
                Rewards.Add( new TankParamReward("MaxShield", 5.0F, TankParamReward.RewardType.ADDITION) );
            }
        }
    }
    // ======================================================================================================================================== //
    // ======================================================================================================================================== //
    // ======================================================================================================================================== //
    // ======================================================================================================================================== //
}
