using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TaskCategory("IA SC/Node Move")]
public class ActionEvade : ActionNodeVehicle
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
            case TypeAgent.Soldier:
                if((_VisionSensor.EnemyView))
                ((AICharacterVehicleLandMilitar)_AICharacterVehicle).MoveToEvadeEnemy();
                break;
            case TypeAgent.Civil:
                ((AICharacterVehicleLandCivil)_AICharacterVehicle).MoveToEvadeEnemy();
                break;
            case TypeAgent.Guard:
                break;
            case TypeAgent.Ciervo:
                ((AICharacterVehicleCiervo)_AICharacterVehicle).MoveToEvadeEnemy();
                break;
            case TypeAgent.Jabali:
                ((AICharacterVehicleJabali)_AICharacterVehicle).MoveToEvadeEnemy();
                break;
            default:
                break;
        }

    }
}
