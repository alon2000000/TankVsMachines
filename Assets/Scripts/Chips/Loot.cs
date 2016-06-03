using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
        SCRAP,
        NORMAL_CHIP,
        SKILL_CHIP
    }

    public enum LogoType
    {
        TURRET,
        BARREL,
        SHELL,
        ENGINE,
        TELEPORT,
        ARMOR,
        ENERGY_BOOST
    }

    public enum LootRarity
    {
        NORMAL = 1,                 // gray
        SPECIAL = 3,                // silver glow
        RARE = 7,                   // glow gold
        UNIQUE                  // blinked random colors r/g/b
    }

    public List<TankParamReward> Rewards = new List<TankParamReward>();

    public LootState State;
    public LootType Type;
    public LootRarity Rarity;
    public LogoType ChipLogo;

    public Sprite GroundTexture;
    public Sprite BodyTexture;
    public Sprite FrameTexture;
    public Sprite SkillTexture;

    // colors
    public Color SilverStartColor;
    public Color SilverEndColor;
    public Color GoldStartColor;
    public Color GoldEndColor;
    public Color BlueStartColor;
    public Color BlueEndColor;
    public Color GreenStartColor;
    public Color GreenEndColor;

    public Gradient UniqueGradient;

    // the parts of the chip
    public GameObject Body;
    public GameObject Frame;
    public GameObject Logo;
    public GameObject SkillChildObject;

    // chances
    public float GetScrapChance = 75.0F;
    public float GetSkillChipChance = 5.0F;

    public float SpecialChance = 20.0F;
    public float RareChance = 4.0F;
    public float UniqueChance = 0.2F;
    // ======================================================================================================================================== //
	void Start () 
	{
        // set init state to be on ground
        State = LootState.ON_GROUND;
        // rotate randomly on ground
        this.transform.RotateAround (this.transform.position, this.transform.forward, Random.Range(0, 360)); 

        // scrap, normal chip or skill chip
        setLootType();

        // do different things for scrap, normal chip or skill chip
        if (Type == LootType.SCRAP)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Toolbox.Instance.ScrapOnGroundResources.GetRandomSprite();
        }
        else if (Type == LootType.SKILL_CHIP) // skill chip
        {
            BodyTexture = Toolbox.Instance.ChipsResources.BodySkillTexture;
            FrameTexture = Toolbox.Instance.ChipsResources.FrameTexture2on2;

            setLootLogoAndScript4SkillChip();
            setRewards();
        }
        else // normal chip
        {
            setLootRarity();

            if (Rarity == LootRarity.UNIQUE)
            {
                setChipUnique();
            }
            else
            {
                setLootSize();
                setLootLogo4NormalChip();
                setRewards();
            }
        }
	}
    // ======================================================================================================================================== //
    void Update () 
	{
        updateColor();
	}
    // ======================================================================================================================================== //
    private void setLootRarity()
    {
        // if skill chip, the rarity is normal
        if (Type == LootType.SKILL_CHIP)
        {
            Rarity = LootRarity.NORMAL;
            return;
        }

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
        else if (rand <= SpecialChance + RareChance + UniqueChance)
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
        float rand = Random.Range(0.0F, 100.0F);
        if (rand <= GetScrapChance)
        {
            Type = LootType.SCRAP;
        }
        else if (rand <= GetScrapChance + GetSkillChipChance)
        {
            Type = LootType.SKILL_CHIP;
        }
        else
        {
            Type = LootType.NORMAL_CHIP;
        }
    }
    // ======================================================================================================================================== //
    private void setLootLogo4NormalChip()
    {
        int rand = Random.Range(0,4);
        if (rand == 0)
        {
            SkillTexture = Toolbox.Instance.ChipsResources.TurretTexture;
            ChipLogo = LogoType.TURRET;
        }
        else if (rand == 1)
        {
            SkillTexture = Toolbox.Instance.ChipsResources.RepairTexture;
            ChipLogo = LogoType.BARREL;
        }
        else if (rand == 2)
        {
            SkillTexture = Toolbox.Instance.ChipsResources.ShellTexture;
            ChipLogo = LogoType.ENGINE;
        }
        else if (rand == 3)
        {
            SkillTexture = Toolbox.Instance.ChipsResources.ShieldTexture;
            ChipLogo = LogoType.ARMOR;
        }
    }
    // ======================================================================================================================================== //
    private void setLootLogoAndScript4SkillChip()
    {
        int rand = Random.Range(0,2);
        if (rand == 0)
        {
            SkillTexture = Toolbox.Instance.ChipsResources.TeleportTexture;
            ChipLogo = LogoType.TELEPORT;
            SkillChildObject.AddComponent<Teleport>();
        }
        else if (rand == 1)
        {
            SkillTexture = Toolbox.Instance.ChipsResources.EnergyBoostTexture;
            ChipLogo = LogoType.ENERGY_BOOST;
            SkillChildObject.AddComponent<EnergyBoost>();
        }
    }
    // ======================================================================================================================================== //
    private void updateColor()
    {
        // update color
        if (Type == LootType.SCRAP)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.black;
        }
        else if (Type == LootType.SKILL_CHIP)
        {
            if (State == LootState.ON_GROUND)
                gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(GoldStartColor, GoldEndColor, Mathf.PingPong(Time.realtimeSinceStartup, 1.0F));
            else if (State == LootState.INSIDE_BAG)
                Logo.GetComponent<SpriteRenderer>().color = (GoldEndColor + GoldStartColor) / 2;
            else
                Logo.GetComponent<SpriteRenderer>().color = Color.Lerp(GoldStartColor, GoldEndColor, Mathf.PingPong(Time.realtimeSinceStartup, 1.0F));
        }
        else if (Rarity == LootRarity.NORMAL)
        {
            if (State == LootState.ON_GROUND)
                gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(SilverStartColor, SilverEndColor, Mathf.PingPong(Time.realtimeSinceStartup, 1.0F));
            else
                Logo.GetComponent<SpriteRenderer>().color = Color.gray;
        }
        else if (Rarity == LootRarity.SPECIAL)
        {
            if (State == LootState.ON_GROUND)
                gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(BlueStartColor, BlueEndColor, Mathf.PingPong(Time.realtimeSinceStartup, 1.0F));
            else if (State == LootState.INSIDE_BAG)
                Logo.GetComponent<SpriteRenderer>().color = (BlueEndColor + BlueStartColor) / 2;
            else
                Logo.GetComponent<SpriteRenderer>().color = Color.Lerp(BlueStartColor, BlueEndColor, Mathf.PingPong(Time.realtimeSinceStartup, 1.0F));
        }
        else if (Rarity == LootRarity.RARE)
        {
            if (State == LootState.ON_GROUND)
                gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(GreenStartColor, GreenEndColor, Mathf.PingPong(Time.realtimeSinceStartup, 1.0F));
            else if (State == LootState.INSIDE_BAG)
                Logo.GetComponent<SpriteRenderer>().color = (GreenEndColor + GreenStartColor) / 2;
            else
                Logo.GetComponent<SpriteRenderer>().color = Color.Lerp(GreenStartColor, GreenEndColor, Mathf.PingPong(Time.realtimeSinceStartup, 1.0F));
        }
        else if (Rarity == LootRarity.UNIQUE)
        {
            float t = Mathf.PingPong(Time.realtimeSinceStartup / 2.0F, 1f);

            if (State == LootState.ON_GROUND)
                gameObject.GetComponent<SpriteRenderer>().color = UniqueGradient.Evaluate(t);
            else if (State == LootState.INSIDE_BAG)
                Logo.GetComponent<SpriteRenderer>().color = Color.red;
            else
                Logo.GetComponent<SpriteRenderer>().color = UniqueGradient.Evaluate(t);
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
    private void addReward(TankParamReward reward)
    {
        // if exists reward with the same name and type - add to it
        foreach (TankParamReward r in Rewards)
        {
            if (r.Name == reward.Name && r.Type == reward.Type)
            {
                r.Value += reward.Value;
                return;
            }
        }
        // else - add new one
        Rewards.Add(reward);
    }
    // ======================================================================================================================================== //
    private void setRewards()
    {
        //########################################################################### //
        // ARMOR
        //########################################################################### //
        if (ChipLogo == LogoType.ARMOR)
        {
            for (int i = 0; i < (int)Rarity; ++i)
            {
                int rand = Random.Range(0, 21);
                if (rand <= 15)
                {
                    addReward(new TankParamReward("MaxShield", (float)Random.Range(1,6), TankParamReward.RewardType.ADDITION));
                }
                else if (rand <= 18)
                {
                    addReward(new TankParamReward("ShieldVsPenetration", Random.Range(1.0F,10.0F), TankParamReward.RewardType.ADDITION));
                }
                else if (rand <= 19)
                {
                    addReward(new TankParamReward("ShieldDurability", (float)Random.Range(1,6), TankParamReward.RewardType.ADDITION));
                }
                else if (rand <= 20)
                {
                    addReward(new TankParamReward("ShieldAbsorption", (float)Random.Range(1,6), TankParamReward.RewardType.ADDITION));
                }
            }
        }
        //########################################################################### //
        // ENGINE
        //########################################################################### //
        if (ChipLogo == LogoType.TELEPORT)
        {
            addReward(new TankParamReward("TeleportLevel", 1.0F, TankParamReward.RewardType.ADDITION));

            int rand = Random.Range(0, 21);
            if (rand <= 17)
            {
                addReward(new TankParamReward("TeleportCost", -(int)Rarity * Random.Range(1.0F, 3.0F), TankParamReward.RewardType.PERCENT));
            }
            else
            {
                addReward(new TankParamReward("TeleportDistance", (int)Rarity * Random.Range(1.0F, 5.0F), TankParamReward.RewardType.ADDITION));
                addReward(new TankParamReward("TeleportCost", (float)(int)Rarity * Random.Range(1, 5), TankParamReward.RewardType.ADDITION));
            }
        }
        //########################################################################### //
        // ENERGY BOOST
        //########################################################################### //
        if (ChipLogo == LogoType.ENERGY_BOOST)
        {
            addReward(new TankParamReward("EnergyBoostLevel", 1.0F, TankParamReward.RewardType.ADDITION));

            int rand = Random.Range(0, 21);
            if (rand <= 17)
            {
                addReward(new TankParamReward("EnergyBoostCost", (float)(-(int)Rarity * Random.Range(1, 4)), TankParamReward.RewardType.PERCENT));
            }
            else
            {
                addReward(new TankParamReward("EnergyBoostValue", (float)(int)Rarity * Random.Range(1, 6), TankParamReward.RewardType.ADDITION));
                addReward(new TankParamReward("EnergyBoostCost", (float)(int)Rarity * Random.Range(1, 5), TankParamReward.RewardType.ADDITION));
            }
        }
        //########################################################################### //
        // OTHERS...
        //########################################################################### //
    }
    // ======================================================================================================================================== //
    private void setChipUnique()
    {

    }
    // ======================================================================================================================================== //
    // ======================================================================================================================================== //
    // ======================================================================================================================================== //
    // ======================================================================================================================================== //
}
