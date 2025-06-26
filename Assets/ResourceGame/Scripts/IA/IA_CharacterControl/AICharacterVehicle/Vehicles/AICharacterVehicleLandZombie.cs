using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AICharacterVehicleLandZombie : AICharacterVehicleLand
{
    public UnityEngine.Color WanderPosition;
    private void Start()
    {
        this.LoadComponent();
    }
    public override void LoadComponent()
    {
        base.LoadComponent();

    }
    public void UpgradeDamage()
    {
        if (health.IsDead) return;
        float b = health._bonus.Percent();
        if (b > 0.5f)
            agent.speed *= 2;

    }
    private void OnDrawGizmos()
    {
        if (!IsDrawGizmos) return;

        Gizmos.color = WanderPosition;
        Gizmos.DrawWireSphere(pointWander, 1f);
    }
}
