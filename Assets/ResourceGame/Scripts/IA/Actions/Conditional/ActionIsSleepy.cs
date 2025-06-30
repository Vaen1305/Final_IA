using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("IA SC/Conditional")]
public class ActionIsSleepy : ActionNode
{
    public override TaskStatus OnUpdate()
    {
        if (MyHealth is HealthCiervo healthCiervo)
        {
            bool isSleepy = healthCiervo.IsSleepy;
            Debug.Log($"😴 {gameObject.name}: ¿Tiene sueño? {isSleepy} (Sueño: {healthCiervo.sleepiness:F1})");
            return isSleepy ? TaskStatus.Success : TaskStatus.Failure;
        }
        if (MyHealth is HealthLobo healthLobo)
        {
            bool isSleepy = healthLobo.IsSleepy;
            Debug.Log($"😴 {gameObject.name}: ¿Tiene sueño? {isSleepy} (Sueño: {healthLobo.sleepiness:F1})");
            return isSleepy ? TaskStatus.Success : TaskStatus.Failure;
        }
        if (MyHealth is HealthJabali healthJabali)
        {
            bool isSleepy = healthJabali.IsSleepy;
            Debug.Log($"😴 {gameObject.name}: ¿Tiene sueño? {isSleepy} (Sueño: {healthJabali.sleepiness:F1})");
            return isSleepy ? TaskStatus.Success : TaskStatus.Failure;
        }
        return TaskStatus.Failure;
    }
}
