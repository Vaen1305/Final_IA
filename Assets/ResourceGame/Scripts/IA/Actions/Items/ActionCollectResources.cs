using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("IA SC/Node Items Action")]
public class ActionCollectResources : ActionNodeActions
{
    public override void OnStart()
    {
        base.OnStart();
    }
    public override TaskStatus OnUpdate()
    {
        if (_AICharacterVehicle.health.IsDead)
        {
            return TaskStatus.Failure;
        }
        else
        {
            if (_AICharacterVehicle._VisionSensor is VisionSensorCivil)
            {
                VisionSensorCivil visionSensor = (VisionSensorCivil)_AICharacterVehicle._VisionSensor;

                if (visionSensor.ResourcesVision.InSight)
                {
                    ItemResource current = (ItemResource)visionSensor.ResourceView;
                    HealthCivil civil = (HealthCivil)visionSensor.health;
                    current.CollectResource(civil);
                    return TaskStatus.Success;
                }
            }
            return TaskStatus.Failure;
        }
    }
}
