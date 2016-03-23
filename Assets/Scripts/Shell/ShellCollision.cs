using UnityEngine;
using System.Collections;

public class ShellCollision : MonoBehaviour 
{

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
		Rigidbody2D selfRigidBody =  this.gameObject.GetComponent<Rigidbody2D> ();
		Rigidbody2D collidedRigidBody =  collision.collider.gameObject.GetComponent<Rigidbody2D> ();
		if (collidedRigidBody != null && selfRigidBody != null) 
		{
			Vector2 impulse = selfRigidBody.mass * selfRigidBody.velocity / Time.deltaTime;

			collidedRigidBody.AddForceAtPosition (impulse, collision.contacts[0].point); // 50 is bulletForce
		}
		Destroy(gameObject);
	}
	// ================================================================================================ //
}
