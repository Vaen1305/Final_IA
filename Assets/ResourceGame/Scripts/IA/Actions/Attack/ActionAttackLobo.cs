using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("IA SC/Node Attack")]
public class ActionAttackLobo : ActionNodeActions
{
    public override TaskStatus OnUpdate()
    {
        if (_AICharacterAction.health.IsDead)
            return TaskStatus.Failure;

        var loboAction = _AICharacterAction as AICharacterActionLobo;
        if (loboAction != null)
        {
            loboAction.Attack();
            return TaskStatus.Success;
        }
        
        return TaskStatus.Failure;
    }
}
