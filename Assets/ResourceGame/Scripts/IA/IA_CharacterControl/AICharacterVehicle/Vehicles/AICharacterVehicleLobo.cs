using UnityEngine;

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
            healthLobo.DepleteStamina(Time.deltaTime);
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
