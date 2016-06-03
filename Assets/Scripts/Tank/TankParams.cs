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
    public float EnergyRegeneration = 1.0F; // energy unit per sec

    public float Weight = 10.0F;

    public float MaxShield = 100.0F;
    public float Shield = 100.0F;
    public float ShieldDurability = 5.0F;
    public float ShieldVsPenetration = 25.0F;
    public float ShieldAbsorption = 25.0F;

    public float TeleportLevel = 0.0F;
    public float TeleportCost = 50.0F;
    public float TeleportCooldown = 5.0F; // in sec
    public float TeleportDistance = 2.0F;

    public float EnergyBoostLevel = 0.0F;
    public float EnergyBoostCost = 10.0F;
    public float EnergyBoostValue = 20.0F;
    public float EnergyBoostCooldown = 5.0F; // in sec

    public GameObject ExplosionObj;

    // ======================================================================================================================================== //
    void Awake()
    {
        _params["MaxHP"] =      new TankParam("MaxHP",      MaxHP);
        _params["HP"] =         new TankParam("HP",         HP);

        _params["MaxEnergy"] =          new TankParam("MaxEnergy",          MaxEnergy);
        _params["Energy"] =             new TankParam("Energy",             Energy);
        _params["EnergyRegeneration"] = new TankParam("EnergyRegeneration", EnergyRegeneration);

        _params["Weight"] =     new TankParam("Weight",     Weight);

        _params["TurretRotateSpeed"] =   new TankParam("TurretRotateSpeed",  90.0F  );
        _params["TankTurnSpeed"] =       new TankParam("TankTurnSpeed",      180.0F );
        _params["TankSpeed"] =           new TankParam("TankSpeed",          3.0F   );
        _params["MagFireRate"] =         new TankParam("MagFireRate",        0.05F  );
        _params["MagRange"] =            new TankParam("MagRange",           3.0F   );
        _params["MagAccuracy"] =         new TankParam("MagAccuracy",        0.05F  );
        _params["TeleportLevel"] =       new TankParam("TeleportLevel",      0.0F   );

        // shield
        _params["MaxShield"] =              new TankParam("MaxShield",              MaxShield);
        _params["Shield"] =                 new TankParam("Shield",                 Shield);
        _params["ShieldDurability"] =       new TankParam("ShieldDurability",       ShieldDurability);
        _params["ShieldVsPenetration"] =    new TankParam("ShieldVsPenetration",    ShieldVsPenetration);
        _params["ShieldAbsorption"] =       new TankParam("ShieldAbsorption",       ShieldAbsorption);

        // teleport
        _params["TeleportLevel"] =      new TankParam("TeleportLevel",      TeleportLevel);
        _params["TeleportCost"] =       new TankParam("TeleportCost",       TeleportCost);
        _params["TeleportCooldown"] =   new TankParam("TeleportCooldown",   TeleportCooldown);
        _params["TeleportDistance"] =   new TankParam("TeleportDistance",   TeleportDistance);

        // energy boost
        _params["EnergyBoostLevel"] =      new TankParam("EnergyBoostLevel",      EnergyBoostLevel);
        _params["EnergyBoostCost"] =       new TankParam("EnergyBoostCost",       EnergyBoostCost);
        _params["EnergyBoostValue"] =       new TankParam("EnergyBoostValue",       EnergyBoostValue);
        _params["EnergyBoostCooldown"] =   new TankParam("EnergyBoostCooldown",   EnergyBoostCooldown);
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
    public float Get(string name)
    {
        return _params[name].Value;
    }
    // ======================================================================================================================================== //
    public void Set(string name, float val)
    {
        _params[name].OriginalValue = val;
    }
    // ======================================================================================================================================== //
    public void Add(string name, float val)
    {
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
