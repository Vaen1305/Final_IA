using UnityEngine;

/// <summary>
/// Script de validación final para verificar que todos los components necesarios estén compilando
/// </summary>
public class CompilationValidator : MonoBehaviour
{
    void Start()
    {
        Debug.Log("🔍 VALIDANDO COMPILACIÓN DE TODOS LOS SCRIPTS...");
        
        // Verificar que las nuevas actions existan
        bool actionNotViewEnemyExists = System.Type.GetType("ActionNotViewEnemy") != null;
        bool actionCiervoNotViewPredatorExists = System.Type.GetType("ActionCiervoNotViewPredator") != null;
        
        Debug.Log($"ActionNotViewEnemy existe: {(actionNotViewEnemyExists ? "✅" : "❌")}");
        Debug.Log($"ActionCiervoNotViewPredator existe: {(actionCiervoNotViewPredatorExists ? "✅" : "❌")}");
        
        // Verificar clases principales
        bool healthLoboExists = System.Type.GetType("HealthLobo") != null;
        bool healthCiervoExists = System.Type.GetType("HealthCiervo") != null;
        bool actionWanderExists = System.Type.GetType("ActionWander") != null;
        
        Debug.Log($"HealthLobo existe: {(healthLoboExists ? "✅" : "❌")}");
        Debug.Log($"HealthCiervo existe: {(healthCiervoExists ? "✅" : "❌")}");
        Debug.Log($"ActionWander existe: {(actionWanderExists ? "✅" : "❌")}");
        
        if (actionNotViewEnemyExists && actionCiervoNotViewPredatorExists && 
            healthLoboExists && healthCiervoExists && actionWanderExists)
        {
            Debug.Log("🎉 TODAS LAS CLASES PRINCIPALES ESTÁN COMPILANDO CORRECTAMENTE!");
            Debug.Log("🔧 Ahora puedes abrir el Behavior Tree del Ciervo y reemplazar los nodos 'Unknown' con:");
            Debug.Log("   - ActionNotViewEnemy");
            Debug.Log("   - ActionCiervoNotViewPredator");
            Debug.Log("   - ActionWander");
        }
        else
        {
            Debug.LogError("❌ FALTAN ALGUNAS CLASES - Revisa los errores de compilación");
        }
        
        // Auto-destruir
        Destroy(this, 2f);
    }
}
