using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("IA SC/ Node Range / Eye")]
public class ActionNotDistanceColliderResource : ActionView
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
        if (_AICharacterVehicle != null)
        {
            if (_AICharacterVehicle._VisionSensor is VisionSensorCivil)
            {
                if (!((VisionSensorCivil)_AICharacterVehicle._VisionSensor).ResourcesVision.InSight)
                {
                    return TaskStatus.Success;
                }
            }
        }
        return TaskStatus.Failure;
    }
}
