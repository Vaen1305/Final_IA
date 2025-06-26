using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
[TaskCategory("IA SC/Node Move")]

public class ActionFollowEnemy : ActionNodeVehicle
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
        _AICharacterVehicle.MoveToEnemy();
        //SwitchMoveToAllied();
        return TaskStatus.Success;
    }
    /*void SwitchMoveToAllied()
    {
        switch (_TypeUnity)
        {
            case TypeAgent.Soldier:
                break;
            case TypeAgent.Zombie:
                break;
            default:
                break;
        }
    }   */
}
