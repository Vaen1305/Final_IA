using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("IA SC/Node Attack")]
public class ActionAttackJabali : ActionNodeActions
{
    public override TaskStatus OnUpdate()
    {
        if (_AICharacterAction.health.IsDead)
            return TaskStatus.Failure;

        var jabaliAction = _AICharacterAction as AICharacterActionJabali;
        if (jabaliAction != null)
        {
            jabaliAction.Attack();
            return TaskStatus.Success;
        }
        
        return TaskStatus.Failure;
    }
}
