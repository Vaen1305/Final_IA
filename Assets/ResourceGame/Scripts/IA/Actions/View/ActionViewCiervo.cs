using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("IA SC/ Node View")]
public class ActionViewCiervo : ActionView
{
    [Header("Configuración Detección")]
    public float detectionRange = 15f;
    
    public override TaskStatus OnUpdate()
    {
        // Verificar si es un Lobo y tiene hambre
        if (_AICharacterVehicle.health is HealthLobo healthLobo)
        {
            if (!healthLobo.IsHungry)
            {
                Debug.Log($"🐺 {gameObject.name}: No tiene hambre - No cazará ciervos");
                return TaskStatus.Failure;
            }
        }

        // Usar VisionSensor si está disponible
        if (_VisionSensor != null && _VisionSensor.EnemyView != null)
        {
            if (_VisionSensor.EnemyView.typeAgent == TypeAgent.Ciervo)
            {
                // Verificar que el ciervo esté vivo
                var healthCiervo = _VisionSensor.EnemyView.GetComponent<HealthCiervo>();
                if (healthCiervo != null && healthCiervo.IsDead)
                {
                    Debug.Log($"💀 {gameObject.name}: Ciervo detectado está muerto - No cazable");
                    return TaskStatus.Failure;
                }
                
                float distance = Vector3.Distance(transform.position, _VisionSensor.EnemyView.transform.position);
                Debug.Log($"🎯 {gameObject.name}: ¡CIERVO VIVO DETECTADO! Distancia: {distance:F1}m");
                return TaskStatus.Success;
            }
        }
        
        // Busqueda alternativa por tag si VisionSensor no detecta
        bool ciervoFound = CheckForNearbyDeer();
        if (ciervoFound)
        {
            Debug.Log($"🎯 {gameObject.name}: ¡CIERVO DETECTADO POR BÚSQUEDA ALTERNATIVA!");
            return TaskStatus.Success;
        }
        
        return TaskStatus.Failure;
    }
    
    private bool CheckForNearbyDeer()
    {
        // Buscar ciervos vivos
        GameObject[] ciervos = GameObject.FindGameObjectsWithTag("Ciervo");
        
        foreach (GameObject ciervo in ciervos)
        {
            if (ciervo != null)
            {
                // Verificar que el ciervo esté vivo
                var healthCiervo = ciervo.GetComponent<HealthCiervo>();
                if (healthCiervo != null && healthCiervo.IsDead)
                {
                    continue; // Ciervo muerto, no cazarlo
                }
                
                float distance = Vector3.Distance(transform.position, ciervo.transform.position);
                if (distance <= detectionRange)
                {
                    Debug.Log($"🦌 {gameObject.name}: Ciervo {ciervo.name} detectado a {distance:F1}m");
                    return true;
                }
            }
        }
        
        return false;
    }
}
