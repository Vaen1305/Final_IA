using UnityEngine;
using BehaviorDesigner.Runtime;

/// <summary>
/// Validador de configuración de Behavior Trees
/// Detecta errores comunes que pueden causar bugs
/// </summary>
public class ValidadorBehaviorTrees : MonoBehaviour
{
    [Header("Referencias de Animales")]
    public BehaviorTree ciervoTree;
    public BehaviorTree loboTree;
    public BehaviorTree jabaliTree;
    
    [Header("Configuración")]
    public bool validarAlInicio = true;
    public bool mostrarErrores = true;
    public bool mostrarAdvertencias = true;
    
    private void Start()
    {
        if (validarAlInicio)
        {
            ValidarTodosLosTrees();
        }
    }
    
    [ContextMenu("Validar Behavior Trees")]
    public void ValidarTodosLosTrees()
    {
        Debug.Log("🔍 INICIANDO VALIDACIÓN DE BEHAVIOR TREES...");
        
        if (ciervoTree != null)
            ValidarTreeCiervo();
        if (loboTree != null)  
            ValidarTreeLobo();
        if (jabaliTree != null)
            ValidarTreeJabali();
            
        Debug.Log("✅ VALIDACIÓN COMPLETADA");
    }
    
    private void ValidarTreeCiervo()
    {
        Debug.Log("🦌 Validando Behavior Tree del CIERVO...");
        
        // Verificar que tenga las acciones correctas
        var variables = ciervoTree.GetAllVariables();
        
        // Verificar estructura esperada
        bool tieneRunAway = BuscarAccionEnTree(ciervoTree, "ActionRunAway");
        bool tieneViewLobo = BuscarAccionEnTree(ciervoTree, "ActionViewLobo");
        bool tieneViewJabali = BuscarAccionEnTree(ciervoTree, "ActionViewJabali");
        bool tieneSleep = BuscarAccionEnTree(ciervoTree, "ActionSleep");
        bool tieneSearchFood = BuscarAccionEnTree(ciervoTree, "ActionSearchFood");
        bool tieneEat = BuscarAccionEnTree(ciervoTree, "ActionEat");
        bool tieneWander = BuscarAccionEnTree(ciervoTree, "ActionWander");
        
        // Verificar que NO tenga acciones incorrectas
        bool tieneHunt = BuscarAccionEnTree(ciervoTree, "ActionHuntCiervo");
        bool tieneDefend = BuscarAccionEnTree(ciervoTree, "ActionDefendTerritory");
        
        if (!tieneRunAway)
            LogError("CIERVO: Falta ActionRunAway - No podrá huir");
        if (!tieneViewLobo || !tieneViewJabali)
            LogError("CIERVO: Falta detección de depredadores");
        if (!tieneSleep)
            LogWarning("CIERVO: Falta ActionSleep - No podrá dormir");
        if (!tieneSearchFood || !tieneEat)
            LogWarning("CIERVO: Falta buscar/comer comida");
        if (!tieneWander)
            LogWarning("CIERVO: Falta ActionWander - No patrullará");
            
        if (tieneHunt)
            LogError("CIERVO: Tiene ActionHuntCiervo - Los ciervos no cazan!");
        if (tieneDefend)
            LogError("CIERVO: Tiene ActionDefendTerritory - Los ciervos no defienden territorio!");
    }
    
    private void ValidarTreeLobo()
    {
        Debug.Log("🐺 Validando Behavior Tree del LOBO...");
        
        bool tieneAvoid = BuscarAccionEnTree(loboTree, "ActionAvoid");
        bool tieneViewJabali = BuscarAccionEnTree(loboTree, "ActionViewJabali");
        bool tieneSleep = BuscarAccionEnTree(loboTree, "ActionSleep");
        bool tieneHunt = BuscarAccionEnTree(loboTree, "ActionHuntCiervo");
        bool tieneViewCiervo = BuscarAccionEnTree(loboTree, "ActionViewCiervo");
        bool tieneWander = BuscarAccionEnTree(loboTree, "ActionWander");
        
        // Verificar que NO tenga acciones incorrectas
        bool tieneSearchFood = BuscarAccionEnTree(loboTree, "ActionSearchFood");
        bool tieneEat = BuscarAccionEnTree(loboTree, "ActionEat");
        bool tieneDefend = BuscarAccionEnTree(loboTree, "ActionDefendTerritory");
        
        if (!tieneAvoid || !tieneViewJabali)
            LogError("LOBO: Falta evitar jabalí - Puede pelear incorrectamente");
        if (!tieneSleep)
            LogWarning("LOBO: Falta ActionSleep - No podrá dormir");
        if (!tieneHunt || !tieneViewCiervo)
            LogWarning("LOBO: Falta cazar ciervo - No se alimentará");
        if (!tieneWander)
            LogWarning("LOBO: Falta ActionWander - No patrullará");
            
        if (tieneSearchFood)
            LogError("LOBO: Tiene ActionSearchFood - Los lobos no buscan hierba!");
        if (tieneEat)
            LogError("LOBO: Tiene ActionEat - Los lobos no comen hierba!");
        if (tieneDefend)
            LogError("LOBO: Tiene ActionDefendTerritory - Los lobos no defienden territorio!");
    }
    
    private void ValidarTreeJabali()
    {
        Debug.Log("🐗 Validando Behavior Tree del JABALÍ...");
        
        bool tieneDefend = BuscarAccionEnTree(jabaliTree, "ActionDefendTerritory");
        bool tieneViewIntruder = BuscarAccionEnTree(jabaliTree, "ActionViewIntruder");
        bool tieneSleep = BuscarAccionEnTree(jabaliTree, "ActionSleep");
        bool tieneSearchFood = BuscarAccionEnTree(jabaliTree, "ActionSearchFood");
        bool tieneEat = BuscarAccionEnTree(jabaliTree, "ActionEat");
        bool tienePatrol = BuscarAccionEnTree(jabaliTree, "ActionPatrolTerritory");
        
        // Verificar que NO tenga acciones incorrectas
        bool tieneHunt = BuscarAccionEnTree(jabaliTree, "ActionHuntCiervo");
        bool tieneRunAway = BuscarAccionEnTree(jabaliTree, "ActionRunAway");
        
        if (!tieneDefend || !tieneViewIntruder)
            LogError("JABALÍ: Falta defender territorio - No atacará intrusos");
        if (!tieneSleep)
            LogWarning("JABALÍ: Falta ActionSleep - No podrá dormir");
        if (!tieneSearchFood || !tieneEat)
            LogWarning("JABALÍ: Falta buscar/comer comida");
        if (!tienePatrol)
            LogWarning("JABALÍ: Falta ActionPatrolTerritory - No patrullará su zona");
            
        if (tieneHunt)
            LogError("JABALÍ: Tiene ActionHuntCiervo - Los jabalíes no cazan!");
        if (tieneRunAway)
            LogWarning("JABALÍ: Tiene ActionRunAway - Los jabalíes no deberían huir (son agresivos)");
    }
    
    private bool BuscarAccionEnTree(BehaviorTree tree, string nombreAccion)
    {
        // Esta es una implementación simplificada
        // En un caso real, necesitarías recorrer el árbol de nodos
        string treeString = tree.ToString();
        return treeString.Contains(nombreAccion);
    }
    
    private void LogError(string mensaje)
    {
        if (mostrarErrores)
            Debug.LogError($"❌ ERROR: {mensaje}");
    }
    
    private void LogWarning(string mensaje)
    {
        if (mostrarAdvertencias)
            Debug.LogWarning($"⚠️ ADVERTENCIA: {mensaje}");
    }
}
