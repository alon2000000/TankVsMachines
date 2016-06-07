using System.Collections;
using UnityEngine;

public interface IPassiveSkill : ISkill
{
    bool IsActive{ get; set; }
    float CostPerSec{ get; }
}
