using UnityEngine;
using System.Collections;

public class Chip : MonoBehaviour 
{
    public enum ChipType
    {
        BURNT,
        SIMPLE,
        UNIQUE
    }

    public enum ChipState
	{
		ON_GROUND,
		IN_BAG,
		IN_BOARD
	}

	private ChipState _state = ChipState.ON_GROUND;
	public ChipState State
	{
		get { return _state; }
		set { _state = value; }
	}

    public ChipType Type;

    public Sprite GroundTexture;
	public Sprite BagTexture;

    public Sprite BagTexture1on1;
    public Sprite BagTexture1on2;
    public Sprite BagTexture1on3;
    public Sprite BagTexture2on2;
    public Sprite BagTexture2on3;

    public TankParam ChipBonus;

    public RuntimeAnimatorController SimpleAnimationController;
    public RuntimeAnimatorController UniqueAnimationController;
    public float AnimationSpeed;

	// ================================================================================================ //
	void Start () 
	{
        ChipBonus = Toolbox.Instance.TankParams.GetRandomTankParam();
        this.transform.RotateAround (this.transform.position, this.transform.forward, Random.Range(0, 360)); // rotate randomly
        //this.GetComponent<SpriteRenderer>().sprite = GroundTexture; // set init ground texture
        setRandomBagTexture();

        // set type
        int rand = Random.Range(0,20);
        if (rand >= 0 && rand < 17)
        {
            Type = ChipType.BURNT;
            gameObject.GetComponent<Animator>().enabled = false;
        }
        else if (rand >= 17 && rand < 18)
        {
            Type = ChipType.SIMPLE;
            gameObject.GetComponent<Animator>().runtimeAnimatorController = SimpleAnimationController;
            gameObject.GetComponent<Animator>().speed = AnimationSpeed;
        }
        else
        {
            Type = ChipType.UNIQUE;
            gameObject.GetComponent<Animator>().runtimeAnimatorController = UniqueAnimationController;
            gameObject.GetComponent<Animator>().speed = AnimationSpeed;
        }


	}
	// ================================================================================================ //
	void Update () 
	{

	}
	// ================================================================================================ //
	void OnMouseDown()
	{
		
	}
    // ================================================================================================ //
    public void setRandomBagTexture()
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
    // ================================================================================================ //
}
