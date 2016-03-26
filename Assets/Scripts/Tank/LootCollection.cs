using UnityEngine;
using System.Collections;

public class LootCollection : MonoBehaviour 
{
	public ChipsBag Bag;

	// ================================================================================================ //
	void Start () 
	{
	
	}
	// ================================================================================================ //
	void Update () 
	{
	
	}
	// ================================================================================================ //
	void OnCollisionEnter2D(Collision2D collision)
	{
		Chip collidedChip =  collision.collider.gameObject.GetComponent<Chip> ();

		if (collidedChip != null) 
		{
			GameObject chip = collision.collider.gameObject;
			chip.transform.Translate (new Vector3(9999.9F, 9999.9F));
			Bag.Chips.Add (chip);
		}
	}
	// ================================================================================================ //
}
