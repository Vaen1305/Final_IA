using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("IA SC/Node Move")]
public class ActionPatrolTerritory : ActionNodeVehicle
{
    [Header("Configuración Patrulla")]
    public float patrolSpeed = 3f;
    
    private Vector3 currentTarget;
    private float targetChangeInterval = 5f; // Cambiar objetivo cada 5 segundos
    private float lastTargetChangeTime = 0f;
    
    public override void OnStart()
    {
        base.OnStart();
        GenerateNewPatrolTarget();
        
        Debug.Log($"🚶 {gameObject.name}: INICIANDO PATRULLA DEL TERRITORIO");
    }
    
    public override void OnEnd()
    {
        base.OnEnd();
        Debug.Log($"🛑 {gameObject.name}: FINALIZANDO PATRULLA");
    }
    
    public override TaskStatus OnUpdate()
    {
        if (_AICharacterVehicle.health.IsDead)
        {
            return TaskStatus.Failure;
        }

        // Solo funciona para Jabalí
        if (!(_AICharacterVehicle.health is HealthJabali healthJabali))
        {
            return TaskStatus.Failure;
        }

        // Verificar si necesita cambiar objetivo
        if (Time.time - lastTargetChangeTime > targetChangeInterval)
        {
            GenerateNewPatrolTarget();
        }

        // Verificar si llegó al objetivo actual
        float distanceToTarget = Vector3.Distance(transform.position, currentTarget);
        if (distanceToTarget <= 2f)
        {
            GenerateNewPatrolTarget();
        }

        // Verificar si está fuera del territorio y necesita regresar
        if (!healthJabali.IsInTerritory)
        {
            ReturnToTerritory(healthJabali);
        }
        else
        {
            // Moverse hacia el objetivo de patrulla
            if (_AICharacterVehicle.Agent != null)
            {
                _AICharacterVehicle.Agent.SetDestination(currentTarget);
                _AICharacterVehicle.Agent.speed = patrolSpeed;
                
                if (Time.time % 3f < 0.02f) // Log cada 3 segundos
                {
                    Debug.Log($"🚶 {gameObject.name}: Patrullando territorio (Dist: {distanceToTarget:F1}m)");
                }
            }
        }

        return TaskStatus.Running;
    }
    
    private void GenerateNewPatrolTarget()
    {
        if (_AICharacterVehicle.health is HealthJabali healthJabali)
        {
            // Generar un punto aleatorio dentro del territorio
            Vector2 randomCircle = Random.insideUnitCircle * healthJabali.territoryRadius * 0.8f; // 80% del radio
            currentTarget = healthJabali.territoryCenter + new Vector3(randomCircle.x, 0, randomCircle.y);
            
            lastTargetChangeTime = Time.time;
            
            Debug.Log($"🎯 {gameObject.name}: Nuevo objetivo de patrulla generado");
        }
    }
    
    private void ReturnToTerritory(HealthJabali healthJabali)
    {
        // Dirigirse al centro del territorio
        Vector3 directionToCenter = (healthJabali.territoryCenter - transform.position).normalized;
        currentTarget = healthJabali.territoryCenter + directionToCenter * (healthJabali.territoryRadius * 0.5f);
        
        if (_AICharacterVehicle.Agent != null)
        {
            _AICharacterVehicle.Agent.SetDestination(currentTarget);
            
            float distanceFromCenter = Vector3.Distance(transform.position, healthJabali.territoryCenter);
            Debug.Log($"🏠 {gameObject.name}: Regresando al territorio (Dist del centro: {distanceFromCenter:F1}m)");
        }
    }
}
