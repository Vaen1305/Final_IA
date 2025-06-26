using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("IA SC/ Node View")]
public class ActionViewResource : ActionView
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
            VisionSensorCivil visionSensorCivil = _AICharacterVehicle._VisionSensor as VisionSensorCivil;
            if (visionSensorCivil.ResourceView != null)
            {
                return TaskStatus.Success;
            }
        }

        if (_AICharacterAction != null)
        {
            VisionSensorCivil visionSensorCivil = _AICharacterAction._VisionSensor as VisionSensorCivil;
            if (visionSensorCivil.ResourceView != null)
            {
                return TaskStatus.Success;
            }
        }

        return TaskStatus.Failure;
    }
}
