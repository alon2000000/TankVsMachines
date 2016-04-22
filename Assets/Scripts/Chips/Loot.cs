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
        PLUG
    }

    public enum LootState
	{
		ON_GROUND,
		INSIDE_BAG,
        ATTACHED
        /*ATTACHED_TO_BOARD,
        ATTACHED_TO_CHIP,
        ATTACHED_TO_ADAPTER*/
	}

    public enum LootRarity
    {
        NORMAL,
        SPECIAL,
        RARE,
        EXTREMLY_RARE
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

    public TankParam ChipBonus;

    public Color SilverStartColor;
    public Color SilverEndColor;
    public Color GoldStartColor;
    public Color GoldEndColor;
    public Color GreenStartColor;
    public Color GreenEndColor;

    // ======================================================================================================================================== //
	void Start () 
	{
        State = LootState.ON_GROUND;

        ChipBonus = Toolbox.Instance.ChipBonusManager.GetRandomTankParam();

        this.transform.RotateAround (this.transform.position, this.transform.forward, Random.Range(0, 360)); // rotate randomly

        // set type
        int rand = Random.Range(0,20);
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
