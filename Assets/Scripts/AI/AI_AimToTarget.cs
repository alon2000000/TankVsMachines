using UnityEngine;
using System.Collections;

public class AI_AimToTarget : MonoBehaviour 
{
	public Transform Turret;
	public float DetectionRange;
	public float RotateSpeed;
	public float TimeBetweenShots;
	public float MaxAngleToShoot;

	public Transform Nuzzle;
	public GameObject Ammo;
	public float ShootingForce;
	public float AmmoLifeTime;

	private GameObject _playerTank = null;
	private float _lastShotTime;

	// ================================================================================================ //
	void Start () 
	{
		_lastShotTime = Time.time;
	}
	// ================================================================================================ //
	void Update () 
	{
		if (_playerTank == null)
			_playerTank = getPlayerTank ();
		if (_playerTank == null)
			return;

		Vector3 playerPos = _playerTank.transform.position;

		if (Vector3.Distance (playerPos, transform.position) > DetectionRange)
			return;

		float speed = -RotateSpeed;
		float step = speed * Time.deltaTime;

		float angle = Mathf.Atan2 (transform.position.x - playerPos.x, transform.position.y - playerPos.y) * Mathf.Rad2Deg;

		Quaternion rotateTo = Quaternion.AngleAxis(-angle, Vector3.forward);

		Turret.rotation = Quaternion.RotateTowards(Turret.rotation, rotateTo, step);

		// if player in direction - shoot!
		float angleBetweenEnemyAndPlayer = Vector3.Angle (Turret.up, playerPos - Turret.position );
		if  ( angleBetweenEnemyAndPlayer < MaxAngleToShoot) 
		{
			if (Time.time - _lastShotTime >= TimeBetweenShots) 
			{
				_lastShotTime = Time.time;
				// create
				GameObject shell = (GameObject)Instantiate(Ammo, Nuzzle.position, Nuzzle.rotation);

				// make it to not collide with the shooter
				Physics2D.IgnoreCollision (shell.GetComponent<Collider2D> (), this.GetComponent<Collider2D> ());

				// add force in nuzzle direction
				Vector2 force = shell.transform.up * ShootingForce;
				shell.GetComponent<Rigidbody2D>().AddForce(force);

				// destroy after life time
				Destroy (shell, AmmoLifeTime);
			}
		}
	}
	// ================================================================================================ //
	private GameObject getPlayerTank()
	{
		GameObject[] gos = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];
		foreach (GameObject go in gos) 
		{
			if ( go.layer == LayerMask.NameToLayer("TankLayer") )
				return go;
		}
		return null;
	}
	// ================================================================================================ //
}
