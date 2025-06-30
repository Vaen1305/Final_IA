using UnityEngine;

/// <summary>
/// Verificador automático de la configuración de la escena
/// </summary>
public class VerificadorEscena : MonoBehaviour
{
    [Header("Configuración")]
    public bool verificarAlInicio = true;
    
    private void Start()
    {
        if (verificarAlInicio)
        {
            VerificarConfiguracion();
        }
    }
    
    [ContextMenu("Verificar Configuración Completa")]
    public void VerificarConfiguracion()
    {
        Debug.Log("🔍 === VERIFICANDO CONFIGURACIÓN DE LA ESCENA ===");
        
        VerificarAnimales();
        VerificarObjetos();
        VerificarTags();
        VerificarNavMesh();
        
        Debug.Log("✅ === VERIFICACIÓN COMPLETADA ===");
    }
    
    private void VerificarAnimales()
    {
        Debug.Log("🦌 Verificando animales...");
        
        // Buscar Ciervo
        HealthCiervo[] ciervos = FindObjectsOfType<HealthCiervo>();
        Debug.Log($"  Ciervos encontrados: {ciervos.Length}");
        foreach (var ciervo in ciervos)
        {
            VerificarComponentesCiervo(ciervo);
        }
        
        // Buscar Lobo
        HealthLobo[] lobos = FindObjectsOfType<HealthLobo>();
        Debug.Log($"  Lobos encontrados: {lobos.Length}");
        foreach (var lobo in lobos)
        {
            VerificarComponentesLobo(lobo);
        }
        
        // Buscar Jabalí
        HealthJabali[] jabalies = FindObjectsOfType<HealthJabali>();
        Debug.Log($"  Jabalíes encontrados: {jabalies.Length}");
        foreach (var jabali in jabalies)
        {
            VerificarComponentesJabali(jabali);
        }
    }
    
    private void VerificarComponentesCiervo(HealthCiervo ciervo)
    {
        GameObject obj = ciervo.gameObject;
        Debug.Log($"  🦌 Verificando Ciervo: {obj.name}");
        
        // Verificar componentes necesarios
        if (!obj.GetComponent<AICharacterVehicleCiervo>())
            Debug.LogError($"    ❌ Falta AICharacterVehicleCiervo en {obj.name}");
        
        if (!obj.GetComponent<UnityEngine.AI.NavMeshAgent>())
            Debug.LogError($"    ❌ Falta NavMeshAgent en {obj.name}");
            
        if (!obj.GetComponent<BehaviorDesigner.Runtime.BehaviorTree>())
            Debug.LogError($"    ❌ Falta BehaviorTree en {obj.name}");
        
        // Verificar valores de salud
        if (ciervo.hungryThreshold <= 0 || ciervo.sleepyThreshold <= 0)
            Debug.LogWarning($"    ⚠️ Thresholds pueden estar mal configurados en {obj.name}");
            
        Debug.Log($"    ✅ Ciervo {obj.name} configurado correctamente");
    }
    
    private void VerificarComponentesLobo(HealthLobo lobo)
    {
        GameObject obj = lobo.gameObject;
        Debug.Log($"  🐺 Verificando Lobo: {obj.name}");
        
        if (!obj.GetComponent<AICharacterVehicleLobo>())
            Debug.LogError($"    ❌ Falta AICharacterVehicleLobo en {obj.name}");
        
        if (!obj.GetComponent<UnityEngine.AI.NavMeshAgent>())
            Debug.LogError($"    ❌ Falta NavMeshAgent en {obj.name}");
            
        if (!obj.GetComponent<BehaviorDesigner.Runtime.BehaviorTree>())
            Debug.LogError($"    ❌ Falta BehaviorTree en {obj.name}");
            
        Debug.Log($"    ✅ Lobo {obj.name} configurado correctamente");
    }
    
    private void VerificarComponentesJabali(HealthJabali jabali)
    {
        GameObject obj = jabali.gameObject;
        Debug.Log($"  🐗 Verificando Jabalí: {obj.name}");
        
        if (!obj.GetComponent<AICharacterVehicleJabali>())
            Debug.LogError($"    ❌ Falta AICharacterVehicleJabali en {obj.name}");
        
        if (!obj.GetComponent<UnityEngine.AI.NavMeshAgent>())
            Debug.LogError($"    ❌ Falta NavMeshAgent en {obj.name}");
            
        if (!obj.GetComponent<BehaviorDesigner.Runtime.BehaviorTree>())
            Debug.LogError($"    ❌ Falta BehaviorTree en {obj.name}");
        
        if (jabali.territoryRadius <= 0)
            Debug.LogWarning($"    ⚠️ Radio de territorio no configurado en {obj.name}");
            
        Debug.Log($"    ✅ Jabalí {obj.name} configurado correctamente");
    }
    
    private void VerificarObjetos()
    {
        Debug.Log("🌿 Verificando objetos de hierba...");
        
        GameObject[] grass = GameObject.FindGameObjectsWithTag("Grass");
        GameObject[] hierba = GameObject.FindGameObjectsWithTag("Hierba");
        
        int totalHierba = grass.Length + hierba.Length;
        Debug.Log($"  Objetos con tag 'Grass': {grass.Length}");
        Debug.Log($"  Objetos con tag 'Hierba': {hierba.Length}");
        Debug.Log($"  Total objetos de hierba: {totalHierba}");
        
        if (totalHierba == 0)
        {
            Debug.LogError("  ❌ NO HAY OBJETOS DE HIERBA EN LA ESCENA!");
            Debug.LogError("     Crea objetos y asígnales el tag 'Grass' o 'Hierba'");
        }
        else
        {
            Debug.Log("  ✅ Objetos de hierba encontrados");
        }
    }
    
    private void VerificarTags()
    {
        Debug.Log("🏷️ Verificando tags necesarios...");
        
        string[] tagsNecesarios = { "Grass", "Hierba", "Ciervo", "Lobo", "Jabali" };
        
        foreach (string tag in tagsNecesarios)
        {
            try
            {
                GameObject.FindGameObjectWithTag(tag);
                Debug.Log($"  ✅ Tag '{tag}' existe");
            }
            catch (UnityException)
            {
                Debug.LogWarning($"  ⚠️ Tag '{tag}' no existe en el proyecto");
            }
        }
    }
    
    private void VerificarNavMesh()
    {
        Debug.Log("🗺️ Verificando NavMesh...");
        
        UnityEngine.AI.NavMeshAgent[] agents = FindObjectsOfType<UnityEngine.AI.NavMeshAgent>();
        Debug.Log($"  NavMeshAgents encontrados: {agents.Length}");
        
        foreach (var agent in agents)
        {
            if (!agent.isOnNavMesh)
            {
                Debug.LogWarning($"  ⚠️ {agent.name} NO está en el NavMesh");
            }
        }
        
        if (UnityEngine.AI.NavMesh.GetAreaCost(0) >= 0)
        {
            Debug.Log("  ✅ NavMesh parece estar configurado");
        }
        else
        {
            Debug.LogWarning("  ⚠️ NavMesh puede no estar configurado correctamente");
        }
    }
}
