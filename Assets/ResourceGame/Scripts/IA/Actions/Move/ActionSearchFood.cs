using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("IA SC/Node Move")]
public class ActionSearchFood : ActionNodeVehicle
{
    public override TaskStatus OnUpdate()
    {
        if (_AICharacterVehicle.health.IsDead)
        {
            return TaskStatus.Failure;
        }
        // Simula buscar comida deambulando
        _AICharacterVehicle.Wander();
        return TaskStatus.Success;
    }
}
