using UnityEngine;

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
            healthCiervo.DepleteStamina(Time.deltaTime);
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
