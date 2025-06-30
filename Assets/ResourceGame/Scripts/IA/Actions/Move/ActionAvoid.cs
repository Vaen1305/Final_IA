using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("IA SC/Node Move")]
public class ActionAvoid : ActionNodeVehicle
{
    private float avoidStartTime = 0f;
    private float avoidDuration = 3f; // Evitar por 3 segundos
    private Vector3 initialEnemyPosition;
    
    public override void OnStart()
    {
        base.OnStart();
        avoidStartTime = Time.time;
        
        // Activar estado de huida para ciervo
        if (_AICharacterVehicle.health is HealthCiervo healthCiervo)
        {
            healthCiervo.StartFleeing();
        }
        
        // Guardar posición inicial del enemigo
        if (_AICharacterVehicle._VisionSensor != null && _AICharacterVehicle._VisionSensor.EnemyView != null)
        {
            initialEnemyPosition = _AICharacterVehicle._VisionSensor.EnemyView.transform.position;
        }
        
        Debug.Log($"↩️ {gameObject.name}: INICIANDO EVASIÓN");
    }
    
    public override void OnEnd()
    {
        base.OnEnd();
        
        // Detener estado de huida para ciervo
        if (_AICharacterVehicle.health is HealthCiervo healthCiervo)
        {
            healthCiervo.StopFleeing();
        }
        
        Debug.Log($"� {gameObject.name}: FINALIZANDO EVASIÓN");
    }
    
    public override TaskStatus OnUpdate()
    {
        if (_AICharacterVehicle.health.IsDead)
        {
            return TaskStatus.Failure;
        }
        
        // Verificar si ha pasado suficiente tiempo evitando
        float elapsedTime = Time.time - avoidStartTime;
        if (elapsedTime >= avoidDuration)
        {
            Debug.Log($"✅ {gameObject.name}: Tiempo de evasión completado - Volviendo a comportamiento normal");
            return TaskStatus.Success;
        }
        
        // Verificar si ya no hay enemigo visible
        if (_AICharacterVehicle._VisionSensor == null || _AICharacterVehicle._VisionSensor.EnemyView == null)
        {
            Debug.Log($"👁️ {gameObject.name}: Enemigo ya no visible - Evasión exitosa");
            return TaskStatus.Success;
        }
        
        // Verificar si se alejó lo suficiente del enemigo
        if (_AICharacterVehicle._VisionSensor.EnemyView != null)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, _AICharacterVehicle._VisionSensor.EnemyView.transform.position);
            if (distanceToEnemy > 15f) // Si está lejos del enemigo
            {
                Debug.Log($"🏃‍♂️ {gameObject.name}: Suficientemente alejado del enemigo - Evasión exitosa");
                return TaskStatus.Success;
            }
        }

        // Continuar evitando
        if (Time.time % 1f < 0.02f) // Log cada segundo para reducir spam
        {
            Debug.Log($"↩️ {gameObject.name}: Evitando enemigo... (Tiempo: {elapsedTime:F1}s)");
        }
        _AICharacterVehicle.MoveToEvadeEnemy();
        return TaskStatus.Running;
    }
}
