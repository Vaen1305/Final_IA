using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("IA SC/Node Conditional")]
public class ActionCiervoNotViewPredator : ActionNode
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
            // Si no hay sensor de visión, asumimos que no ve predadores
            return TaskStatus.Success;
        }
        
        // Para el ciervo, verificar si NO hay lobos en vista
        bool hasWolfInView = false;
        
        if (_VisionSensor.EnemyView != null)
        {
            // Verificar si el enemigo detectado es un lobo
            var healthLobo = _VisionSensor.EnemyView.GetComponent<HealthLobo>();
            if (healthLobo != null)
            {
                hasWolfInView = true;
            }
        }
        
        if (!hasWolfInView)
        {
            // No hay lobos en vista - el ciervo está seguro
            return TaskStatus.Success;
        }
        else
        {
            // Hay un lobo en vista - el ciervo debe huir
            return TaskStatus.Failure;
        }
    }
}
