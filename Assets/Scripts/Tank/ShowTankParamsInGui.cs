using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowTankParamsInGui : MonoBehaviour 
{
    public TankParams Params;
    public Text TankParamsDescriptionText;
    public Text ChipsAmountText;
    public Slider HealthBar;
    public Slider ShieldBar;
    public Slider EnergyBar;

    // ======================================================================================================================================== //
	void Start () 
    {
	
	}
    // ======================================================================================================================================== //
	void Update () 
    {
        showTankParamsDescriptionInUI();
        showChipsCashAmountInUI();
        showBars();
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
        ChipsAmountText.text = Params.Get("Cash").ToString();
    }
    // ======================================================================================================================================== //
    private void showBars()
    {
        HealthBar.value = Params.Get("HP");
        HealthBar.maxValue = Params.Get("MaxHP");

        ShieldBar.value = Params.Get("Shield");
        ShieldBar.maxValue = Params.Get("MaxShield");

        EnergyBar.value = Params.Get("Energy");
        EnergyBar.maxValue = Params.Get("MaxEnergy");
    }
    // ======================================================================================================================================== //
}
