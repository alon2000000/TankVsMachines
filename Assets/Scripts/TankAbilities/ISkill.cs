using System.Collections;
using UnityEngine;

public enum SkillState
{
    NOT_READY,
    READY,
    ACTION,
    COOLDOWN
}

public interface ISkill
{
    SkillState State{ get; set; }

    KeyCode Key{ get; set; }

    float Cost{ get; }

    float MaxActionTime{ get; }
    float ActionTime{ get; }

    float MaxCooldown{ get; }
    float Cooldown{ get; }
}
