using UnityEngine;
using System.Collections;

public class LevelCreator : MonoBehaviour 
{
	public GameObject Enemy;
	public int EnemiesNumber;
	public int EnemiesRadius;

	// ================================================================================================ //
	void Start () 
	{
		for (int i = 0; i < EnemiesNumber; i++) 
		{
			Vector3 pos = Random.insideUnitCircle * EnemiesRadius;
			Instantiate (Enemy, pos, Quaternion.identity);
		}
	}
	// ================================================================================================ //
	void Update () 
	{
	
	}
	// ================================================================================================ //
}
