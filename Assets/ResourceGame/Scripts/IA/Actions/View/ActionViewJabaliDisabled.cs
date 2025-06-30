using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("IA SC/ Node View")]
public class ActionViewJabaliDisabled : ActionView
{
    public override TaskStatus OnUpdate()
    {
        // Temporalmente deshabilitado para debug
        Debug.Log($"🚫 {gameObject.name}: Detección de jabalí DESHABILITADA temporalmente");
        return TaskStatus.Failure;
    }
}
