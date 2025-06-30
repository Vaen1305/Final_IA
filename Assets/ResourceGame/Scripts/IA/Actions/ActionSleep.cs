using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("IA SC/Node Actions")]
public class ActionSleep : ActionNodeVehicle
{
    public override void OnStart()
    {
        base.OnStart();
        Debug.Log($"😴 {gameObject.name}: INICIANDO SUEÑO");
    }
    
    public override void OnEnd()
    {
        base.OnEnd();
        
        // Detener el sueño al finalizar
        if (_AICharacterVehicle.health is HealthCiervo healthCiervo)
        {
            healthCiervo.StopSleeping();
        }
        else if (_AICharacterVehicle.health is HealthLobo healthLobo)
        {
            healthLobo.StopSleeping();
        }
        else if (_AICharacterVehicle.health is HealthJabali healthJabali)
        {
            healthJabali.StopSleeping();
        }
        
        Debug.Log($"⏰ {gameObject.name}: FINALIZANDO SUEÑO");
    }
    
    public override TaskStatus OnUpdate()
    {
        if (_AICharacterVehicle.health.IsDead)
        {
            return TaskStatus.Failure;
        }

        // Verificar tipo de animal y dormir
        if (_AICharacterVehicle.health is HealthCiervo healthCiervo)
        {
            return HandleCiervoSleep(healthCiervo);
        }
        else if (_AICharacterVehicle.health is HealthLobo healthLobo)
        {
            return HandleLoboSleep(healthLobo);
        }
        else if (_AICharacterVehicle.health is HealthJabali healthJabali)
        {
            return HandleJabaliSleep(healthJabali);
        }
        
        return TaskStatus.Failure;
    }
    
    private TaskStatus HandleCiervoSleep(HealthCiervo healthCiervo)
    {
        var vehicleCiervo = _AICharacterVehicle as AICharacterVehicleCiervo;
        
        // Solo dormir si realmente tiene sueño Y no está completamente descansado
        if (healthCiervo.IsSleepy || !healthCiervo.IsRested)
        {
            // Detener movimiento mientras duerme
            if (vehicleCiervo != null && vehicleCiervo.Agent != null && vehicleCiervo.Agent.isOnNavMesh)
            {
                vehicleCiervo.Agent.isStopped = true;
                vehicleCiervo.Agent.velocity = Vector3.zero;
            }
            
            healthCiervo.Sleep();
            Debug.Log($"😴 {gameObject.name}: Durmiendo... (Sueño: {healthCiervo.sleepiness:F1}, Descansado: {healthCiervo.IsRested})");
            return TaskStatus.Running;
        }
        else
        {
            // Ya está completamente descansado, permitir movimiento
            if (vehicleCiervo != null && vehicleCiervo.Agent != null && vehicleCiervo.Agent.isOnNavMesh)
            {
                vehicleCiervo.Agent.isStopped = false;
            }
            
            Debug.Log($"⏰ {gameObject.name}: Completamente descansado - Puede continuar (Sueño: {healthCiervo.sleepiness:F1})");
            return TaskStatus.Success;
        }
    }
    
    private TaskStatus HandleLoboSleep(HealthLobo healthLobo)
    {
        var vehicleLobo = _AICharacterVehicle as AICharacterVehicleLobo;
        
        if (healthLobo.IsSleepy || !healthLobo.IsRested)
        {
            if (vehicleLobo != null && vehicleLobo.Agent != null && vehicleLobo.Agent.isOnNavMesh)
            {
                vehicleLobo.Agent.isStopped = true;
                vehicleLobo.Agent.velocity = Vector3.zero;
            }
            
            healthLobo.Sleep();
            Debug.Log($"😴 {gameObject.name}: Lobo durmiendo... (Sueño: {healthLobo.sleepiness:F1}, Descansado: {healthLobo.IsRested})");
            return TaskStatus.Running;
        }
        else
        {
            if (vehicleLobo != null && vehicleLobo.Agent != null && vehicleLobo.Agent.isOnNavMesh)
            {
                vehicleLobo.Agent.isStopped = false;
            }
            
            Debug.Log($"⏰ {gameObject.name}: Lobo completamente descansado - Puede continuar (Sueño: {healthLobo.sleepiness:F1})");
            return TaskStatus.Success;
        }
    }
    
    private TaskStatus HandleJabaliSleep(HealthJabali healthJabali)
    {
        var vehicleJabali = _AICharacterVehicle as AICharacterVehicleJabali;
        
        if (healthJabali.IsSleepy || !healthJabali.IsRested)
        {
            if (vehicleJabali != null && vehicleJabali.Agent != null && vehicleJabali.Agent.isOnNavMesh)
            {
                vehicleJabali.Agent.isStopped = true;
                vehicleJabali.Agent.velocity = Vector3.zero;
            }
            
            healthJabali.Sleep();
            Debug.Log($"😴 {gameObject.name}: Jabalí durmiendo... (Sueño: {healthJabali.sleepiness:F1}, Descansado: {healthJabali.IsRested})");
            return TaskStatus.Running;
        }
        else
        {
            if (vehicleJabali != null && vehicleJabali.Agent != null && vehicleJabali.Agent.isOnNavMesh)
            {
                vehicleJabali.Agent.isStopped = false;
            }
            
            Debug.Log($"⏰ {gameObject.name}: Jabalí completamente descansado - Puede continuar (Sueño: {healthJabali.sleepiness:F1})");
            return TaskStatus.Success;
        }
    }
}

