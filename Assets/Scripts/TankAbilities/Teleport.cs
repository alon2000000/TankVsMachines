using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour 
{
    public TankParams Params;
    public KeyCode Key;
    public Texture2D Image;
    public int Experience;
    // ======================================================================================================================================== //
	void Start () 
    {
	
	}
    // ======================================================================================================================================== //
	void Update () 
    {
        /*_params["TeleportLevel"] =      new TankParam("TeleportLevel",      TeleportLevel);
        _params["TeleportCost"] =       new TankParam("TeleportCost",       TeleportCost);
        _params["TeleportCooldown"] =   new TankParam("TeleportCooldown",   TeleportCooldown);
        _params["TeleportDistance"] =   new TankParam("TeleportDistance",   TeleportDistance);*/
        if (!Input.GetKeyDown(Key))
            return;

        int level = Mathf.RoundToInt(Params.Get("TeleportLevel"));
        if (level <= 0)
            return;

        if (Params.Get("Energy") < Params.Get("TeleportCost"))
            return;

        float distance = Vector2.Distance(gameObject.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));

        if (Params.Get("TeleportDistance") >= distance)
        {
            gameObject.transform.position = new Vector3(
                Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 
                Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 
                gameObject.transform.position.z);
        }
        else
        {
            Vector2 dir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position).normalized;
            Vector2 newPosition = dir * Params.Get("TeleportDistance");
            gameObject.transform.position = newPosition;
        }

        Params.Add("Energy", -Params.Get("TeleportCost"));
	}
    // ======================================================================================================================================== //
}
