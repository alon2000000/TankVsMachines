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
        _params["TurretRotateSpeed"] =   new TankParam("TurretRotateSpeed",  90.0F  );
        _params["TankTurnSpeed"] =       new TankParam("TankTurnSpeed",      180.0F );
        _params["TankSpeed"] =           new TankParam("TankSpeed",          3.0F   );
        _params["MagFireRate"] =         new TankParam("MagFireRate",        0.05F  );
        _params["MagRange"] =            new TankParam("MagRange",           3.0F   );
        _params["MagAccuracy"] =         new TankParam("MagAccuracy",        0.05F  );
        _params["TeleportLevel"] =       new TankParam("TeleportLevel",      0.0F   );
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
    public float Get(string name)
    {
        return _params[name].Value;
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
