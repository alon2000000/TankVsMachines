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
        SPECIAL,                // gold
        RARE,                   // glow gold
        EXTREMLY_RARE_RED,      // glow red
        EXTREMLY_RARE_GREEN,    // glow green
        EXTREMLY_RARE_BLUE,     // glow blue
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

        if (Rarity == LootRarity.RARE)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(GoldStartColor, GoldEndColor, Mathf.PingPong(Time.time, 1.0F));
        }
        else if (Rarity == LootRarity.EXTREMLY_RARE_RED)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(RedStartColor, RedEndColor, Mathf.PingPong(Time.time, 1.0F));
        }
        else if (Rarity == LootRarity.EXTREMLY_RARE_GREEN)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(GreenStartColor, GreenEndColor, Mathf.PingPong(Time.time, 1.0F));
        }
        else if (Rarity == LootRarity.EXTREMLY_RARE_BLUE)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(BlueStartColor, BlueEndColor, Mathf.PingPong(Time.time, 1.0F));
        }
        else if (Rarity == LootRarity.UNIQUE)
        {
            int rand = Random.Range(0, 6);
            if (rand == 0)
                gameObject.GetComponent<SpriteRenderer>().color = GreenStartColor;
            else if (rand == 1)
                gameObject.GetComponent<SpriteRenderer>().color = GreenEndColor;
            else if (rand == 2)
                gameObject.GetComponent<SpriteRenderer>().color = RedStartColor;
            else if (rand == 3)
                gameObject.GetComponent<SpriteRenderer>().color = RedEndColor;
            else if (rand == 4)
                gameObject.GetComponent<SpriteRenderer>().color = BlueStartColor;
            else if (rand == 5)
                gameObject.GetComponent<SpriteRenderer>().color = BlueEndColor;
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
            gameObject.GetComponent<SpriteRenderer>().color  = GoldEndColor;
        }
        else if (rand < 990) // 4%
        {
            Debug.Log("RARE CHIP");
            Rarity = LootRarity.RARE;
        }
        else if (rand < 998) // 0.7%
        {
            rand = Random.Range(0, 3);
            if (rand == 0)
            {
                Debug.Log("EXTREMLY RARE (RED) CHIP");
                Rarity = LootRarity.EXTREMLY_RARE_RED;
            }
            else if (rand == 1)
            {
                Debug.Log("EXTREMLY RARE (GREEN) CHIP");
                Rarity = LootRarity.EXTREMLY_RARE_GREEN;
            }
            else
            {
                Debug.Log("EXTREMLY RARE (BLUE) CHIP");
                Rarity = LootRarity.EXTREMLY_RARE_BLUE;
            }
        }
        else // 0.2%
        {
            Debug.Log("UNIQUE CHIP");
            Rarity = LootRarity.UNIQUE;
        }
    }
    // ======================================================================================================================================== //
}
