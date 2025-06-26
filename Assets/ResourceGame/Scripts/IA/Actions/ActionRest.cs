using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("IA SC/Node Actions")]
public class ActionRest : ActionNode
{
    private AICharacterVehicleCiervo _vehicle;
    private AICharacterActionCiervo _action;

    public override void OnStart()
    {
        base.OnStart();
        _vehicle = GetComponent<AICharacterVehicleCiervo>();
        _action = GetComponent<AICharacterActionCiervo>();
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

        _action.Rest(); // Llama a la regeneración de estamina

        var healthCiervo = MyHealth as HealthCiervo;
        // Termina cuando la estamina está llena
        if (healthCiervo != null && healthCiervo.stamina >= healthCiervo.maxStamina)
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