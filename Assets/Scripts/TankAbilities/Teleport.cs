using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour 
{
    //public KeyCode Key = KeyCode.Alpha1;//KeyCode.None;

    private TankParams _params;
    // ======================================================================================================================================== //
	void Start () 
    {
        _params = Toolbox.Instance.TankParams;
	}
    // ======================================================================================================================================== //
	void Update () 
    {
        KeyCode key = gameObject.GetComponentInParent<Loot>().SkillKey;

        if (key == KeyCode.None)
            return;
        if (!Input.GetKeyDown(key))
            return;

        int level = Mathf.RoundToInt(_params.Get("TeleportLevel"));
        if (level <= 0)
            return;

        if (_params.Get("Energy") < _params.Get("TeleportCost"))
            return;

        //float distance = Vector2.Distance(gameObject.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));

        GameObject tankObj = GameObject.Find("Tank");
        tankObj.transform.position = new Vector3(
            Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 
            Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 
            tankObj.transform.position.z);

        _params.Add("Energy", -_params.Get("TeleportCost"));
	}
    // ======================================================================================================================================== //
}
