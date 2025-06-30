using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("IA SC/Debug")]
public class ActionDebugTest : ActionNode
{
    public override TaskStatus OnUpdate()
    {
        Debug.Log("🔧 ActionDebugTest - OnUpdate ejecutándose");
        
        // Verificar que ActionNode puede obtener los componentes
        Debug.Log($"_AICharacterVehicle: {(_AICharacterVehicle != null ? "✅" : "❌")}");
        Debug.Log($"_AICharacterAction: {(_AICharacterAction != null ? "✅" : "❌")}");
        Debug.Log($"MyHealth: {(MyHealth != null ? "✅" : "❌")}");
        Debug.Log($"_VisionSensor: {(_VisionSensor != null ? "✅" : "❌")}");
        Debug.Log($"_TypeUnity: {_TypeUnity}");
        
        if (MyHealth != null)
        {
            Debug.Log($"Health Type: {MyHealth.typeAgent}");
            Debug.Log($"Health Dead: {MyHealth.IsDead}");
        }
        
        if (_VisionSensor != null)
        {
            Debug.Log($"VisionSensor EnemyView: {(_VisionSensor.EnemyView != null ? _VisionSensor.EnemyView.name : "null")}");
        }
        
        return TaskStatus.Success;
    }
}
