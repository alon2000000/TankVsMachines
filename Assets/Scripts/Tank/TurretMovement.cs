using UnityEngine;
using System.Collections;

public class TurretMovement : MonoBehaviour 
{
	public Transform Turret;

    public TankParams TankParamsScript;

	// ================================================================================================ //
	void Start () 
	{

	}
	// ================================================================================================ //
	void Update () 
	{
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = -(transform.position.x - Camera.main.transform.position.x);
		Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        float speed = -TankParamsScript.GetParam("TurretRotateSpeed");
		float step = speed * Time.deltaTime;

		float angle = Mathf.Atan2 (transform.position.x - worldPos.x, transform.position.y - worldPos.y) * Mathf.Rad2Deg;

		Quaternion rotateTo = Quaternion.AngleAxis(-angle, Vector3.forward);

		// need to fix the vibrating barrel bug
		/*float angleBetweenRotations = Quaternion.Angle (Turret.rotation, rotateTo);
		Debug.Log (180-angleBetweenRotations);
		if (180-angleBetweenRotations > step)*/

		Turret.rotation = Quaternion.RotateTowards(Turret.rotation, rotateTo, step);
	}
	// ================================================================================================ //
}
