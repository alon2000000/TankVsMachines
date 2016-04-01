using UnityEngine;
using System.Collections;

public class ParallelMagShooting : MonoBehaviour 
{
	public float ShootingInterval;
	public Transform Nuzzle;
	public float BulletForce;
	public Transform Flash;
	public GameObject HitEffect;
	public float ShootDistance;
	public float ErrorShootDistance;
	public float ErrorShootDir;

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
		if (Input.GetMouseButton(0) && timePassedfFromLastShot > ShootingInterval) 
		{
			// show flash effect
			Flash.GetComponent<Renderer>().enabled = true;

			_lastTimeShooting = Time.time;

			Vector2 nuzzleRotation = Nuzzle.localRotation * Nuzzle.up;
			Vector2 nuzzleRotationWithError = nuzzleRotation + Random.insideUnitCircle * ErrorShootDir;
			float shootDistanceWithError = ShootDistance + Random.value * ErrorShootDistance;

            LayerMask layerMask = (1 << LayerMask.NameToLayer("TankLayer"));
            layerMask |= (1 << LayerMask.NameToLayer("LootOnGroundLayer"));
            layerMask |= (1 << LayerMask.NameToLayer("LootInInventoryLayer"));
            layerMask = ~layerMask;

            RaycastHit2D hit = Physics2D.Raycast (Nuzzle.position, nuzzleRotationWithError, shootDistanceWithError, layerMask);

			if (hit.collider != null) 
			{
				// hit effect
				Instantiate (HitEffect, hit.point, Quaternion.identity);

				Rigidbody2D collidedRigidBody = hit.collider.gameObject.GetComponent<Rigidbody2D> ();
				if (collidedRigidBody != null) 
				{
					collidedRigidBody.AddForceAtPosition (nuzzleRotationWithError * BulletForce, hit.point);
				}
			} 
			else 
			{
				// hit effect
				Vector3 hitPosition = Nuzzle.position + new Vector3(nuzzleRotationWithError.x, nuzzleRotationWithError.y, 0.0F) * shootDistanceWithError;
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
