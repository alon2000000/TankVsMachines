using UnityEngine;
using System.Collections;

public class Loot : MonoBehaviour 
{
    public enum LootType
    {
        BURNT,
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
        NORMAL,                 // gray
        SPECIAL,                // silver glow
        RARE,                   // glow gold
        EXTREMLY_RARE,          // glow color
        UNIQUE                  // blinked random colors r/g/b
    }

    public LootState State { get; set; }

    public LootType Type;
    public LootRarity Rarity;

    public Sprite GroundTexture;
	public Sprite BagTexture;

    public Sprite BagTexture1on1;
    public Sprite BagTexture1on2;
    public Sprite BagTexture1on3;
    public Sprite BagTexture2on2;
    public Sprite BagTexture2on3;

    public TankParamReward Reward;

    public Color SilverStartColor;
    public Color SilverEndColor;
    public Color GoldStartColor;
    public Color GoldEndColor;
    public Color GreenStartColor;
    public Color GreenEndColor;
    public Color RedStartColor;
    public Color RedEndColor;
    public Color BlueStartColor;
    public Color BlueEndColor;

    public Gradient UniqueGradient;

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

        this.transform.RotateAround (this.transform.position, this.transform.forward, Random.Range(0, 360)); // rotate randomly

        setLootType();

        if (Type == LootType.BURNT)
            return;

        setLootRarity();
        setRandomBagTexture();
	}
    // ======================================================================================================================================== //
    void Update () 
	{
        /*if (State != LootState.ON_GROUND)
            return;*/

        if (Rarity == LootRarity.SPECIAL)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(SilverStartColor, SilverEndColor, Mathf.PingPong(Time.realtimeSinceStartup, 1.0F));
        }
        else if (Rarity == LootRarity.RARE)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(GoldStartColor, GoldEndColor, Mathf.PingPong(Time.realtimeSinceStartup, 1.0F));
        }
        else if (Rarity == LootRarity.EXTREMLY_RARE)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(GreenStartColor, GreenEndColor, Mathf.PingPong(Time.realtimeSinceStartup, 1.0F));
        }
        else if (Rarity == LootRarity.UNIQUE)
        {
            float t = Mathf.PingPong(Time.realtimeSinceStartup / 2.0F, 1f);
            gameObject.GetComponent<SpriteRenderer>().color = UniqueGradient.Evaluate(t);
        }
	}
    // ======================================================================================================================================== //
	void OnMouseDown()
	{
		
	}
    // ======================================================================================================================================== //
    private void setRandomBagTexture()
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
    // ======================================================================================================================================== //
    private void setLootType()
    {
        // 70% to be burnt
        int rand = Random.Range(0,10);
        if (rand < 7)
        {
            Type = LootType.BURNT;
            gameObject.GetComponent<SpriteRenderer>().color = Color.black;
            return;
        }

        // 30% to be usable chip
        rand = Random.Range(0,4);
        if (rand == 0)
        {
            Reward = new TankParamReward("TurretRotateSpeed", 5.0F, TankParamReward.RewardType.ADDITION);
            SkillTexture = TurretTexture;
            Type = LootType.TURRET_CHIP;
        }
        else if (rand == 1)
        {
            Reward = new TankParamReward("TeleportLevel", 1.0F, TankParamReward.RewardType.ADDITION);
            SkillTexture = TeleportTexture;
            Type = LootType.TELEPORT_CHIP;
        }
        else if (rand == 2)
        {
            Reward = new TankParamReward("MagFireRate", 0.2F, TankParamReward.RewardType.ADDITION);
            SkillTexture = RepairTexture;
            Type = LootType.BARREL_CHIP;
        }
        else if (rand == 3)
        {
            Reward = new TankParamReward("MagAccuracy", 0.1F, TankParamReward.RewardType.ADDITION);
            SkillTexture = ShellTexture;
            Type = LootType.ENGINE_CHIP;
        }
    }
    // ======================================================================================================================================== //
    private void setLootRarity()
    {
        int rand = Random.Range(0,1000);
        if (rand < 750) // 75%
        {
            Debug.Log("NORMAL CHIP");
            Rarity = LootRarity.NORMAL;
            gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
        }
        else if (rand < 950) // 20%
        {
            Debug.Log("SPECIAL CHIP");
            Rarity = LootRarity.SPECIAL;
        }
        else if (rand < 990) // 4%
        {
            Debug.Log("RARE CHIP");
            Rarity = LootRarity.RARE;
        }
        else if (rand < 998) // 0.7%
        {
            Debug.Log("EXTREMLY RARE CHIP");
            Rarity = LootRarity.EXTREMLY_RARE;
        }
        else // 0.2%
        {
            Debug.Log("UNIQUE CHIP");
            Rarity = LootRarity.UNIQUE;
        }
    }
    // ======================================================================================================================================== //
}
