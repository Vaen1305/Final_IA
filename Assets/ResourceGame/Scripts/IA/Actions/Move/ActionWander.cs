using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("IA SC/Node Move")]
public class ActionWander : ActionNodeVehicle
{
    public override void OnStart()
    {
        base.OnStart();
        Debug.Log($"🚶 {gameObject.name}: INICIANDO WANDER");
    }

    public override TaskStatus OnUpdate()
    {
        if (_AICharacterVehicle.health.IsDead)
        {
            return TaskStatus.Failure;
        }
        
        // Verificar estado según el tipo de animal
        if (_AICharacterVehicle.health is HealthCiervo healthCiervo)
        {
            // El ciervo solo deambula cuando no está huyendo, no tiene hambre ni sueño
            if (healthCiervo.IsFleeing)
            {
                Debug.Log($"⚠️ {gameObject.name}: No puede deambular - Está huyendo");
                return TaskStatus.Failure;
            }
            
            if (healthCiervo.IsHungry)
            {
                Debug.Log($"🍽️ {gameObject.name}: No puede deambular - Tiene hambre ({healthCiervo.hunger:F1})");
                return TaskStatus.Failure;
            }
            
            if (healthCiervo.IsSleepy)
            {
                Debug.Log($"😴 {gameObject.name}: No puede deambular - Tiene sueño ({healthCiervo.sleepiness:F1})");
                return TaskStatus.Failure;
            }
            
            Debug.Log($"🚶 {gameObject.name}: Deambulando tranquilamente - H:{healthCiervo.hunger:F1} S:{healthCiervo.sleepiness:F1}");
        }
        else if (_AICharacterVehicle.health is HealthLobo healthLobo)
        {
            // El lobo deambula cuando no tiene hambre ni sueño
            if (healthLobo.IsHungry)
            {
                Debug.Log($"🍽️ {gameObject.name}: No puede deambular - Debe cazar (hambre: {healthLobo.hunger:F1})");
                return TaskStatus.Failure;
            }
            
            if (healthLobo.IsSleepy)
            {
                Debug.Log($"😴 {gameObject.name}: No puede deambular - Tiene sueño ({healthLobo.sleepiness:F1})");
                return TaskStatus.Failure;
            }
            
            Debug.Log($"🚶 {gameObject.name}: Deambulando - H:{healthLobo.hunger:F1} S:{healthLobo.sleepiness:F1}");
        }
        else if (_AICharacterVehicle.health is HealthJabali healthJabali)
        {
            // El jabalí deambula solo si no tiene hambre ni sueño
            if (healthJabali.IsHungry)
            {
                Debug.Log($"🍽️ {gameObject.name}: No puede deambular - Debe buscar comida (hambre: {healthJabali.hunger:F1})");
                return TaskStatus.Failure;
            }
            
            if (healthJabali.IsSleepy)
            {
                Debug.Log($"😴 {gameObject.name}: No puede deambular - Tiene sueño ({healthJabali.sleepiness:F1})");
                return TaskStatus.Failure;
            }
            
            Debug.Log($"🚶 {gameObject.name}: Deambulando en territorio - H:{healthJabali.hunger:F1} S:{healthJabali.sleepiness:F1}");
        }
        
        _AICharacterVehicle.Wander();
        return TaskStatus.Running;
    }
}
