using UnityEngine;
using System.Collections;

public class BarrelShooting : MonoBehaviour 
{
	public Transform Nuzzle;
	public GameObject Ammo;
	public float ShootingForce;
	public float AmmoLifeTime;

	// ================================================================================================ //
	void Start () 
	{
	
	}
	// ================================================================================================ //
	void Update () 
	{
        // return if in inventory
        if (Mathf.RoundToInt(Time.timeScale) == 0)
            return;

        if (Input.GetMouseButtonDown(1)) // left click
		{
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
	// ================================================================================================ //
}
