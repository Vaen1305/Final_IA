using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TaskCategory("IA SC/Node Attack")]
public class ActionAttack : ActionNodeActions
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
                ((AICharacterActionLandMilitar)_AICharacterAction).Attack();
                break;
            case TypeAgent.Zombie:
                ((AICharacterActionLandZombie)_AICharacterAction).Attack();
                break;
            case TypeAgent.Guard:
                ((AICharacterActionLandGuard)_AICharacterAction).Attack();
                break;
            case TypeAgent.Lobo:
                ((AICharacterActionLobo)_AICharacterAction).Attack();
                break;
            case TypeAgent.Jabali:
                ((AICharacterActionJabali)_AICharacterAction).Attack();
                break;
            default:
                break;
        }
    
    }
}