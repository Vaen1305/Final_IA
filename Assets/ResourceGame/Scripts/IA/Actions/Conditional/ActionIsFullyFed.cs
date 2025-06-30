using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("IA SC/Node Conditional")]
public class ActionIsFullyFed : ActionNodeVehicle
{
    public override TaskStatus OnUpdate()
    {
        if (_AICharacterVehicle.health.IsDead)
        {
            return TaskStatus.Failure;
        }

        // Verificar si está completamente saciado usando los umbrales correctos
        if (_AICharacterVehicle.health is HealthCiervo healthCiervo)
        {
            bool isFullyFed = healthCiervo.IsFull; // Usa fullThreshold = 15f
            if (isFullyFed)
            {
                Debug.Log($"🍽️✅ {gameObject.name}: Completamente saciado (Hambre: {healthCiervo.hunger:F1})");
                return TaskStatus.Success;
            }
            else
            {
                Debug.Log($"🍽️⏰ {gameObject.name}: Aún necesita comer (Hambre: {healthCiervo.hunger:F1})");
                return TaskStatus.Failure;
            }
        }
        else if (_AICharacterVehicle.health is HealthJabali healthJabali)
        {
            bool isFullyFed = healthJabali.IsFull; // Usa fullThreshold = 15f
            if (isFullyFed)
            {
                Debug.Log($"🍽️✅ {gameObject.name}: Completamente saciado (Hambre: {healthJabali.hunger:F1})");
                return TaskStatus.Success;
            }
            else
            {
                Debug.Log($"🍽️⏰ {gameObject.name}: Aún necesita comer (Hambre: {healthJabali.hunger:F1})");
                return TaskStatus.Failure;
            }
        }

        return TaskStatus.Failure;
    }
}
