using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LootCollection : MonoBehaviour 
{
    public GameObject ChipsBagObj;

    public TankParams Params;

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
        if (lootScript.Type == Loot.LootType.SCRAP)
        {
            Destroy(lootObject);
            Params.Add("Cash", 1);
            return;
        }

        // if skill that exists - upgrade version to skill else, add it to the tank skills list
        if (lootScript.Type == Loot.LootType.SKILL_CHIP)
        {
            Skill skillScript = lootObject.GetComponent<Skill>();

            List<Skill> existsSameSkillScriptList = Toolbox.Instance.SkillsManager.TankSkills.Where(a => a.GetType() == skillScript.GetType()).ToList();

            if (existsSameSkillScriptList.Count == 0)
            {
                Toolbox.Instance.SkillsManager.TankSkills.Add(skillScript);
            }
            else
            {
                existsSameSkillScriptList[0].Version += 0.1F;
                Destroy(lootObject);
                return;
            }
        }

        // else, create bigger version in inventory
        if (lootScript != null) 
		{
			// change chip state - to in bag
            lootScript.State = Loot.LootState.INSIDE_BAG;

            // remove color
            chipSpriteRenderer.color = Color.white;

            // change chip texture
            chipSpriteRenderer.sprite = lootScript.FrameTexture;
            lootScript.Logo.SetActive(true);
            lootScript.Body.SetActive(true);
            lootScript.Decoration.SetActive(true);
            lootScript.Logo.GetComponent<SpriteRenderer>().sprite = lootScript.SkillTexture;
            lootScript.Body.GetComponent<SpriteRenderer>().sprite = lootScript.BodyTexture;
            if (lootScript.Type == Loot.LootType.SKILL_CHIP)
                lootScript.Decoration.GetComponent<SpriteRenderer>().sprite = lootScript.DecorationTexture;

			// change the layer of sprite
			chipSpriteRenderer.sortingLayerName = "AboveUI";
            // change the sorting layer of chips parts
            lootScript.PutChipOnTopOfAllOthers();

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
            {
                lootScript.Rotate90Deg();
            }

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
