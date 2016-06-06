using System.Collections;
using UnityEngine;

public interface ISkill
{
    KeyCode Key{ get; set; }

    float Cost{ get; }
    bool IsReady{ get; }

    float MaxCooldown{ get; }
    float Cooldown{ get; }
}
