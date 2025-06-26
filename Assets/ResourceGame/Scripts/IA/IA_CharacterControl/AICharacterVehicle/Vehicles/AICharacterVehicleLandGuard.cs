using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacterVehicleLandGuard : AICharacterVehicleLand
{
    public UnityEngine.Color WanderPosition;
    public Vector3 posOfGuard;
    private void Start()
    {
        this.LoadComponent();
    }
    public override void LoadComponent()
    {
        base.LoadComponent();
    }

    private void OnDrawGizmos()
    {
        if (!IsDrawGizmos) return;

        Gizmos.color = WanderPosition;
        Gizmos.DrawWireSphere(pointWander, 1f);
    }
    public void BackToPatrolZone()
    {
        MoveToPosition(posOfGuard);
    }
}
