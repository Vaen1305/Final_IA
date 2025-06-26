using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime.Tasks;
[TaskCategory("IA SC/ Node View")]
public class ActionNotViewAllie : ActionView
{
    public override void OnStart()
    {
        base.OnStart();
    }

    public override TaskStatus OnUpdate()
    {
        if (_AICharacterVehicle != null && _AICharacterVehicle.health.IsDead)
        {
            return TaskStatus.Failure;
        }

        if ((_AICharacterVehicle == null || _AICharacterVehicle._VisionSensor.AlliedView == null) &&
            (_AICharacterAction == null || _AICharacterAction._VisionSensor.AlliedView == null))
        {
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }
}