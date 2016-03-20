using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour 
{
	public Texture2D CursorTexture;
	// ================================================================================================ //
	void Start () 
	{
		Cursor.SetCursor (CursorTexture, new Vector2(16.0F, 16.0F), CursorMode.Auto);
	}
	// ================================================================================================ //
	void Update () 
	{
		if (Input.GetKey (KeyCode.Escape))
			Application.Quit ();
	}
	// ================================================================================================ //
}
