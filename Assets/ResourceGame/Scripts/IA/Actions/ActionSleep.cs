using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("IA SC/Node Actions")]
public class ActionSleep : ActionNode
{
    private AICharacterVehicleLobo _vehicle;
    private AICharacterActionLobo _action;

    public override void OnStart()
    {
        base.OnStart();
        _vehicle = GetComponent<AICharacterVehicleLobo>();
        _action = GetComponent<AICharacterActionLobo>();
        if (_vehicle != null)
        {
            _vehicle.StopMoving();
        }
    }

    public override TaskStatus OnUpdate()
    {
        if (_vehicle == null || _action == null)
        {
            return TaskStatus.Failure;
        }

        _action.Sleep(); // Llama a la regeneración de estamina

        var healthLobo = MyHealth as HealthLobo;
        // Termina cuando la estamina está llena
        if (healthLobo != null && healthLobo.stamina >= healthLobo.maxStamina)
        {
             return TaskStatus.Success;
        }
        
        return TaskStatus.Running;
    }

    public override void OnEnd()
    {
        if (_vehicle != null)
        {
            _vehicle.ResumeMoving();
        }
    }
}

