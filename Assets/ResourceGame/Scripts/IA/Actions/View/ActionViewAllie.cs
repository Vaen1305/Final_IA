using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime.Tasks;
[TaskCategory("IA SC/ Node View")]
public class ActionViewAllie : ActionView
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
            if (_AICharacterVehicle._VisionSensor.AlliedView != null)
            {
                return TaskStatus.Success;
            }
        }

        if (_AICharacterAction != null)
        {
            if (_AICharacterAction._VisionSensor.AlliedView != null)
            {
                return TaskStatus.Success;
            }
        }

        return TaskStatus.Failure;
    }
}