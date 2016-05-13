using UnityEngine;
using System.Collections;

public class ShellCollision : MonoBehaviour 
{
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
                //paramsScript.Shield
                paramsScript.HP -= 25; // TODO: to config
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
