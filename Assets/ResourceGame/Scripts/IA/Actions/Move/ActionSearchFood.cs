using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("IA SC/Node Move")]
public class ActionSearchFood : ActionNodeVehicle
{
    [Header("Configuración Búsqueda")]
    public float searchRadius = 15f;
    public LayerMask grassLayerMask = 1; // Layer de la hierba
    
    private Transform targetGrass = null;
    private float searchStartTime = 0f;
    private float maxSearchTime = 10f; // Buscar máximo 10 segundos
    
    public override void OnStart()
    {
        base.OnStart();
        searchStartTime = Time.time;
        targetGrass = null;
        
        // Debug específico para el ciervo
        if (_AICharacterVehicle.health is HealthCiervo healthCiervo)
        {
            Debug.Log($"🔍 {gameObject.name}: CIERVO INICIANDO BÚSQUEDA - Hambre: {healthCiervo.hunger:F1}, Hambriento: {healthCiervo.IsHungry}");
        }
        else
        {
            Debug.Log($"🔍 {gameObject.name}: INICIANDO BÚSQUEDA DE COMIDA");
        }
    }
    
    public override void OnEnd()
    {
        base.OnEnd();
        Debug.Log($"🛑 {gameObject.name}: FINALIZANDO BÚSQUEDA DE COMIDA");
    }
    
    public override TaskStatus OnUpdate()
    {
        if (_AICharacterVehicle.health.IsDead)
        {
            return TaskStatus.Failure;
        }

        // Solo herbívoros deberían buscar hierba
        if (_AICharacterVehicle.health is HealthLobo)
        {
            Debug.LogWarning($"❌ {gameObject.name}: LOBO NO DEBE BUSCAR HIERBA - Error en Behavior Tree!");
            return TaskStatus.Failure;
        }

        // Verificar si ha pasado demasiado tiempo buscando
        float elapsedTime = Time.time - searchStartTime;
        if (elapsedTime >= maxSearchTime)
        {
            Debug.Log($"⏰ {gameObject.name}: Tiempo de búsqueda agotado - No encontró comida");
            return TaskStatus.Failure;
        }

        // Verificar si ya no tiene hambre
        bool stillHungry = false;
        if (_AICharacterVehicle.health is HealthCiervo healthCiervo)
        {
            stillHungry = healthCiervo.IsHungry;
            Debug.Log($"🦌 {gameObject.name}: CIERVO estado - Hambre: {healthCiervo.hunger:F1}, Hambriento: {stillHungry}, Saciado: {healthCiervo.IsFull}");
        }
        else if (_AICharacterVehicle.health is HealthJabali healthJabali)
        {
            stillHungry = healthJabali.IsHungry;
            Debug.Log($"🐗 {gameObject.name}: JABALÍ estado - Hambre: {healthJabali.hunger:F1}, Hambriento: {stillHungry}");
        }
        
        if (!stillHungry)
        {
            Debug.Log($"✅ {gameObject.name}: Ya no tiene hambre - Búsqueda completada");
            return TaskStatus.Success;
        }

        // Buscar hierba si no tenemos objetivo
        if (targetGrass == null)
        {
            FindNearestGrass();
        }

        // Si encontramos hierba, ir hacia ella
        if (targetGrass != null)
        {
            float distanceToGrass = Vector3.Distance(transform.position, targetGrass.position);
            
            // Si estamos cerca de la hierba, cambiar a comer
            if (distanceToGrass <= 2f)
            {
                Debug.Log($"🌿 {gameObject.name}: Llegó a la hierba - Cambiando a comer");
                return TaskStatus.Success;
            }
            
            // Moverse hacia la hierba
            if (_AICharacterVehicle.Agent != null)
            {
                _AICharacterVehicle.Agent.SetDestination(targetGrass.position);
                
                if (Time.time % 2f < 0.02f) // Log cada 2 segundos
                {
                    Debug.Log($"🏃 {gameObject.name}: Yendo hacia hierba (Dist: {distanceToGrass:F1}m)");
                }
            }
            
            return TaskStatus.Running;
        }
        
        // No hay hierba disponible, continuar patrullando
        Debug.Log($"❌ {gameObject.name}: No se encontró hierba - Patrullando...");
        _AICharacterVehicle.Wander();
        return TaskStatus.Running;
    }
    
    private void FindNearestGrass()
    {
        // Buscar objetos con tag "Grass" o "Hierba"
        GameObject[] grassObjects = GameObject.FindGameObjectsWithTag("Grass");
        if (grassObjects.Length == 0)
        {
            grassObjects = GameObject.FindGameObjectsWithTag("Hierba");
        }
        
        // Debug: Reportar cuántos objetos de hierba hay
        Debug.Log($"🌿 {gameObject.name}: Encontrados {grassObjects.Length} objetos de hierba en la escena");
        
        if (grassObjects.Length == 0)
        {
            Debug.LogError($"❌ {gameObject.name}: NO HAY OBJETOS CON TAG 'Grass' O 'Hierba' EN LA ESCENA!");
            return;
        }
        
        Transform nearestGrass = null;
        float nearestDistance = searchRadius;
        
        foreach (GameObject grass in grassObjects)
        {
            if (grass != null)
            {
                float distance = Vector3.Distance(transform.position, grass.transform.position);
                
                // Para Jabalí, verificar que esté dentro del territorio
                if (_AICharacterVehicle.health is HealthJabali healthJabali)
                {
                    float distanceFromTerritory = Vector3.Distance(grass.transform.position, healthJabali.territoryCenter);
                    if (distanceFromTerritory > healthJabali.territoryRadius)
                    {
                        Debug.Log($"🚫 {gameObject.name}: Hierba fuera del territorio (Dist: {distanceFromTerritory:F1}m)");
                        continue; // Hierba fuera del territorio
                    }
                }
                
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestGrass = grass.transform;
                }
                
                Debug.Log($"🌿 {gameObject.name}: Hierba candidata '{grass.name}' a {distance:F1}m");
            }
        }
        
        if (nearestGrass != null)
        {
            targetGrass = nearestGrass;
            Debug.Log($"🎯 {gameObject.name}: Hierba seleccionada '{nearestGrass.name}' a {nearestDistance:F1}m");
        }
        else
        {
            Debug.LogWarning($"❌ {gameObject.name}: No se encontró hierba accesible en radio de {searchRadius}m");
        }
    }
}
