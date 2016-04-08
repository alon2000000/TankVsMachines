using UnityEngine;
using System.Collections;

public class Chip : MonoBehaviour 
{
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

    public Sprite GroundTexture;
	public Sprite BagTexture;

    public Sprite BagTexture1on1;
    public Sprite BagTexture1on2;
    public Sprite BagTexture1on3;
    public Sprite BagTexture2on2;
    public Sprite BagTexture2on3;

    public TankParam ChipBonus;

	// ================================================================================================ //
	void Start () 
	{
        ChipBonus = Toolbox.Instance.TankParams.GetRandomTankParam();
        this.GetComponent<SpriteRenderer>().sprite = GroundTexture; // set init ground texture
        setRandomBagTexture();
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
