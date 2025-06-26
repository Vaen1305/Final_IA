using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacterVehicleLandCivil : IACharacterHumanVehicle
{
    public UnityEngine.Color WanderPosition;
    public Vector3 posOfHome;
    private void Start()
    {
        this.LoadComponent();
    }
    public override void LoadComponent()
    {
        base.LoadComponent();

    }
    public void MoveToCave()
    {
        if (((VisionSensorCivil)_VisionSensor).AccommodationView != null)
            MoveToPosition(((VisionSensorCivil)_VisionSensor).AccommodationView.transform.position);
    
    }
    public override void MoveToEvadeEnemy()
    {
        base.MoveToEvadeEnemy();
    }
     
     
    private void OnDrawGizmos()
    {
        if (!IsDrawGizmos) return;

        Gizmos.color = WanderPosition;
        Gizmos.DrawWireSphere(pointWander, 1f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(positionEvade, 1f);
    }
    public void BackToPatrolZone()
    {
        MoveToPosition(posOfHome);
    }
}
