using UnityEngine;
using System.Collections;

public class TankParamReward
{
    public enum RewardType
    {
        ADDITION,
        PERCENT
    }

    public string Name;
    public float Value;
    public RewardType Type;
    // ======================================================================================================================================== //
    public TankParamReward(string name, float rewardValue, RewardType rewardType)
    {
        Name = name;
        Value = rewardValue;
        Type = rewardType;
    }
    // ======================================================================================================================================== //
}
