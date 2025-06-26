using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("IA SC/Node Move")]
public class ActionRunAway : ActionNodeVehicle
{
    public override TaskStatus OnUpdate()
    {
        if (_AICharacterVehicle.health.IsDead)
        {
            return TaskStatus.Failure;
        }

        _AICharacterVehicle.MoveToEvadeEnemy();
        return TaskStatus.Running;
    }
}
