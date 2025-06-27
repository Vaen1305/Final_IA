using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("IA SC/ Node View")]
public class ActionViewLobo : ActionView
{
    public override TaskStatus OnUpdate()
    {
        if (_VisionSensor != null && _VisionSensor.EnemyView != null)
        {
            if (_VisionSensor.EnemyView.typeAgent == TypeAgent.Lobo)
            {
                return TaskStatus.Success;
            }
        }
        return TaskStatus.Failure;
    }
}
