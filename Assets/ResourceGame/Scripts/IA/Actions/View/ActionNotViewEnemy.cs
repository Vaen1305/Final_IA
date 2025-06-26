using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TaskCategory("IA SC/ Node View")]
public class ActionNotViewEnemy : ActionView
{
    public override void OnStart()
    {
        base.OnStart();
    }

    public override TaskStatus OnUpdate()
    {
        if (_AICharacterVehicle != null)
        {
            if (_AICharacterVehicle.health.IsDead)
            {
                return TaskStatus.Failure;
            }
        }

        if (_VisionSensor != null && _VisionSensor.EnemyView == null)
        {
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }
}
