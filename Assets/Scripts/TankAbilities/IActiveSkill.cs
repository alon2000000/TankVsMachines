using System.Collections;
using UnityEngine;

public interface IActiveSkill : ISkill
{
    float Cost{ get; }
    bool IsReady{ get; }

    float MaxCooldown{ get; }
    float Cooldown{ get; }
}