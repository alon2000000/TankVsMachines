using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour 
{
	public Texture2D CursorTexture;

	public GameObject DraggedObject = null;
	// ================================================================================================ //
	void Start () 
	{
		Cursor.SetCursor (CursorTexture, new Vector2(16.0F, 16.0F), CursorMode.Auto);
	}
	// ================================================================================================ //
	void Update () 
	{
		// Esc to quit
		if (Input.GetKey (KeyCode.Escape))
			Application.Quit ();

		// Dragged object
		if (DraggedObject != null) 
		{
			Vector3 mousePos = Input.mousePosition;
			mousePos.z = -(DraggedObject.transform.position.x - Camera.main.transform.position.x);
			Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
			DraggedObject.transform.position = worldPos;
		}
		if (Input.GetMouseButtonUp (0)) 
		{
			DraggedObject = null;
		}
	}
	// ================================================================================================ //
}
