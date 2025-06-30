using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("IA SC/ Node View")]
public class ActionViewJabali : ActionView
{
    private float lastDetectionTime = 0f;
    private float detectionCooldown = 1f; // Cooldown reducido a 1 segundo
    
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
        
        // Verificar tipo de enemigo
        if (_VisionSensor.EnemyView.typeAgent == TypeAgent.Jabali)
        {
            float distanceToJabali = Vector3.Distance(transform.position, _VisionSensor.EnemyView.transform.position);
            
            // Verificar cooldown para evitar detección constante
            if (Time.time - lastDetectionTime > detectionCooldown)
            {
                lastDetectionTime = Time.time;
                Debug.Log($"🐗 {gameObject.name}: ¡JABALÍ DETECTADO! Distancia: {distanceToJabali:F1}m - Activando evasión...");
                return TaskStatus.Success;
            }
            else
            {
                // Aún en cooldown, no activar evasión
                Debug.Log($"⏳ {gameObject.name}: Jabalí detectado pero en cooldown... (Dist: {distanceToJabali:F1}m)");
                return TaskStatus.Failure;
            }
        }
        else
        {
            // Detectado otro tipo de enemigo
            Debug.Log($"👁️ {gameObject.name}: Detectado {_VisionSensor.EnemyView.name} (Tipo: {_VisionSensor.EnemyView.typeAgent}) - No es un jabalí");
            return TaskStatus.Failure;
        }
    }
}
