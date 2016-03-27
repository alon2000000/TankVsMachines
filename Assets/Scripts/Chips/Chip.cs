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
		set 
		{
			_state = value;
			if (_state == ChipState.ON_GROUND)
				this.GetComponent<SpriteRenderer> ().sprite = GroundTexture;
			else
				this.GetComponent<SpriteRenderer> ().sprite = BagTexture;
		}
	}

	public Sprite GroundTexture;
	public Sprite BagTexture;

	// ================================================================================================ //
	void Start () 
	{
	
	}
	// ================================================================================================ //
	void Update () 
	{

	}
	// ================================================================================================ //
	void OnMouseDown()
	{
		if (State == ChipState.ON_GROUND)
			return;

		GameObject.Find ("InputManager").GetComponent<InputManager> ().DraggedObject = this.gameObject;
	}
	// ================================================================================================ //
}
