using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("IA SC/Conditional")]
public class ActionIsTired : ActionNode
{
    public override TaskStatus OnUpdate()
    {
        if (MyHealth is HealthCiervo healthCiervo)
        {
            return healthCiervo.IsTired ? TaskStatus.Success : TaskStatus.Failure;
        }
        if (MyHealth is HealthLobo healthLobo)
        {
            return healthLobo.IsTired ? TaskStatus.Success : TaskStatus.Failure;
        }
        return TaskStatus.Failure;
    }
}
