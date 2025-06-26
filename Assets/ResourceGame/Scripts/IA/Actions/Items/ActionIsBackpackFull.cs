using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TaskCategory("IA SC/Node Items Action")]
public class ActionIsBackpackFull : ActionNodeActions
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
                HealthCivil civilHealth = (HealthCivil)_AICharacterVehicle.health;
                if (civilHealth.ResourceBackpack>=civilHealth.maxResourceBackpack)
                {
                    return TaskStatus.Success;
                }
            }
            return TaskStatus.Failure;
        }
    }
}
