using UnityEngine;
using System.Collections;

public class BarrelMovement : MonoBehaviour 
{
	public Transform Barrel;
	public float PitchSpeed;
	public float MaxPitch;

	private float _originBarrelScale;
	// ================================================================================================ //
	void Start () 
	{
		_originBarrelScale = Barrel.transform.localScale.y;
	}
	// ================================================================================================ //
	void Update () 
	{
		/*if (Input.GetKey (KeyCode.W)) 
		{
			if (Barrel.transform.localScale.y < _originBarrelScale)
				Barrel.transform.localScale += new Vector3( 0.0F,PitchSpeed * Time.deltaTime,0.0F );
		}
		if (Input.GetKey (KeyCode.S)) 
		{
			if (Barrel.transform.localScale.y > _originBarrelScale - MaxPitch)
				Barrel.transform.localScale -= new Vector3( 0.0F,PitchSpeed * Time.deltaTime,0.0F );
		}*/
	}
	// ================================================================================================ //
}
