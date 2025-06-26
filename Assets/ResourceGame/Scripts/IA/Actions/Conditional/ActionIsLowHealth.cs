using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("IA SC/Conditional")]
public class ActionIsLowHealth : ActionNode
{
    [UnityEngine.Tooltip("El porcentaje de vida para considerarse bajo")]
    public float lowHealthThreshold = 0.3f; // 30%

    public override TaskStatus OnUpdate()
    {
        if (MyHealth != null)
        {
            float healthPercent = (float)MyHealth.health / MyHealth.healthMax;
            return healthPercent <= lowHealthThreshold
                ? TaskStatus.Success
                : TaskStatus.Failure;
        }
        return TaskStatus.Failure;
    }
}
