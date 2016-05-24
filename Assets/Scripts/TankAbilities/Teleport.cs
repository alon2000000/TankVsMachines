using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour 
{
    public TankParams Params;
    public KeyCode Key;
    public Texture2D Image;
    public int Level;
    public int Experience;
    public float Cost = 15.0F;
    // ======================================================================================================================================== //
	void Start () 
    {
	
	}
    // ======================================================================================================================================== //
	void Update () 
    {
        if (Input.GetKeyDown(Key))
        {
            if (Params.Get("Energy") >= Cost)
            {
                gameObject.transform.position = new Vector3(
                    Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 
                    Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 
                    gameObject.transform.position.z);

                Params.Add("Energy", -Cost);
            }
        }
	}
    // ======================================================================================================================================== //
}
