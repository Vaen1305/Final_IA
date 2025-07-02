using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("IA SC/Conditional")]
public class ActionIsExhausted : ActionNode
{
    [Header("Configuración")]
    public float exhaustionThreshold = 90f; // Considera exhausto cuando el sueño es >= 90%

    public override TaskStatus OnUpdate()
    {
        if (MyHealth is HealthCiervo healthCiervo)
        {
            bool isExhausted = healthCiervo.sleepiness >= exhaustionThreshold;
            Debug.Log($"😴💤 {gameObject.name}: ¿Exhausto? {isExhausted} (Sueño: {healthCiervo.sleepiness:F1})");
            return isExhausted ? TaskStatus.Success : TaskStatus.Failure;
        }
        if (MyHealth is HealthLobo healthLobo)
        {
            bool isExhausted = healthLobo.sleepiness >= exhaustionThreshold;
            Debug.Log($"😴💤 {gameObject.name}: ¿Exhausto? {isExhausted} (Sueño: {healthLobo.sleepiness:F1})");
            return isExhausted ? TaskStatus.Success : TaskStatus.Failure;
        }
        if (MyHealth is HealthJabali healthJabali)
        {
            bool isExhausted = healthJabali.sleepiness >= exhaustionThreshold;
            Debug.Log($"😴💤 {gameObject.name}: ¿Exhausto? {isExhausted} (Sueño: {healthJabali.sleepiness:F1})");
            return isExhausted ? TaskStatus.Success : TaskStatus.Failure;
        }
        
        return TaskStatus.Failure;
    }
}