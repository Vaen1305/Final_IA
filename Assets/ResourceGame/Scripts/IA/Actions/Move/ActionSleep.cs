using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("IA SC/Node Move")]
public class ActionSleep : ActionNodeVehicle
{
    [Header("Configuración Dormir")]
    public float sleepDuration = 5f; // Dormir por 5 segundos
    
    private float sleepStartTime = 0f;
    
    public override void OnStart()
    {
        base.OnStart();
        sleepStartTime = Time.time;
        
        if (_AICharacterVehicle.health is HealthCiervo healthCiervo)
        {
            healthCiervo.IsSleeping = true;
            Debug.Log($"😴 {gameObject.name}: Ciervo empezó a dormir");
        }
        else if (_AICharacterVehicle.health is HealthLobo healthLobo)
        {
            healthLobo.IsSleeping = true;
            Debug.Log($"😴 {gameObject.name}: Lobo empezó a dormir");
        }
        else if (_AICharacterVehicle.health is HealthJabali healthJabali)
        {
            healthJabali.IsSleeping = true;
            Debug.Log($"😴 {gameObject.name}: Jabalí empezó a dormir");
        }
    }
    
    public override void OnEnd()
    {
        if (_AICharacterVehicle.health is HealthCiervo healthCiervo)
        {
            healthCiervo.IsSleeping = false;
        }
        else if (_AICharacterVehicle.health is HealthLobo healthLobo)
        {
            healthLobo.IsSleeping = false;
        }
        else if (_AICharacterVehicle.health is HealthJabali healthJabali)
        {
            healthJabali.IsSleeping = false;
        }
    }
    
    public override TaskStatus OnUpdate()
    {
        if (_AICharacterVehicle.health.IsDead)
        {
            return TaskStatus.Failure;
        }
        
        float elapsedTime = Time.time - sleepStartTime;
        
        // Verificar si ha dormido lo suficiente
        if (elapsedTime >= sleepDuration)
        {
            Debug.Log($"😴✅ {gameObject.name}: Terminó de dormir");
            return TaskStatus.Success;
        }
        
        return TaskStatus.Running;
    }
}