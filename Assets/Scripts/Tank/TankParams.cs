using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TankParams : MonoBehaviour 
{
    private Text _tankParamsDescriptionText = null;

    private Dictionary<string, TankParam> _params = new Dictionary<string, TankParam>();



    // ======================================================================================================================================== //
    void Awake()
    {
        _params["TurretRotateSpeed"] =   new TankParam("TurretRotateSpeed",  90.0F,  50.0F,  30.0F);
        _params["TankTurnSpeed"] =       new TankParam("TankTurnSpeed",      180.0F, 30.0F,  20.0F);
        _params["TankSpeed"] =           new TankParam("TankSpeed",          3.0F,   2.0F,   100.0F);
        _params["MagFireRate"] =         new TankParam("MagFireRate",        0.05F,  0.05F,  50.0F);
        _params["MagRange"] =            new TankParam("MagRange",           3.0F,   2.0F,   100.0F);
        _params["MagAccuracy"] =         new TankParam("MagAccuracy",        0.05F,  0.05F,  60.0F);
    }
    // ======================================================================================================================================== //
	void Start () 
    {
        
	}
    // ======================================================================================================================================== //
	void Update () 
    {
        showTankParamsDescriptionInUI();
	}
    // ======================================================================================================================================== //
    public float GetParam(string name)
    {
        return _params[name].CalculatedValue;
    }
    // ======================================================================================================================================== //
    public void AddBonus2Param(string name, float bonus)
    {
        _params[name].Bonus += bonus;
    }
    // ======================================================================================================================================== //
    public void AddPercentBonus2Param(string name, float percentBonus)
    {
        _params[name].PercentBonus += percentBonus;
    }
    // ======================================================================================================================================== //
    public TankParam GetRandomTankParam()
    {
        List<string> dicList = _params.Keys.ToList();
        string randomName = dicList[Random.Range(0, dicList.Count)];

        float randomBonus = Random.Range(-_params[randomName].MaxBonus, _params[randomName].MaxBonus);
        float randomPercentBonus = Random.Range(-_params[randomName].MaxPercentBonus, _params[randomName].MaxPercentBonus);

        TankParam param = new TankParam(randomName, randomBonus, randomPercentBonus);
        return param;
    }
    // ======================================================================================================================================== //
    private void showTankParamsDescriptionInUI()
    {
        if (_tankParamsDescriptionText == null)
        {
            GameObject descriptionTextObj = GameObject.Find("TankParamsDescriptionText");
            if (descriptionTextObj != null)
            {
                _tankParamsDescriptionText = descriptionTextObj.GetComponent<Text>();
            }
        }
        if (_tankParamsDescriptionText == null)
            return;

        _tankParamsDescriptionText.text = "";

        foreach (var item in _params)
        {
            _tankParamsDescriptionText.text += item.Key;
            _tankParamsDescriptionText.text += ": ";
            _tankParamsDescriptionText.text += GetParam(item.Key);
            _tankParamsDescriptionText.text += "\n";
        }
    }
    // ======================================================================================================================================== //
}
