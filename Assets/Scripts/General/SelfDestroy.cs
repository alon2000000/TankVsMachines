using UnityEngine;
using System.Collections;

public class SelfDestroy : MonoBehaviour 
{
	public float LifeTime;
	// ================================================================================================ //
	void Start () 
	{
		Destroy(gameObject, LifeTime);
	}
	// ================================================================================================ //
}
