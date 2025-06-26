using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TaskCategory("IA SC/Node Attack")]
public class ActionFireGuard : ActionNodeActions
{
    public override TaskStatus OnUpdate()
    {
        if (_AICharacterAction.health.IsDead)
            return TaskStatus.Failure;

        var guardAction = _AICharacterAction as AICharacterActionLandGuard;
        if (guardAction != null)
        {
            guardAction.Shoot();
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
