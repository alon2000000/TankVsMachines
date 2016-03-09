using UnityEngine;
using System.Collections;

public class ShootShell : MonoBehaviour 
{
	public Transform Nuzzle;
	public GameObject Shell;
	public float ShellForce;
	public float ShellLifeTime;

	// ================================================================================================ //
	void Start () 
	{
		
	}
	// ================================================================================================ //
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			// create
			GameObject shell = (GameObject)Instantiate(Shell, Nuzzle.position, Nuzzle.rotation);

			// make it to not collide with the shooter
			Physics2D.IgnoreCollision (shell.GetComponent<Collider2D> (), this.GetComponent<Collider2D> ());

			// add force in nuzzle direction
			Vector2 force = shell.transform.up * ShellForce;
			shell.GetComponent<Rigidbody2D>().AddForce(force);

			// destroy after life time
			Destroy (shell, ShellLifeTime);
		}
	}
	// ================================================================================================ //
}
