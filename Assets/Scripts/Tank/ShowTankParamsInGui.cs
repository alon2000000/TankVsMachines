using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowTankParamsInGui : MonoBehaviour 
{
    public TankParams TankParamsScript;
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

        foreach (var item in TankParamsScript.Params)
        {
            TankParamsDescriptionText.text += item.Key;
            TankParamsDescriptionText.text += ": ";
            TankParamsDescriptionText.text += TankParamsScript.GetParam(item.Key);
            TankParamsDescriptionText.text += "\n";
        }
    }
    // ======================================================================================================================================== //
    private void showChipsCashAmountInUI()
    {
        ChipsAmountText.text = TankParamsScript.CashChips.ToString();;
    }
    // ======================================================================================================================================== //
}
