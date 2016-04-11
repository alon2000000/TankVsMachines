using UnityEngine;
using System.Collections;

public class KeysPressing : MonoBehaviour 
{
    private bool _isInInventory = false;
    public GameObject ChipsBagObj;
    public GameObject ChipsBoardObj;
    public GameObject InventoryBackgroungObj;
    public GameObject UiCanvas;
    // ======================================================================================================================================== //
    void Start () 
    {

    }
    // ======================================================================================================================================== //
    void Update () 
    {
        // Esc to quit
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit ();

        // I to inventory
        if (Input.GetKeyDown(KeyCode.I))
        {
            _isInInventory = !_isInInventory;

            Time.timeScale = _isInInventory ? 0.0F : 1.0F;

            InventoryBackgroungObj.SetActive(_isInInventory);
            ChipsBagObj.SetActive(_isInInventory);
            ChipsBoardObj.SetActive(_isInInventory);
            UiCanvas.SetActive(_isInInventory);
        }
    }
    // ======================================================================================================================================== //
}