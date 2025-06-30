using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("IA SC/ Node View")]
public class ActionViewLobo : ActionView
{
    public override TaskStatus OnUpdate()
    {
        // Debug más detallado
        if (_VisionSensor == null)
        {
            Debug.LogWarning($"⚠️ {gameObject.name}: VisionSensor es null");
            return TaskStatus.Failure;
        }
        
        if (_VisionSensor.EnemyView == null)
        {
            // No hay enemigo detectado
            return TaskStatus.Failure;
        }
        
        // Log detallado de lo que se detecta
        Debug.Log($"🔍 {gameObject.name}: Detectado {_VisionSensor.EnemyView.name} (Tipo: {_VisionSensor.EnemyView.typeAgent})");
        
        if (_VisionSensor.EnemyView.typeAgent == TypeAgent.Lobo)
        {
            float distanceToLobo = Vector3.Distance(transform.position, _VisionSensor.EnemyView.transform.position);
            Debug.Log($"🐺 {gameObject.name}: ¡LOBO DETECTADO! Distancia: {distanceToLobo:F1}m - ACTIVANDO HUIDA URGENTE");
            return TaskStatus.Success;
        }
        else
        {
            Debug.Log($"👁️ {gameObject.name}: No es un lobo - Tipo detectado: {_VisionSensor.EnemyView.typeAgent}");
            return TaskStatus.Failure;
        }
    }
}
