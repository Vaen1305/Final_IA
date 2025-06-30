using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("IA SC/Node Move")]
public class ActionHuntCiervo : ActionNodeVehicle
{
    [Header("Configuración Caza")]
    public float huntRadius = 20f;
    public float catchRadius = 2f;
    
    private Transform targetCiervo = null;
    private float huntStartTime = 0f;
    private float maxHuntTime = 15f; // Cazar máximo 15 segundos
    
    public override void OnStart()
    {
        base.OnStart();
        huntStartTime = Time.time;
        targetCiervo = null;
        
        // Activar estado de caza
        if (_AICharacterVehicle.health is HealthLobo healthLobo)
        {
            healthLobo.StartHunting();
        }
        
        Debug.Log($"🎯 {gameObject.name}: INICIANDO CAZA DE CIERVO");
    }
    
    public override void OnEnd()
    {
        base.OnEnd();
        
        // Detener estado de caza
        if (_AICharacterVehicle.health is HealthLobo healthLobo)
        {
            healthLobo.StopHunting();
        }
        
        Debug.Log($"🛑 {gameObject.name}: FINALIZANDO CAZA");
    }
    
    public override TaskStatus OnUpdate()
    {
        if (_AICharacterVehicle.health.IsDead)
        {
            return TaskStatus.Failure;
        }

        // Verificar si ha pasado demasiado tiempo cazando
        float elapsedTime = Time.time - huntStartTime;
        if (elapsedTime >= maxHuntTime)
        {
            Debug.Log($"⏰ {gameObject.name}: Tiempo de caza agotado - No atrapó ciervo");
            return TaskStatus.Failure;
        }

        // Verificar si ya no tiene hambre
        if (_AICharacterVehicle.health is HealthLobo healthLobo)
        {
            if (!healthLobo.IsHungry)
            {
                Debug.Log($"✅ {gameObject.name}: Ya no tiene hambre - Caza completada");
                return TaskStatus.Success;
            }
        }

        // Buscar ciervo si no tenemos objetivo
        if (targetCiervo == null)
        {
            FindNearestCiervo();
        }

        // Si encontramos ciervo, cazarlo
        if (targetCiervo != null)
        {
            float distanceToCiervo = Vector3.Distance(transform.position, targetCiervo.position);
            
            // Si estamos cerca del ciervo, atraparlo
            if (distanceToCiervo <= catchRadius)
            {
                CatchCiervo();
                return TaskStatus.Success;
            }
            
            // Perseguir al ciervo
            if (_AICharacterVehicle.Agent != null)
            {
                _AICharacterVehicle.Agent.SetDestination(targetCiervo.position);
                
                if (Time.time % 1f < 0.02f) // Log cada segundo
                {
                    Debug.Log($"🏃 {gameObject.name}: Persiguiendo ciervo (Dist: {distanceToCiervo:F1}m)");
                }
            }
            
            return TaskStatus.Running;
        }
        
        // No hay ciervo disponible, patrullar
        Debug.Log($"❌ {gameObject.name}: No se encontró ciervo - Patrullando...");
        _AICharacterVehicle.Wander();
        return TaskStatus.Running;
    }
    
    private void FindNearestCiervo()
    {
        // Buscar objetos con tag "Ciervo"
        GameObject[] ciervoObjects = GameObject.FindGameObjectsWithTag("Ciervo");
        
        Transform nearestCiervo = null;
        float nearestDistance = huntRadius;
        
        foreach (GameObject ciervo in ciervoObjects)
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
                
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestCiervo = ciervo.transform;
                }
            }
        }
        
        if (nearestCiervo != null)
        {
            targetCiervo = nearestCiervo;
            Debug.Log($"🎯 {gameObject.name}: Ciervo encontrado a {nearestDistance:F1}m");
        }
        else
        {
            Debug.Log($"❌ {gameObject.name}: No se encontró ciervo en radio de {huntRadius}m");
        }
    }
    
    private void CatchCiervo()
    {
        if (targetCiervo != null && _AICharacterVehicle.health is HealthLobo healthLobo)
        {
            Debug.Log($"🍖 {gameObject.name}: ¡ATRAPÓ CIERVO! Comiendo...");
            
            // El lobo come (reduce hambre)
            healthLobo.EatPrey();
            
            // Opcional: "matar" o desactivar el ciervo
            var healthCiervo = targetCiervo.GetComponent<HealthCiervo>();
            if (healthCiervo != null)
            {
                // Puedes implementar lógica para "matar" el ciervo aquí
                Debug.Log($"💀 Ciervo {targetCiervo.name} fue cazado por {gameObject.name}");
            }
            
            targetCiervo = null;
        }
    }
}
