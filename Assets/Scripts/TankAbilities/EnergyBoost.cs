using UnityEngine;
using System.Collections;

public class EnergyBoost : MonoBehaviour 
{
    //public KeyCode Key = KeyCode.Alpha2;//KeyCode.None;

    private TankParams _params;
    // ======================================================================================================================================== //
	void Start () 
    {
        _params = Toolbox.Instance.TankParams;
	}
    // ======================================================================================================================================== //
	void Update () 
    {
        KeyCode key = gameObject.GetComponentInParent<Loot>().SkillKey;

        if (key == KeyCode.None)
            return;
        if (!Input.GetKeyDown(key))
            return;

        int level = Mathf.RoundToInt(_params.Get("EnergyBoostLevel"));
        if (level <= 0)
            return;

        if (_params.CashChips < _params.Get("EnergyBoostCost"))
            return;

        _params.CashChips -= Mathf.RoundToInt(_params.Get("EnergyBoostCost"));

        float newEnergyAmount = Mathf.Clamp(_params.Get("Energy") + _params.Get("EnergyBoostValue"), 0.0F, _params.Get("MaxEnergy"));

        _params.Set("Energy", newEnergyAmount);
	}
    // ======================================================================================================================================== //
}
