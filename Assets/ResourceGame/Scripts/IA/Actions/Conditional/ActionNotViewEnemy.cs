using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("IA SC/Node Conditional")]
public class ActionNotViewEnemy : ActionNode
{
    public override TaskStatus OnUpdate()
    {
        // Verificar que el objeto siga vivo
        if (MyHealth != null && MyHealth.IsDead)
        {
            return TaskStatus.Failure;
        }
        
        if (_VisionSensor == null)
        {
            // Si no hay sensor de visión, asumimos que no ve enemigos
            return TaskStatus.Success;
        }
        
        // Verificar si NO hay enemigos en vista
        bool hasEnemyInView = (_VisionSensor.EnemyView != null);
        
        if (!hasEnemyInView)
        {
            // No hay enemigos en vista - éxito para la condición "Not View Enemy"
            return TaskStatus.Success;
        }
        else
        {
            // Hay enemigos en vista - falla la condición "Not View Enemy"
            return TaskStatus.Failure;
        }
    }
}
