using UnityEngine;
using BehaviorDesigner.Runtime;

/// <summary>
/// Script simplificado para verificar Behavior Trees
/// </summary>
public class BehaviorTreeDebugger : MonoBehaviour
{
    void Start()
    {
        var behaviorTree = GetComponent<BehaviorTree>();
        if (behaviorTree != null)
        {
            Debug.Log($"🔍 VERIFICANDO BEHAVIOR TREE: {gameObject.name}");
            Debug.Log($"External Behavior: {(behaviorTree.ExternalBehavior != null ? behaviorTree.ExternalBehavior.name : "NINGUNO")}");
            Debug.Log($"BehaviorTree habilitado: {behaviorTree.enabled}");
            Debug.Log($"GameObject activo: {gameObject.activeInHierarchy}");
        }
        else
        {
            Debug.LogWarning($"❌ No se encontró BehaviorTree en {gameObject.name}");
        }
        
        // Mostrar actions que deberían estar disponibles
        ListKnownActions();
        
        // Auto-destruir después de debuggear
        Destroy(this, 1f);
    }
    
    private void ListKnownActions()
    {
        Debug.Log("📋 ACTIONS QUE DEBERÍAN ESTAR DISPONIBLES PARA CIERVO:");
        Debug.Log("  - ActionNotViewEnemy (recién creado)");
        Debug.Log("  - ActionCiervoNotViewPredator (recién creado)");
        Debug.Log("  - ActionWander");
        Debug.Log("  - ActionRunAway");
        Debug.Log("  - ActionSleep");
        Debug.Log("  - ActionSearchFood");
        Debug.Log("  - ActionEat");
        Debug.Log("  - ActionIsHungry");
        Debug.Log("  - ActionIsSleepy");
        Debug.Log("  - ActionIsFullyFed");
        Debug.Log("  - ActionIsFullyRested");
        Debug.Log("  - ActionViewLobo");
        
        Debug.Log("✅ TODOS LOS SCRIPTS DEBERÍAN ESTAR COMPILADOS CORRECTAMENTE");
    }
}
