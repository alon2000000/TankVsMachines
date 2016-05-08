using UnityEngine;
using System.Collections;

public class Loot : MonoBehaviour 
{
    public enum LootType
    {
        BURNT,
        SIMPLE_CHIP,
        SKILL_CHIP,
        SKILL_CHIP_ADAPTER,
        PLUG,
        TURRET_CHIP,
        BARREL_CHIP,
        SHELL_CHIP,
        ENGINE_CHIP,
        TELEPORT_CHIP
    }

    public enum LootState
	{
		ON_GROUND,
		INSIDE_BAG,
        ATTACHED
	}

    public enum LootRarity
    {
        NORMAL,         // gray
        SPECIAL,        // silver
        RARE,           // gold
        EXTREMLY_RARE,  // glow gold
        UNIQUE          // blinked random colors
    }

    public LootState State { get; set; }

    public LootType Type;

    public Sprite GroundTexture;
	public Sprite BagTexture;

    public Sprite BagTexture1on1;
    public Sprite BagTexture1on2;
    public Sprite BagTexture1on3;
    public Sprite BagTexture2on2;
    public Sprite BagTexture2on3;
    public Sprite BagTextureSkillChip;
    public Sprite BagTextureSkillChipAdapter;

    public TankParamReward Reward;

    public Color SilverStartColor;
    public Color SilverEndColor;
    public Color GoldStartColor;
    public Color GoldEndColor;
    public Color GreenStartColor;
    public Color GreenEndColor;

    public Transform SkillTextureTransform;
    public Sprite SkillTexture;
    // skills sprites
    public Sprite RepairTexture;
    public Sprite TurretTexture;
    public Sprite ShellTexture;
    public Sprite TeleportTexture;

    // ======================================================================================================================================== //
	void Start () 
	{
        State = LootState.ON_GROUND;

        // TODO: to function...
        int rand = Random.Range(0,4);
        if (rand == 0)
        {
            Reward = new TankParamReward("TurretRotateSpeed", 5.0F, TankParamReward.RewardType.ADDITION);
            SkillTexture = TurretTexture;
        }
        else if (rand == 1)
        {
            Reward = new TankParamReward("TeleportLevel", 1.0F, TankParamReward.RewardType.ADDITION);
            SkillTexture = TeleportTexture;
        }
        else if (rand == 2)
        {
            Reward = new TankParamReward("MagFireRate", 0.2F, TankParamReward.RewardType.ADDITION);
            SkillTexture = RepairTexture;
        }
        else if (rand == 3)
        {
            Reward = new TankParamReward("MagAccuracy", 0.1F, TankParamReward.RewardType.ADDITION);
            SkillTexture = ShellTexture;
        }

        this.transform.RotateAround (this.transform.position, this.transform.forward, Random.Range(0, 360)); // rotate randomly

        // set type
        rand = Random.Range(0,20);
        if (rand >= 0 && rand < 10)
        {
            Type = LootType.BURNT;
            gameObject.GetComponent<SpriteRenderer>().color = Color.black;
        }
        else if (rand >= 10 && rand < 17)
        {
            Type = LootType.SIMPLE_CHIP;
            gameObject.GetComponent<SpriteRenderer>().color = SilverStartColor;
        }
        else if (rand >= 17 && rand < 19)
        {
            Type = LootType.SKILL_CHIP_ADAPTER;
            gameObject.GetComponent<SpriteRenderer>().color  = GreenStartColor;
        }
        else
        {
            Type = LootType.SKILL_CHIP;
            gameObject.GetComponent<SpriteRenderer>().color = GoldStartColor;
        }

        setRandomBagTexture();
	}
    // ======================================================================================================================================== //
    void Update () 
	{
        if (State != LootState.ON_GROUND)
            return;

        if (Type == LootType.SIMPLE_CHIP)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(SilverStartColor, SilverEndColor, Mathf.PingPong(Time.time, 1.0F));
        }
        else if (Type == LootType.SKILL_CHIP)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(GoldStartColor, GoldEndColor, Mathf.PingPong(Time.time, 1.0F));
        }
        else if (Type == LootType.SKILL_CHIP_ADAPTER)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(GreenStartColor, GreenEndColor, Mathf.PingPong(Time.time, 1.0F));
        }
	}
    // ======================================================================================================================================== //
	void OnMouseDown()
	{
		
	}
    // ======================================================================================================================================== //
    public void setRandomBagTexture()
    {
        if (Type == LootType.SIMPLE_CHIP)
        {
            int rand = Random.Range(0, 30);
            if (rand >= 0 && rand < 5)
                BagTexture = BagTexture1on1;
            else if (rand >= 5 && rand < 15)
                BagTexture = BagTexture1on2;
            else if (rand >= 15 && rand < 20)
                BagTexture = BagTexture1on3;
            else if (rand >= 20 && rand < 25)
                BagTexture = BagTexture2on2;
            else
                BagTexture = BagTexture2on3;
        }
        else if (Type == LootType.SKILL_CHIP)
        {
            BagTexture = BagTextureSkillChip;
        }
        else if (Type == LootType.SKILL_CHIP_ADAPTER)
        {
            BagTexture = BagTextureSkillChipAdapter;
        }
    }
    // ======================================================================================================================================== //
}
