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
    public float MaxHP = 100.0F;
    public float HP = 100.0F;
    public float MaxEnergy = 100.0F;
    public float Energy = 100.0F;

    public float MaxShield = 100.0F;
    public float Shield = 100.0F;
    public float ShieldDurability = 5.0F;
    public float ShieldVsPenetration = 25.0F;
    public float ShieldAbsorption = 25.0F;
    public float ShieldWeight = 20.0F;

    public GameObject ExplosionObj;

    // ======================================================================================================================================== //
    void Awake()
    {
        _params["TurretRotateSpeed"] =   new TankParam("TurretRotateSpeed",  90.0F  );
        _params["TankTurnSpeed"] =       new TankParam("TankTurnSpeed",      180.0F );
        _params["TankSpeed"] =           new TankParam("TankSpeed",          3.0F   );
        _params["MagFireRate"] =         new TankParam("MagFireRate",        0.05F  );
        _params["MagRange"] =            new TankParam("MagRange",           3.0F   );
        _params["MagAccuracy"] =         new TankParam("MagAccuracy",        0.05F  );
        _params["TeleportLevel"] =       new TankParam("TeleportLevel",      0.0F   );

        // shield skill
        _params["MaxShield"] =              new TankParam("MaxShield",              MaxShield);
        _params["Shield"] =                 new TankParam("Shield",                 Shield);
        _params["ShieldDurability"] =       new TankParam("ShieldDurability",       ShieldDurability);
        _params["ShieldVsPenetration"] =    new TankParam("ShieldVsPenetration",    ShieldVsPenetration);
        _params["ShieldAbsorption"] =       new TankParam("ShieldAbsorption",       ShieldAbsorption);
        _params["ShieldWeight"] =           new TankParam("ShieldWeight",           ShieldWeight);
    }
    // ======================================================================================================================================== //
	void Start () 
    {
        
	}
    // ======================================================================================================================================== //
	void Update () 
    {
        if (HP <= 0)
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
    public float Get(string name)
    {
        return _params[name].Value;
    }
    // ======================================================================================================================================== //
    public float Set(string name, float val)
    {
        return _params[name].OriginalValue = val;
    }
    // ======================================================================================================================================== //
    public void AddReward(TankParamReward reward)
    {
        if (reward.Type == TankParamReward.RewardType.ADDITION)
            _params[reward.Name].Bonus += reward.Value;
        if (reward.Type == TankParamReward.RewardType.PERCENT)
            _params[reward.Name].PercentBonus += reward.Value;
    }
    // ======================================================================================================================================== //
    public void RemoveReward(TankParamReward reward)
    {
        if (reward.Type == TankParamReward.RewardType.ADDITION)
            _params[reward.Name].Bonus -= reward.Value;
        if (reward.Type == TankParamReward.RewardType.PERCENT)
            _params[reward.Name].PercentBonus -= reward.Value;
    }
    // ======================================================================================================================================== //
}
