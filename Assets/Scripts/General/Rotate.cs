using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour 
{
	public float RotateSpeed;
	// ================================================================================================ //
	void Start () 
	{
	
	}
	// ================================================================================================ //
	void Update () 
	{
		this.transform.RotateAround (this.transform.position, this.transform.forward, RotateSpeed * Time.deltaTime);
	}
	// ================================================================================================ //
}
