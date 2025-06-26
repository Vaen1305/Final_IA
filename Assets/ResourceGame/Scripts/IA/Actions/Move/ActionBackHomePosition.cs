using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TaskCategory("IA SC/Node Move")]
public class ActionBackHomePosition : ActionNodeVehicle
{
    public override void OnStart()
    {
        base.OnStart();
    }
    public override TaskStatus OnUpdate()
    {
        if (_AICharacterVehicle.health.IsDead)
        {
            return TaskStatus.Failure;
        }

        SwitchCharacter();
        return TaskStatus.Success;
    }
    void SwitchCharacter()
    {

        switch (_TypeUnity)
        {
            case TypeAgent.Civil:
                ((AICharacterVehicleLandCivil)_AICharacterVehicle).BackToPatrolZone();
                break;
            case TypeAgent.Guard:
                ((AICharacterVehicleLandGuard)_AICharacterVehicle).BackToPatrolZone();
                break;
            default:
                break;
        }

    }
}
