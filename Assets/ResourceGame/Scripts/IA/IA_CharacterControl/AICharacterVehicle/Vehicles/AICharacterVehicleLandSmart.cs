using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacterVehicleLandSmart : AICharacterVehicleLand
{
    public override void LoadComponent()
    {
        base.LoadComponent();

    }
    /*#region move
    public override void MoveToPosition(Vector3 position)
    {
        agent.SetDestination(position);
    }

    public override void MoveToPositionWander(Vector3 pos)
    {
        Debug.Log("Move Wander");
    }
    #endregion

    #region Look
    public override void LookPosition(Vector3 posLook)
    {
        Debug.Log("Look position");
    }

    #endregion
    public virtual void Evade()
    {

    }   */
}
