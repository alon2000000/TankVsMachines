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
    public Sprite BodyTexture;
    public Sprite FrameTexture;

    public Sprite BodyTexture1on1;
    public Sprite BodyTexture1on2;
    public Sprite BodyTexture1on3;
    public Sprite BodyTexture2on2;
    public Sprite BodyTexture2on3;

    public Sprite FrameTexture1on1;
    public Sprite FrameTexture1on2;
    public Sprite FrameTexture1on3;
    public Sprite FrameTexture2on2;
    public Sprite FrameTexture2on3;

    public TankParamReward Reward;

    public Color SilverStartColor;
    public Color SilverEndColor;
    public Color GoldStartColor;
    public Color GoldEndColor;
    public Color GreenStartColor;
    public Color GreenEndColor;

    public Gradient UniqueGradient;

    public Transform SkillTextureTransform;
    public Sprite SkillTexture;
    // skills sprites
    public Sprite RepairTexture;
    public Sprite TurretTexture;
    public Sprite ShellTexture;
    public Sprite TeleportTexture;

    // the parts of the chip
    public GameObject Body;
    public GameObject Frame;
    public GameObject Logo;
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
        updateColor();
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
        {
            BodyTexture = BodyTexture1on1;
            FrameTexture = FrameTexture1on1;
        }
        else if (rand >= 5 && rand < 15)
        {
            BodyTexture = BodyTexture1on2;
            FrameTexture = FrameTexture1on2;
        }
        else if (rand >= 15 && rand < 20)
        {
            BodyTexture = BodyTexture1on3;
            FrameTexture = FrameTexture1on3;
        }
        else if (rand >= 20 && rand < 25)
        {
            BodyTexture = BodyTexture2on2;
            FrameTexture = FrameTexture2on2;
        }
        else
        {
            BodyTexture = BodyTexture2on3;
            FrameTexture = FrameTexture2on3;
        }
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
            //Debug.Log("NORMAL CHIP");
            Rarity = LootRarity.NORMAL;
        }
        else if (rand < 950) // 20%
        {
            //Debug.Log("SPECIAL CHIP");
            Rarity = LootRarity.SPECIAL;
        }
        else if (rand < 990) // 4%
        {
            //Debug.Log("RARE CHIP");
            Rarity = LootRarity.RARE;
        }
        else if (rand < 998) // 0.7%
        {
            //Debug.Log("EXTREMLY RARE CHIP");
            Rarity = LootRarity.EXTREMLY_RARE;
        }
        else // 0.2%
        {
            //Debug.Log("UNIQUE CHIP");
            Rarity = LootRarity.UNIQUE;
        }
    }
    // ======================================================================================================================================== //
    private void updateColor()
    {
        if (Type == LootType.BURNT)
            return;

        GameObject objectChangingColor;
        if (State == LootState.ON_GROUND)
            objectChangingColor = gameObject;
        else
            objectChangingColor = Body;

        if (Rarity == LootRarity.NORMAL)
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
        else if (Rarity == LootRarity.EXTREMLY_RARE)
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
}
