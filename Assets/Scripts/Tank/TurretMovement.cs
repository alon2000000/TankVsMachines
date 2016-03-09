using UnityEngine;
using System.Collections;

public class TurretMovement : MonoBehaviour 
{
	public Transform Turret;
	public float RotateSpeed;
	// ================================================================================================ //
	void Start () 
	{
	
	}
	// ================================================================================================ //
	void Update () 
	{
		// turn right & left
		if (Input.GetKey (KeyCode.D)) 
		{
			Turret.gameObject.transform.Rotate( new Vector3(0, 0, -RotateSpeed * Time.deltaTime), Space.Self );
		}
		if (Input.GetKey (KeyCode.A)) 
		{
			Turret.gameObject.transform.Rotate( new Vector3(0, 0, RotateSpeed * Time.deltaTime), Space.Self );
		}
	}
	// ================================================================================================ //
}
