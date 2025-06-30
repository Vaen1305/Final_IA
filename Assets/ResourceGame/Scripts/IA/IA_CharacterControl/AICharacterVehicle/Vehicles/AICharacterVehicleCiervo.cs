using UnityEngine;
using UnityEngine.AI;

public class AICharacterVehicleCiervo : AICharacterVehicleLand
{
    private void Start()
    {
        this.LoadComponent();
    }

    public override void MoveToEvadeEnemy()
    {
        base.MoveToEvadeEnemy();
        var healthCiervo = health as HealthCiervo;
        if (healthCiervo != null)
        {
            // No consumir recursos extra al huir - el huir es una acción de supervivencia
            // Solo modificar velocidad si es necesario
            if (Agent != null)
            {
                Agent.speed = 6f; // Velocidad de huida rápida
            }
            Debug.Log($"🏃 Ciervo huyendo - Hambre: {healthCiervo.hunger:F1} | Sueño: {healthCiervo.sleepiness:F1}");
        }
    }

    public override void Wander()
    {
        base.Wander();
        var healthCiervo = health as HealthCiervo;
        if (healthCiervo != null)
        {
            // Velocidad normal para deambular
            if (Agent != null)
            {
                Agent.speed = 3.5f; // Velocidad normal
            }
            
            // El deambular no consume recursos adicionales - eso se maneja en HealthCiervo.Update()
            Debug.Log($"🚶 Ciervo deambulando - Hambre: {healthCiervo.hunger:F1} | Sueño: {healthCiervo.sleepiness:F1}");
        }
    }

    public void StopMoving()
    {
        if (Agent != null && Agent.isOnNavMesh)
        {
            Agent.isStopped = true;
        }
    }

    public void ResumeMoving()
    {
        if (Agent != null && Agent.isOnNavMesh)
        {
            Agent.isStopped = false;
        }
    }
}
