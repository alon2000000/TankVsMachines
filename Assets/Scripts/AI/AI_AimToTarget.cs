using UnityEngine;
using System.Collections;

public class AI_AimToTarget : MonoBehaviour 
{
	public Transform Turret;
	public float DetectionRange;

	private GameObject _playerTank = null;

	// ================================================================================================ //
	void Start () 
	{

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

		float speed = -90.0F;
		float step = speed * Time.deltaTime;

		float angle = Mathf.Atan2 (transform.position.x - playerPos.x, transform.position.y - playerPos.y) * Mathf.Rad2Deg;

		Quaternion rotateTo = Quaternion.AngleAxis(-angle, Vector3.forward);

		Turret.rotation = Quaternion.RotateTowards(Turret.rotation, rotateTo, step);
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
