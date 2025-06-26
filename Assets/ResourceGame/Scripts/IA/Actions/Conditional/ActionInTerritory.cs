using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("IA SC/Conditional")]
public class ActionInTerritory : ActionNode
{
    public override TaskStatus OnUpdate()
    {
        var healthJabali = MyHealth as HealthJabali;
        if (healthJabali != null)
        {
            return healthJabali.IsInTerritory ? TaskStatus.Success : TaskStatus.Failure;
        }
        return TaskStatus.Failure;
    }
}
