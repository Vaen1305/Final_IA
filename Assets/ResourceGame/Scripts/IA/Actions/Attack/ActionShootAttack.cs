using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TaskCategory("IA SC/Node Attack")]
public class ActionShootAttack : ActionNodeActions
{
    public override void OnStart()
    {
        base.OnStart();
    }
    public override TaskStatus OnUpdate()
    {
        if (_AICharacterAction.health.IsDead)
            return TaskStatus.Failure;
        SwitchAttackPlay();
        return TaskStatus.Success;
    }

    void SwitchAttackPlay()
    {
        switch (_TypeUnity)
        {
            case TypeAgent.Soldier:
                ((AICharacterActionLandMilitar)_AICharacterAction).Shoot();
                break;
            case TypeAgent.Guard:
                ((AICharacterActionLandGuard)_AICharacterAction).Shoot();
                break;

            default:
                break;
        }
    
    }
}