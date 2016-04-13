using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TankParams : MonoBehaviour 
{
    public GameObject ChipObj;

    private Dictionary<string, TankParam> _params = new Dictionary<string, TankParam>();
    public Dictionary<string, TankParam> Params
    {
        get{ return _params;}
    }

    public int CashChips = 0;
    public int Life = 100;

    public GameObject ExplosionObj;

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
        if (Life <= 0)
        {
            if (ChipObj != null)
            {
                for (int i = 0; i < Random.Range(1, 5); ++i)
                {
                    Instantiate(ChipObj, transform.position + (Vector3)Random.insideUnitCircle, Quaternion.identity);
                }
            }
            Instantiate(ExplosionObj, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
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


}
