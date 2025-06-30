using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory ("IA SC/Node Base")]

public class ActionNode : Action
{
    protected AICharacterVehicle _AICharacterVehicle;
    protected AICharacterAction _AICharacterAction;
    protected Health MyHealth;
    protected VisionSensor _VisionSensor;
    protected TypeAgent _TypeUnity;
    public override void OnStart()
    {
        _AICharacterVehicle = GetComponent<AICharacterVehicle>();
        _AICharacterAction = GetComponent<AICharacterAction>();
        if (_AICharacterVehicle != null)
        {
            _TypeUnity = this._AICharacterVehicle.health.typeAgent;
        }
        else if (_AICharacterAction != null)
        {
            _TypeUnity = this._AICharacterAction.health.typeAgent;
        }

        if (_AICharacterVehicle != null && _AICharacterVehicle.health!=null)
        {
            MyHealth = _AICharacterVehicle.health;
        }
        else if (_AICharacterAction != null && _AICharacterAction.health != null)
        {
            MyHealth = _AICharacterAction.health;
        }

        if (_AICharacterVehicle != null && _AICharacterVehicle._VisionSensor != null)
        {
            _VisionSensor = _AICharacterVehicle._VisionSensor;
        }
        else if (_AICharacterAction != null && _AICharacterAction._VisionSensor != null)
        {
            _VisionSensor = _AICharacterAction._VisionSensor;
        }
        base.OnStart();
    }

    public override TaskStatus OnUpdate()
    {
        // Verificar que el objeto siga vivo
        if (MyHealth != null && MyHealth.IsDead)
        {
            return TaskStatus.Failure;
        }
        return TaskStatus.Success;
    }
}
