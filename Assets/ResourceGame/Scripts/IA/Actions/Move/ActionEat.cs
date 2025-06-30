using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("IA SC/Node Move")]
public class ActionEat : ActionNodeVehicle
{
    [Header("Configuración Comer")]
    public float eatRadius = 3f;
    
    private Transform targetGrass = null;
    private float eatStartTime = 0f;
    private float maxEatTime = 5f; // Comer máximo 5 segundos
    
    public override void OnStart()
    {
        base.OnStart();
        eatStartTime = Time.time;
        targetGrass = null;
        
        Debug.Log($"🍽️ {gameObject.name}: INICIANDO A COMER");
    }
    
    public override void OnEnd()
    {
        base.OnEnd();
        
        // Detener el comer al finalizar
        if (_AICharacterVehicle.health is HealthCiervo healthCiervo)
        {
            healthCiervo.StopEating();
        }
        else if (_AICharacterVehicle.health is HealthJabali healthJabali)
        {
            healthJabali.StopEating();
        }
        
        Debug.Log($"✅ {gameObject.name}: FINALIZANDO COMER");
    }
    
    public override TaskStatus OnUpdate()
    {
        if (_AICharacterVehicle.health.IsDead)
        {
            return TaskStatus.Failure;
        }

        // Solo herbívoros deberían comer hierba
        if (_AICharacterVehicle.health is HealthLobo)
        {
            Debug.LogWarning($"❌ {gameObject.name}: LOBO NO DEBE COMER HIERBA - Error en Behavior Tree!");
            return TaskStatus.Failure;
        }

        // Verificar si ha pasado demasiado tiempo comiendo
        float elapsedTime = Time.time - eatStartTime;
        if (elapsedTime >= maxEatTime)
        {
            Debug.Log($"⏰ {gameObject.name}: Tiempo de comer agotado - Terminó");
            return TaskStatus.Success;
        }

        // Verificar si ya no tiene hambre Y está completamente saciado
        bool stillHungry = false;
        bool needsMoreFood = false;
        if (_AICharacterVehicle.health is HealthCiervo healthCiervo)
        {
            stillHungry = healthCiervo.IsHungry;
            needsMoreFood = !healthCiervo.IsFull;
            if (stillHungry || needsMoreFood)
            {
                return HandleCiervoEat(healthCiervo);
            }
        }
        else if (_AICharacterVehicle.health is HealthJabali healthJabali)
        {
            stillHungry = healthJabali.IsHungry;
            needsMoreFood = !healthJabali.IsFull;
            if (stillHungry || needsMoreFood)
            {
                return HandleJabaliEat(healthJabali);
            }
        }
        
        if (!stillHungry && !needsMoreFood)
        {
            Debug.Log($"✅ {gameObject.name}: Completamente saciado - Terminó de comer");
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }
    
    private TaskStatus HandleCiervoEat(HealthCiervo healthCiervo)
    {
        var vehicleCiervo = _AICharacterVehicle as AICharacterVehicleCiervo;
        
        // Buscar hierba cercana si no tenemos objetivo
        if (targetGrass == null)
        {
            FindNearestGrass();
        }
        
        if (targetGrass != null)
        {
            float distanceToGrass = Vector3.Distance(transform.position, targetGrass.position);
            
            // Si estamos cerca de la hierba, comer
            if (distanceToGrass <= eatRadius)
            {
                // Detener movimiento mientras come
                if (vehicleCiervo != null && vehicleCiervo.Agent != null && vehicleCiervo.Agent.isOnNavMesh)
                {
                    vehicleCiervo.Agent.isStopped = true;
                    vehicleCiervo.Agent.velocity = Vector3.zero;
                }
                
                healthCiervo.Eat();
                Debug.Log($"🌿 {gameObject.name}: Comiendo hierba... (Hambre: {healthCiervo.hunger:F1}, Saciado: {healthCiervo.IsFull})");
                return TaskStatus.Running;
            }
            else
            {
                // Acercarse a la hierba
                if (vehicleCiervo != null && vehicleCiervo.Agent != null)
                {
                    vehicleCiervo.Agent.SetDestination(targetGrass.position);
                    Debug.Log($"🏃 {gameObject.name}: Acercándose a hierba (Dist: {distanceToGrass:F1}m)");
                }
                return TaskStatus.Running;
            }
        }
        else
        {
            Debug.Log($"❌ {gameObject.name}: No hay hierba disponible para comer");
            return TaskStatus.Failure;
        }
    }
    
    private TaskStatus HandleJabaliEat(HealthJabali healthJabali)
    {
        var vehicleJabali = _AICharacterVehicle as AICharacterVehicleJabali;
        
        // Buscar hierba cercana si no tenemos objetivo
        if (targetGrass == null)
        {
            FindNearestGrass();
        }
        
        if (targetGrass != null)
        {
            // Verificar que la hierba esté dentro del territorio
            float distanceFromTerritory = Vector3.Distance(targetGrass.position, healthJabali.territoryCenter);
            if (distanceFromTerritory > healthJabali.territoryRadius)
            {
                targetGrass = null; // Hierba fuera del territorio
                return TaskStatus.Failure;
            }
            
            float distanceToGrass = Vector3.Distance(transform.position, targetGrass.position);
            
            // Si estamos cerca de la hierba, comer
            if (distanceToGrass <= eatRadius)
            {
                // Detener movimiento mientras come
                if (vehicleJabali != null && vehicleJabali.Agent != null && vehicleJabali.Agent.isOnNavMesh)
                {
                    vehicleJabali.Agent.isStopped = true;
                    vehicleJabali.Agent.velocity = Vector3.zero;
                }
                
                healthJabali.Eat();
                Debug.Log($"🌿 {gameObject.name}: Jabalí comiendo hierba... (Hambre: {healthJabali.hunger:F1}, Saciado: {healthJabali.IsFull})");
                return TaskStatus.Running;
            }
            else
            {
                // Acercarse a la hierba
                if (vehicleJabali != null && vehicleJabali.Agent != null)
                {
                    vehicleJabali.Agent.SetDestination(targetGrass.position);
                    Debug.Log($"🏃 {gameObject.name}: Jabalí acercándose a hierba (Dist: {distanceToGrass:F1}m)");
                }
                return TaskStatus.Running;
            }
        }
        else
        {
            Debug.Log($"❌ {gameObject.name}: No hay hierba disponible en territorio para Jabalí");
            return TaskStatus.Failure;
        }
    }
    
    private void FindNearestGrass()
    {
        // Buscar objetos con tag "Grass" o "Hierba"
        GameObject[] grassObjects = GameObject.FindGameObjectsWithTag("Grass");
        if (grassObjects.Length == 0)
        {
            grassObjects = GameObject.FindGameObjectsWithTag("Hierba");
        }
        
        Transform nearestGrass = null;
        float nearestDistance = eatRadius;
        
        foreach (GameObject grass in grassObjects)
        {
            if (grass != null)
            {
                float distance = Vector3.Distance(transform.position, grass.transform.position);
                
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestGrass = grass.transform;
                }
            }
        }
        
        if (nearestGrass != null)
        {
            targetGrass = nearestGrass;
            Debug.Log($"🎯 {gameObject.name}: Hierba para comer encontrada a {nearestDistance:F1}m");
        }
    }
}
