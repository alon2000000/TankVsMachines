using UnityEngine;
using System.Collections;

public class WeaponControl : MonoBehaviour 
{
	private Transform _currentWeapon;
	private WeaponProperties _currentWeaponProperties;
	private float _originBarrelScale;

	public Transform Turret;
	public Transform Mag;
	public Transform Mortar;

	public Transform Barrel;
	// ================================================================================================ //
	void Start () 
	{
		_currentWeapon = Turret;
		_currentWeaponProperties = _currentWeapon.gameObject.GetComponent<WeaponProperties>();
		_originBarrelScale = Barrel.transform.localScale.y;
	}
	// ================================================================================================ //
	void Update () 
	{
		toggleWeaponUpdate();
		rotateWeaponUpdate();
		rotateBarrel();
		shootUpdate();
	}
	// ================================================================================================ //
	private void toggleWeaponUpdate()
	{
		if (Input.GetKeyDown (KeyCode.E)) 
		{
			if (_currentWeapon == Turret)
				_currentWeapon = Mag;
			else if (_currentWeapon == Mag)
				_currentWeapon = Mortar;
			else if (_currentWeapon == Mortar)
				_currentWeapon = Turret;
			_currentWeaponProperties = _currentWeapon.gameObject.GetComponent<WeaponProperties>();
		}
		if (Input.GetKeyDown (KeyCode.Q)) 
		{
			if (_currentWeapon == Turret)
				_currentWeapon = Mortar;
			else if (_currentWeapon == Mag)
				_currentWeapon = Turret;
			else if (_currentWeapon == Mortar)
				_currentWeapon = Mag;
			_currentWeaponProperties = _currentWeapon.gameObject.GetComponent<WeaponProperties>();
		}
	}
	// ================================================================================================ //
	private void rotateBarrel()
	{
		if (Input.GetKey (KeyCode.W)) 
		{
			if (Barrel.transform.localScale.y < _originBarrelScale)
				Barrel.transform.localScale += new Vector3( 0.0F,_currentWeaponProperties.PitchSpeed * Time.deltaTime,0.0F );
		}
		if (Input.GetKey (KeyCode.S)) 
		{
			if (Barrel.transform.localScale.y > _originBarrelScale - _currentWeaponProperties.MaxPitch)
				Barrel.transform.localScale -= new Vector3( 0.0F,_currentWeaponProperties.PitchSpeed * Time.deltaTime,0.0F );
		}
	}
	// ================================================================================================ //
	private void rotateWeaponUpdate()
	{
		// turn right & left
		if (Input.GetKey (KeyCode.D)) 
		{
			_currentWeapon.gameObject.transform.Rotate( new Vector3(0, 0, -_currentWeaponProperties.RotateSpeed * Time.deltaTime), Space.Self );
		}
		if (Input.GetKey (KeyCode.A)) 
		{
			_currentWeapon.gameObject.transform.Rotate( new Vector3(0, 0, _currentWeaponProperties.RotateSpeed * Time.deltaTime), Space.Self );
		}
	}
	// ================================================================================================ //
	private void shootUpdate()
	{
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			// create
			GameObject shell = (GameObject)Instantiate(_currentWeaponProperties.Ammo, _currentWeaponProperties.Nuzzle.position, _currentWeaponProperties.Nuzzle.rotation);

			// make it to not collide with the shooter
			Physics2D.IgnoreCollision (shell.GetComponent<Collider2D> (), this.GetComponent<Collider2D> ());

			// add force in nuzzle direction
			Vector2 force = shell.transform.up * _currentWeaponProperties.ShootingForce;
			shell.GetComponent<Rigidbody2D>().AddForce(force);

			// destroy after life time
			Destroy (shell, _currentWeaponProperties.AmmoLifeTime);
		}
	}
	// ================================================================================================ //
}
