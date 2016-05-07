using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowTankParamsInGui : MonoBehaviour 
{
    public TankParams Params;
    public Text TankParamsDescriptionText;
    public Text ChipsAmountText;

    // ======================================================================================================================================== //
	void Start () 
    {
	
	}
    // ======================================================================================================================================== //
	void Update () 
    {
        showTankParamsDescriptionInUI();
        showChipsCashAmountInUI();
	}
    // ======================================================================================================================================== //
    private void showTankParamsDescriptionInUI()
    {
        TankParamsDescriptionText.text = "";

        foreach (var item in Params.Params)
        {
            TankParamsDescriptionText.text += item.Key;
            TankParamsDescriptionText.text += ": ";
            TankParamsDescriptionText.text += item.Value.Value;
            TankParamsDescriptionText.text += "\n";
        }
    }
    // ======================================================================================================================================== //
    private void showChipsCashAmountInUI()
    {
        ChipsAmountText.text = Params.CashChips.ToString();
    }
    // ======================================================================================================================================== //
}
