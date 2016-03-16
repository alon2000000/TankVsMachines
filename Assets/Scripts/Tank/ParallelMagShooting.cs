using UnityEngine;
using System.Collections;

public class ParallelMagShooting : MonoBehaviour 
{
	public KeyCode FireKey;
	public float ShootingInterval;
	public Transform Nuzzle;
	public float BulletForce;
	public Transform Flash;
	public GameObject HitEffect;
	public float ShootDistance;

	private float _lastTimeShooting;
	// ================================================================================================ //
	void Start () 
	{
		_lastTimeShooting = Time.time;
	}
	// ================================================================================================ //
	void FixedUpdate () 
	{
		float timePassedfFromLastShot = Time.time - _lastTimeShooting;
		if (Input.GetKey (FireKey) && timePassedfFromLastShot > ShootingInterval) 
		{
			// show flash effect
			Flash.GetComponent<Renderer>().enabled = true;

			_lastTimeShooting = Time.time;

			Vector2 nuzzleRotation = Nuzzle.localRotation * Nuzzle.up;

			RaycastHit2D hit = Physics2D.Raycast (Nuzzle.position, nuzzleRotation, ShootDistance, ~(1 << LayerMask.NameToLayer ("TankLayer")));

			if (hit.collider != null) 
			{
				// hit effect
				Instantiate (HitEffect, hit.point, Quaternion.identity);

				Rigidbody2D collidedRigidBody = hit.collider.gameObject.GetComponent<Rigidbody2D> ();
				if (collidedRigidBody != null) 
				{
					collidedRigidBody.AddForce (nuzzleRotation * BulletForce);
				}
			} 
			else 
			{
				// hit effect
				Vector3 hitPosition = Nuzzle.position + new Vector3(nuzzleRotation.x, nuzzleRotation.y, 0.0F) * ShootDistance;
				Instantiate (HitEffect, hitPosition, Quaternion.identity);
			}
		} 
		else 
		{
			// hide flash effect
			Flash.GetComponent<Renderer>().enabled = false;
		}
	}
	// ================================================================================================ //
}
