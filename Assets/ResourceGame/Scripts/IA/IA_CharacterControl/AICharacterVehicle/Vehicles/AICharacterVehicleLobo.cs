using UnityEngine;
using UnityEngine.AI;

public class AICharacterVehicleLobo : AICharacterVehicleLand
{
    private void Start()
    {
        this.LoadComponent();
    }

    public override void MoveToEnemy()
    {
        base.MoveToEnemy();
        var healthLobo = health as HealthLobo;
        if (healthLobo != null)
        {
            // Ya no usar stamina - el nuevo sistema maneja esto automáticamente
            Debug.Log($"🐺 Lobo cazando - Hambre: {healthLobo.hunger:F1} | Sueño: {healthLobo.sleepiness:F1}");
        }
    }

    public void StopMoving()
    {
        if (agent.isOnNavMesh)
        {
            agent.isStopped = true;
        }
    }

    public void ResumeMoving()
    {
        if (agent.isOnNavMesh)
        {
            agent.isStopped = false;
        }
    }
}
