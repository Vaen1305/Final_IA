using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TaskCategory("IA SC/ Node Range / Eye")]
public class ActionNotDistanceColliderFireGuard : ActionNode
{
    public override TaskStatus OnUpdate()
    {
        if (_VisionSensor == null) return TaskStatus.Failure;
        if (_VisionSensor.health.IsDead) return TaskStatus.Failure;

        var guardVision = _VisionSensor as VisionSensorGuard;
        if (guardVision != null)
        {
            if (!guardVision.FireVision.InSight)
            {
                return TaskStatus.Success;
            }
        }
        return TaskStatus.Failure;
    }
}
