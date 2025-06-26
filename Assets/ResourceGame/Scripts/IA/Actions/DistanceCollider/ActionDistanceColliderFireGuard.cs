using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TaskCategory("IA SC/ Node Range / Eye")]
public class ActionDistanceColliderFireGuard : ActionDistanceColliderFire
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
            if (_AICharacterVehicle._VisionSensor is VisionSensorGuard)
            {
                if (((VisionSensorGuard)_AICharacterVehicle._VisionSensor).FireVision.InSight)
                {
                    return TaskStatus.Success;
                }
            }
        }
        return TaskStatus.Failure;
    }
}
