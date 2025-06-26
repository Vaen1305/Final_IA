using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("IA SC/Conditional")]
public class ActionIsHungry : ActionNode
{
    public override TaskStatus OnUpdate()
    {
        var healthJabali = MyHealth as HealthJabali;
        if (healthJabali != null)
        {
            return healthJabali.IsHungry ? TaskStatus.Success : TaskStatus.Failure;
        }
        return TaskStatus.Failure;
    }
}
