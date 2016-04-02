using UnityEngine;
using System.Collections;

public class LootCollection : MonoBehaviour 
{
    public GameObject ChipsBagObj;

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
		GameObject 		chipObject 			= collision.collider.gameObject;
		Chip 			chipScript 			= chipObject.GetComponent<Chip> ();
		SpriteRenderer 	chipSpriteRenderer 	= chipObject.GetComponent<SpriteRenderer> ();

		if (chipScript != null) 
		{
			// change cip state - to in bag
			chipScript.State = Chip.ChipState.IN_BAG;

			// change position to be inside the bag
            SpriteRenderer bagSpriteRenderer = ChipsBagObj.GetComponent<SpriteRenderer> ();
			float bagWidth = bagSpriteRenderer.sprite.bounds.size.x;
			float bagHeight = bagSpriteRenderer.sprite.bounds.size.y;
            float randomX = Random.Range (ChipsBagObj.transform.position.x - bagWidth / 2.0F, ChipsBagObj.transform.position.x + bagWidth / 2.0F);
            float randomY = Random.Range (ChipsBagObj.transform.position.y - bagHeight / 2.0F, ChipsBagObj.transform.position.y + bagHeight / 2.0F);
			chipObject.transform.position = new Vector3(randomX, randomY);

			// change the layer of sprite
			chipSpriteRenderer.sortingLayerName = "AboveUI";

			// update polygon collider
			Destroy(chipObject.GetComponent<PolygonCollider2D>());
			chipObject.AddComponent<PolygonCollider2D>();

			// make the chip stop rotate
			chipObject.GetComponent<Rotate>().enabled = false;
			chipObject.transform.rotation = Quaternion.identity;

			// make the chip the child of the bag
            chipObject.transform.parent = ChipsBagObj.transform;

            // change chip layer
            chipObject.layer = LayerMask.NameToLayer("LootInInventoryLayer");

			// add to bag script ???? need this????
            ChipsBagObj.GetComponent<ChipsBag>().Chips.Add (chipObject);
		}
	}
	// ================================================================================================ //
}
