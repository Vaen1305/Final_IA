using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.UIElements;

[TaskCategory("IA SC/ Node Range / Eye")]
public class ActionDistanceColliderCave : ActionRange
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
                if (((VisionSensorCivil)_AICharacterVehicle._VisionSensor).ResourcesVision.InSight)
                    return TaskStatus.Success;
            }
        }                        
        return TaskStatus.Failure; 
    } 
}