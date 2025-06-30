using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("IA SC/Node Move")]
public class ActionRunAway : ActionNodeVehicle
{
    private float fleeStartTime = 0f;
    private float fleeDuration = 4f; // Huir por 4 segundos
    
    public override void OnStart()
    {
        base.OnStart();
        fleeStartTime = Time.time;
        
        // Activar estado de huida
        if (_AICharacterVehicle.health is HealthCiervo healthCiervo)
        {
            healthCiervo.StartFleeing();
        }
        
        Debug.Log($"🚨 {gameObject.name}: INICIANDO HUIDA URGENTE!");
    }
    
    public override void OnEnd()
    {
        base.OnEnd();
        
        // Detener estado de huida
        if (_AICharacterVehicle.health is HealthCiervo healthCiervo)
        {
            healthCiervo.StopFleeing();
        }
        
        Debug.Log($"� {gameObject.name}: FINALIZANDO HUIDA");
    }
    
    public override TaskStatus OnUpdate()
    {
        if (_AICharacterVehicle.health.IsDead)
        {
            return TaskStatus.Failure;
        }

        // Verificar si ha pasado suficiente tiempo huyendo
        float elapsedTime = Time.time - fleeStartTime;
        if (elapsedTime >= fleeDuration)
        {
            Debug.Log($"✅ {gameObject.name}: Huida completada - A salvo ({elapsedTime:F1}s)");
            return TaskStatus.Success;
        }
        
        // Verificar si ya no hay enemigo peligroso visible
        if (_AICharacterVehicle._VisionSensor == null || _AICharacterVehicle._VisionSensor.EnemyView == null)
        {
            Debug.Log($"👁️ {gameObject.name}: Enemigo ya no visible - Huida exitosa");
            return TaskStatus.Success;
        }
        
        // Verificar si se alejó lo suficiente del enemigo peligroso
        if (_AICharacterVehicle._VisionSensor.EnemyView != null)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, _AICharacterVehicle._VisionSensor.EnemyView.transform.position);
            if (distanceToEnemy > 20f) // Distancia segura para lobos
            {
                Debug.Log($"🏃‍♂️ {gameObject.name}: Suficientemente alejado - Huida exitosa (Dist: {distanceToEnemy:F1}m)");
                return TaskStatus.Success;
            }
        }

        // Continuar huyendo
        if (Time.time % 1f < 0.02f) // Log cada segundo para reducir spam
        {
            Debug.Log($"🏃 {gameObject.name}: HUYENDO DEL PELIGRO... (Tiempo: {elapsedTime:F1}s)");
        }
        _AICharacterVehicle.MoveToEvadeEnemy();
        return TaskStatus.Running;
    }
}
