using UnityEngine;
using System.Collections;

public class LootCollection : MonoBehaviour 
{
    public GameObject ChipsBagObj;

    public TankParams TankParamsScript;

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
		GameObject 		lootObject 			= collision.collider.gameObject;
        Loot 			lootScript 			= lootObject.GetComponent<Loot> ();
        if (lootScript == null)
            return;
        SpriteRenderer 	chipSpriteRenderer 	= lootObject.GetComponent<SpriteRenderer> ();

        // if burnt add to cash
        if (lootScript.Type == Loot.LootType.BURNT)
        {
            Destroy(lootObject);
            TankParamsScript.CashChips++;
            return;
        }

        // else, create bigger version in inventory
        if (lootScript != null) 
		{
			// change chip state - to in bag
            lootScript.State = Loot.LootState.INSIDE_BAG;

            // remove color
            chipSpriteRenderer.color = Color.white;

            // change chip texture
            chipSpriteRenderer.sprite = lootScript.BagTexture;

			// change the layer of sprite
			chipSpriteRenderer.sortingLayerName = "AboveUI";

			// update polygon collider
            Destroy(lootObject.GetComponent<BoxCollider2D>());
            lootObject.AddComponent<BoxCollider2D>();

			// make the chip stop rotate
			//chipObject.GetComponent<Rotate>().enabled = false;
			
            // cancel rotation
            lootObject.transform.rotation = Quaternion.identity;

			// make the chip the child of the bag
            lootObject.transform.parent = ChipsBagObj.transform;

            // change chip layer
            lootObject.layer = LayerMask.NameToLayer("LootInInventoryLayer");

            // rotate the chip 90 deg at random
            int rand = Random.Range(0,2);
            if (rand == 1) 
                lootObject.transform.Rotate(new Vector3(180.0F, 0.0F, 90.0F));

            // get the chip size
            float chipWidth = chipSpriteRenderer.sprite.bounds.size.x;
            float chipHeight = chipSpriteRenderer.sprite.bounds.size.y;
            float maxScale = Mathf.Max(chipWidth, chipHeight);
            maxScale *= 1.15F; // margin factor

            // change position to be inside the bag
            SpriteRenderer bagSpriteRenderer = ChipsBagObj.GetComponent<SpriteRenderer> ();
            float bagWidth = bagSpriteRenderer.sprite.bounds.size.x;
            float bagHeight = bagSpriteRenderer.sprite.bounds.size.y;
            float randomX = Random.Range (ChipsBagObj.transform.position.x - bagWidth / 2.0F + maxScale / 2.0F, ChipsBagObj.transform.position.x + bagWidth / 2.0F - maxScale / 2.0F);
            float randomY = Random.Range (ChipsBagObj.transform.position.y - bagHeight / 2.0F + maxScale / 2.0F, ChipsBagObj.transform.position.y + bagHeight / 2.0F - maxScale / 2.0F);
            lootObject.transform.position = new Vector3(randomX, randomY);
		}
	}
	// ================================================================================================ //
}
