using UnityEngine;
using System.Collections;

public class ShellCollision : MonoBehaviour 
{
    public float ImpactDamage;
    public float ExplosionDamage;
    public float ChanceToPenetrate;
    public GameObject ExplosionObj;
    // ======================================================================================================================================== //
	void Start () 
	{
	
	}
    // ======================================================================================================================================== //
	void Update () 
	{
		
	}
    // ======================================================================================================================================== //
	void OnCollisionEnter2D(Collision2D collision)
	{
		Rigidbody2D selfRigidBody =  this.gameObject.GetComponent<Rigidbody2D> ();
		Rigidbody2D collidedRigidBody =  collision.collider.gameObject.GetComponent<Rigidbody2D> ();
		if (collidedRigidBody != null && selfRigidBody != null) 
		{
			Vector2 impulse = selfRigidBody.mass * selfRigidBody.velocity / Time.deltaTime;

			collidedRigidBody.AddForceAtPosition (impulse, collision.contacts[0].point); // 50 is bulletForce

            TankParams paramsScript = collidedRigidBody.gameObject.GetComponent<TankParams>();
            if (paramsScript != null)
            {
                float totalDamage = ImpactDamage + ExplosionDamage;

                float shieldVsPenetration = paramsScript.Get("ShieldVsPenetration");
                float maxShield = paramsScript.Get("MaxShield");
                float shield = paramsScript.Get("Shield");
                float shieldFactor = shield / maxShield;
                float shieldDurability = paramsScript.Get("ShieldDurability");
                float shieldAbsorption = paramsScript.Get("ShieldAbsorption");

                float rand = Random.Range(0.0F, 100.0F);
                if (rand <= shieldVsPenetration) // not penetrate
                {
                    Debug.Log("DEFEND!");
                    float damage4Shield = totalDamage * shieldAbsorption / 100.0F;
                    float totalDamage4Shiled = damage4Shield * (1.0F - shieldDurability / 100.0F);
                    float margin = (shield >= totalDamage4Shiled) ? 0.0F : (totalDamage4Shiled - shield);
                    paramsScript.Set("Shield", (shield < totalDamage4Shiled) ? 0.0F : (shield - totalDamage4Shiled));

                    float damage4HP = totalDamage - damage4Shield + margin;
                    paramsScript.HP -= damage4HP;
                }
                else // penetrate
                {
                    Debug.Log("OUCH!");
                    paramsScript.HP -= (totalDamage * 2.0F);
                }
            }
		}
        //Instantiate(ExplosionObj, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
	}
    // ======================================================================================================================================== //
    void OnDestroy() 
    {
        Instantiate(ExplosionObj, gameObject.transform.position, Quaternion.identity);
    }
    // ======================================================================================================================================== //
}
