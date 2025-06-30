using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[System.Serializable]
public class SimpleDebugger : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("🎯 DEBUGGER INICIADO - " + gameObject.name);
        
        var behaviorTree = GetComponent<BehaviorTree>();
        var health = GetComponent<Health>();
        var visionSensor = GetComponent<VisionSensor>();
        
        Debug.Log($"BehaviorTree: {(behaviorTree != null ? "✅" : "❌")}");
        Debug.Log($"Health: {(health != null ? "✅" : "❌")}");
        Debug.Log($"VisionSensor: {(visionSensor != null ? "✅" : "❌")}");
        
        if (behaviorTree != null)
        {
            Debug.Log($"External Behavior asignado: {(behaviorTree.ExternalBehavior != null ? "✅" : "❌")}");
            Debug.Log($"BehaviorTree habilitado: {behaviorTree.enabled}");
        }
        
        if (visionSensor != null && visionSensor.MainVision != null)
        {
            Debug.Log($"VisionSensor Distance: {visionSensor.MainVision.distance}");
            Debug.Log($"VisionSensor Angle: {visionSensor.MainVision.angle}");
        }
    }
    
    private void Update()
    {
        // Cada 3 segundos, verificar estado
        if (Time.time % 3f < Time.deltaTime)
        {
            var visionSensor = GetComponent<VisionSensor>();
            if (visionSensor != null)
            {
                if (visionSensor.EnemyView != null)
                {
                    Debug.Log($"🔍 DETECTADO: {visionSensor.EnemyView.name}");
                }
                else
                {
                    Debug.Log($"👁️ No hay enemigos detectados");
                }
            }
            
            var behaviorTree = GetComponent<BehaviorTree>();
            if (behaviorTree != null)
            {
                Debug.Log($"BehaviorTree ejecutándose: {behaviorTree.enabled}");
                
                // Verificar si el árbol está realmente corriendo
                if (behaviorTree.ExternalBehavior != null)
                {
                    Debug.Log($"📋 External Behavior: {behaviorTree.ExternalBehavior.name}");
                }
                
                // Verificar el estado del árbol más detallado
                try 
                {
                    Debug.Log($"🔄 BehaviorTree IsActive: {behaviorTree.gameObject.activeInHierarchy}");
                }
                catch (System.Exception e)
                {
                    Debug.LogError($"Error checking BehaviorTree: {e.Message}");
                }
            }
        }
    }
}
