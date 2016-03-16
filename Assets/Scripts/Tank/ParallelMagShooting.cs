using UnityEngine;
using System.Collections;

public class ParallelMagShooting : MonoBehaviour 
{
	public KeyCode FireKey;
	public float ShootingInterval;
	public Transform Nuzzle;
	public float BulletForce;

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
		if (Input.GetKey(FireKey) && timePassedfFromLastShot > ShootingInterval) 
		{
			_lastTimeShooting = Time.time;

			Vector2 nuzzleRotation = Nuzzle.localRotation * Nuzzle.up;

			RaycastHit2D hit = Physics2D.Raycast(Nuzzle.position, nuzzleRotation, 100, ~(1 << LayerMask.NameToLayer("TankLayer")));
			if (hit.collider != null) 
			{
				Rigidbody2D collidedRigidBody = hit.collider.gameObject.GetComponent<Rigidbody2D> ();
				if (collidedRigidBody != null) 
				{
					collidedRigidBody.AddForce(nuzzleRotation * BulletForce);
				}
			}
		}
	}
	// ================================================================================================ //
}
