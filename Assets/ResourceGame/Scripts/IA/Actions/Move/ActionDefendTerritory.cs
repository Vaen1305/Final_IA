using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("IA SC/Node Move")]
public class ActionDefendTerritory : ActionNodeVehicle
{
    [Header("Configuración Defensa")]
    public float attackRange = 3f;
    public float chaseRange = 10f;
    
    private Transform targetIntruder = null;
    private float defenseStartTime = 0f;
    private float maxDefenseTime = 8f; // Defender máximo 8 segundos
    
    public override void OnStart()
    {
        base.OnStart();
        defenseStartTime = Time.time;
        targetIntruder = null;
        
        // Activar estado de defensa
        if (_AICharacterVehicle.health is HealthJabali healthJabali)
        {
            healthJabali.StartDefending();
        }
        
        Debug.Log($"⚔️ {gameObject.name}: INICIANDO DEFENSA DEL TERRITORIO");
    }
    
    public override void OnEnd()
    {
        base.OnEnd();
        
        // Detener estado de defensa
        if (_AICharacterVehicle.health is HealthJabali healthJabali)
        {
            healthJabali.StopDefending();
        }
        
        Debug.Log($"🛡️ {gameObject.name}: FINALIZANDO DEFENSA");
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

        // Verificar si ha pasado demasiado tiempo defendiendo
        float elapsedTime = Time.time - defenseStartTime;
        if (elapsedTime >= maxDefenseTime)
        {
            Debug.Log($"⏰ {gameObject.name}: Tiempo de defensa agotado - Volviendo a patrulla");
            return TaskStatus.Success;
        }

        // Buscar intruso si no tenemos objetivo
        if (targetIntruder == null)
        {
            FindIntruderInTerritory(healthJabali);
        }

        // Si hay intruso, atacarlo
        if (targetIntruder != null)
        {
            float distanceToIntruder = Vector3.Distance(transform.position, targetIntruder.position);
            
            // Verificar si el intruso salió del territorio
            float intruderDistanceFromCenter = Vector3.Distance(targetIntruder.position, healthJabali.territoryCenter);
            if (intruderDistanceFromCenter > healthJabali.territoryRadius)
            {
                Debug.Log($"✅ {gameObject.name}: Intruso expulsado del territorio - Defensa exitosa");
                return TaskStatus.Success;
            }
            
            // Si estamos cerca del intruso, atacar
            if (distanceToIntruder <= attackRange)
            {
                AttackIntruder();
                return TaskStatus.Running;
            }
            // Si está en rango de persecución, perseguir
            else if (distanceToIntruder <= chaseRange)
            {
                ChaseIntruder();
                return TaskStatus.Running;
            }
            else
            {
                // Intruso muy lejos, buscar otro
                targetIntruder = null;
            }
        }
        
        // No hay intruso visible, patrullar agresivamente
        Debug.Log($"👁️ {gameObject.name}: No hay intruso visible - Patrullando agresivamente");
        _AICharacterVehicle.Wander();
        return TaskStatus.Running;
    }
    
    private void FindIntruderInTerritory(HealthJabali healthJabali)
    {
        // Buscar Lobos y Ciervos en el territorio
        GameObject[] potentialIntruders = GameObject.FindGameObjectsWithTag("Lobo");
        GameObject[] ciervos = GameObject.FindGameObjectsWithTag("Ciervo");
        
        Transform nearestIntruder = null;
        float nearestDistance = float.MaxValue;
        
        // Verificar lobos
        foreach (GameObject lobo in potentialIntruders)
        {
            if (lobo != null && lobo != gameObject)
            {
                float distanceFromCenter = Vector3.Distance(lobo.transform.position, healthJabali.territoryCenter);
                if (distanceFromCenter <= healthJabali.territoryRadius)
                {
                    float distance = Vector3.Distance(transform.position, lobo.transform.position);
                    if (distance < nearestDistance)
                    {
                        nearestDistance = distance;
                        nearestIntruder = lobo.transform;
                    }
                }
            }
        }
        
        // Verificar ciervos
        foreach (GameObject ciervo in ciervos)
        {
            if (ciervo != null)
            {
                float distanceFromCenter = Vector3.Distance(ciervo.transform.position, healthJabali.territoryCenter);
                if (distanceFromCenter <= healthJabali.territoryRadius)
                {
                    float distance = Vector3.Distance(transform.position, ciervo.transform.position);
                    if (distance < nearestDistance)
                    {
                        nearestDistance = distance;
                        nearestIntruder = ciervo.transform;
                    }
                }
            }
        }
        
        if (nearestIntruder != null)
        {
            targetIntruder = nearestIntruder;
            Debug.Log($"🎯 {gameObject.name}: Intruso detectado: {targetIntruder.name} a {nearestDistance:F1}m");
        }
    }
    
    private void AttackIntruder()
    {
        if (targetIntruder != null)
        {
            // Detener movimiento para atacar
            if (_AICharacterVehicle.Agent != null)
            {
                _AICharacterVehicle.Agent.isStopped = true;
                _AICharacterVehicle.Agent.velocity = Vector3.zero;
            }
            
            // Rotar hacia el intruso
            Vector3 direction = (targetIntruder.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(direction);
            
            Debug.Log($"⚔️ {gameObject.name}: ATACANDO a {targetIntruder.name}!");
            
            // Aquí puedes añadir lógica de daño si es necesario
            // Por ejemplo: targetIntruder.GetComponent<Health>().TakeDamage(damage);
        }
    }
    
    private void ChaseIntruder()
    {
        if (targetIntruder != null && _AICharacterVehicle.Agent != null)
        {
            _AICharacterVehicle.Agent.isStopped = false;
            _AICharacterVehicle.Agent.SetDestination(targetIntruder.position);
            
            float distance = Vector3.Distance(transform.position, targetIntruder.position);
            Debug.Log($"🏃 {gameObject.name}: Persiguiendo intruso {targetIntruder.name} (Dist: {distance:F1}m)");
        }
    }
}
