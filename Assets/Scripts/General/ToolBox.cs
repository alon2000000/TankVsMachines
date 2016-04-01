using UnityEngine;
using System.Collections;

public class ToolBox : MonoBehaviour 
{
    private static ToolBox _instance = null;
    public static ToolBox Instance
    {
        get
        { 
            if (_instance == null)
                _instance = new ToolBox();
            return _instance;
        }
    }

    public ChipsBag ChipsBag;
    public ChipsBoard ChipsBoard;
    // ======================================================================================================================================== //
	void Start () 
    {
	
	}
    // ======================================================================================================================================== //
	
	void Update () 
    {
	
	}
    // ======================================================================================================================================== //
}
