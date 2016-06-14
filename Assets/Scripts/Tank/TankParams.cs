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

    //public int CashChips = 0;

    public GameObject ExplosionObj;

    // ======================================================================================================================================== //
    void Awake()
    {
        addParam("Cash", 0.0F);

        addParam("MaxHP", 1000.0F);
        addParam("HP", 1000.0F);

        addParam("MaxEnergy", 100.0F);
        addParam("Energy", 100.0F);
        addParam("EnergyRegeneration", 1.0F);

        addParam("Weight", 10.0F);

        addParam("TurretRotateSpeed", 90.0F);
        addParam("TankTurnSpeed", 180.0F);
        addParam("TankSpeed", 3.0F);
        addParam("MagFireRate", 0.05F);
        addParam("MagRange", 3.0F);
        addParam("MagAccuracy", 0.05F);

        // shield
        addParam("MaxShield", 100.0F);
        addParam("Shield", 100.0F);
        addParam("ShieldDurability", 5.0F);
        addParam("ShieldVsPenetration", 25.0F);
        addParam("ShieldAbsorption", 25.0F);

        // teleport
        addParam("TeleportLevel", 1.0F);
        addParam("TeleportCost", 80.0F);
        addParam("TeleportCooldown", 5.0F); // in sec

        // energy boost
        addParam("EnergyBoostLevel", 1.0F);
        addParam("EnergyBoostCost", 10.0F);
        addParam("EnergyBoostValue", 15.0F);
        addParam("EnergyBoostCooldown", 1.0F);

        // turbo
        addParam("TurboLevel", 1.0F);
        addParam("TurboCost", 25.0F);
        addParam("TurboSpeedMultiplyer", 4.0F);
        addParam("TurboActionTime", 1.0F);
        addParam("TurboCooldown", 10.0F);

        // time master
        addParam("TimeMasterLevel", 1.0F);
        addParam("TimeMasterCost", 50.0F);
        addParam("TimeMasterActionTime", 10.0F);
        addParam("TimeMasterCooldown", 20.0F);
        addParam("TimeMasterTimeMultiplyer", 2.0F);
    }
    // ======================================================================================================================================== //
	void Start () 
    {
        
	}
    // ======================================================================================================================================== //
	void Update () 
    {
        updateCheckLife();
        updateEnergyRegeneration();
	}
    // ======================================================================================================================================== //
    private void addParam(string paramName, float initValue)
    {
        if (_params.Keys.Contains(paramName))
            throw new UnityException("duplication of param "+paramName);
        _params[paramName] =   new TankParam(paramName, initValue);
    }
    // ======================================================================================================================================== //
    public float Get(string name)
    {
        if (!_params.Keys.Contains(name))
            throw new UnityException("param not exists");
        return _params[name].Value;
    }
    // ======================================================================================================================================== //
    public void Set(string name, float val)
    {
        if (!_params.Keys.Contains(name))
            throw new UnityException("param not exists");
        _params[name].OriginalValue = val;
    }
    // ======================================================================================================================================== //
    public void Add(string name, float val)
    {
        if (!_params.Keys.Contains(name))
            throw new UnityException("param not exists");
        _params[name].OriginalValue += val;
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
    private void updateCheckLife()
    {
        if (this.Get("HP") <= 0)
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
    private void updateEnergyRegeneration()
    {
        float regeneratedEnergy = Time.deltaTime * this.Get("EnergyRegeneration");
        if ((this.Get("Energy") + regeneratedEnergy) > this.Get("MaxEnergy"))
            regeneratedEnergy = this.Get("MaxEnergy") - this.Get("Energy");
        this.Add("Energy", regeneratedEnergy);
    }
    // ======================================================================================================================================== //
}
