using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("IA SC/Conditional")]
public class ActionIsHungry : ActionNode
{
    public override TaskStatus OnUpdate()
    {
        if (MyHealth is HealthCiervo healthCiervo)
        {
            bool isHungry = healthCiervo.IsHungry;
            Debug.Log($"🍽️ {gameObject.name}: ¿Tiene hambre? {isHungry} (Hambre: {healthCiervo.hunger:F1})");
            return isHungry ? TaskStatus.Success : TaskStatus.Failure;
        }
        if (MyHealth is HealthLobo healthLobo)
        {
            bool isHungry = healthLobo.IsHungry;
            Debug.Log($"🍖 {gameObject.name}: ¿Tiene hambre? {isHungry} (Hambre: {healthLobo.hunger:F1})");
            return isHungry ? TaskStatus.Success : TaskStatus.Failure;
        }
        if (MyHealth is HealthJabali healthJabali)
        {
            bool isHungry = healthJabali.IsHungry;
            Debug.Log($"🌿 {gameObject.name}: ¿Tiene hambre? {isHungry} (Hambre: {healthJabali.hunger:F1})");
            return isHungry ? TaskStatus.Success : TaskStatus.Failure;
        }
        return TaskStatus.Failure;
    }
}
