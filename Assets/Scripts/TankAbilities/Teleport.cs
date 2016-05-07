using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour 
{
    public KeyCode Key;
    public Texture2D Image;
    public int Level;
    public int Experience;
    // ======================================================================================================================================== //
	void Start () 
    {
	
	}
    // ======================================================================================================================================== //
	void Update () 
    {
        if (Input.GetKeyDown(Key))
        {
            gameObject.transform.position = new Vector3(
                Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 
                Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 
                gameObject.transform.position.z);
        }
	}
    // ======================================================================================================================================== //
}
