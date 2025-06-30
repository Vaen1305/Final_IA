using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("IA SC/Node Conditional")]
public class ActionIsFullyRested : ActionNodeVehicle
{
    public override TaskStatus OnUpdate()
    {
        if (_AICharacterVehicle.health.IsDead)
        {
            return TaskStatus.Failure;
        }

        // Verificar si está completamente descansado usando los umbrales correctos
        if (_AICharacterVehicle.health is HealthCiervo healthCiervo)
        {
            bool isFullyRested = healthCiervo.IsRested; // Usa restedThreshold = 10f
            if (isFullyRested)
            {
                Debug.Log($"😴✅ {gameObject.name}: Completamente descansado (Sueño: {healthCiervo.sleepiness:F1})");
                return TaskStatus.Success;
            }
            else
            {
                Debug.Log($"😴⏰ {gameObject.name}: Aún necesita descanso (Sueño: {healthCiervo.sleepiness:F1})");
                return TaskStatus.Failure;
            }
        }
        else if (_AICharacterVehicle.health is HealthLobo healthLobo)
        {
            bool isFullyRested = healthLobo.IsRested; // Usa restedThreshold = 10f
            if (isFullyRested)
            {
                Debug.Log($"😴✅ {gameObject.name}: Completamente descansado (Sueño: {healthLobo.sleepiness:F1})");
                return TaskStatus.Success;
            }
            else
            {
                Debug.Log($"😴⏰ {gameObject.name}: Aún necesita descanso (Sueño: {healthLobo.sleepiness:F1})");
                return TaskStatus.Failure;
            }
        }
        else if (_AICharacterVehicle.health is HealthJabali healthJabali)
        {
            bool isFullyRested = healthJabali.IsRested; // Usa restedThreshold = 10f
            if (isFullyRested)
            {
                Debug.Log($"😴✅ {gameObject.name}: Completamente descansado (Sueño: {healthJabali.sleepiness:F1})");
                return TaskStatus.Success;
            }
            else
            {
                Debug.Log($"😴⏰ {gameObject.name}: Aún necesita descanso (Sueño: {healthJabali.sleepiness:F1})");
                return TaskStatus.Failure;
            }
        }

        return TaskStatus.Failure;
    }
}
