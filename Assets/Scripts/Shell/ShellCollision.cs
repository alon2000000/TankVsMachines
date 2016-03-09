using UnityEngine;
using System.Collections;

public class ShellCollision : MonoBehaviour 
{
	private bool _isCollided = false;
	private int _framesCounterSinceCollision = 0;
	// ================================================================================================ //
	void Start () 
	{
	
	}
	// ================================================================================================ //
	void Update () 
	{
		if (_isCollided)
			_framesCounterSinceCollision++;
		if (_framesCounterSinceCollision == 3)
			Destroy(gameObject); // try
	}
	// ================================================================================================ //
	void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log ("Collision...");
		_isCollided = true;
		//Destroy(gameObject);
	}
	// ================================================================================================ //
	/*void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log ("Trigger...");
		//if(other.gameObject.tag=="bullet")
		Destroy(gameObject);    
	}*/
	// ================================================================================================ //
}
