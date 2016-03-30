using UnityEngine;
using System.Collections;

public class TileMouseInput : MonoBehaviour 
{

	// ======================================================================================================================================== //
	void Start () 
	{
	
	}
	// ======================================================================================================================================== //
	void Update () 
	{
	
	}
	// ======================================================================================================================================== //
	void OnMouseEnter()
	{
		//GetComponent<SpriteRenderer>().color = Color.red;
	}
	// ======================================================================================================================================== //
	void OnMouseExit()
	{
		//GetComponent<SpriteRenderer>().color = GetComponent<Tile>().Color;
	}
	// ======================================================================================================================================== //
	void OnMouseDown()
	{
		// change select state
		GetComponent<Tile>().IsSelected = !GetComponent<Tile>().IsSelected;

		string state = GetComponent<Tile>().IsSelected ? "Selected!" : "Deselected...";
		Debug.Log(GetComponent<Tile>().PosX.ToString() +","+ GetComponent<Tile>().PosY.ToString() + " " + state);
	}
	// ======================================================================================================================================== //
}
