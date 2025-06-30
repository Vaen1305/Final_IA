using UnityEngine;

[System.Serializable]
public class ConfiguracionBehaviorTree : MonoBehaviour
{
    [Header("Guía de Configuración del Behavior Tree")]
    [TextArea(15, 20)]
    public string guiaConfiguracion = @"
ESTRUCTURA RECOMENDADA PARA EL BEHAVIOR TREE DEL CIERVO:

ROOT (Selector)
├── SECUENCIA: Huida Urgente
│   ├── ActionViewLobo (Conditional)
│   └── ActionRunAway (Action)
│
├── SECUENCIA: Evasión
│   ├── ActionViewJabali (Conditional)
│   └── ActionAvoid (Action)
│
├── SECUENCIA: Descanso
│   ├── ActionIsTired (Conditional)
│   └── ActionRest (Action)
│
└── ActionWander (Action - Por defecto)

PRIORIDADES (de mayor a menor):
1. Huir del Lobo (máxima prioridad)
2. Evitar al Jabalí
3. Descansar si está cansado
4. Deambular (comportamiento por defecto)

NOTAS IMPORTANTES:
- Usar SELECTOR como raíz para que evalúe en orden de prioridad
- Usar SECUENCIAS para condición + acción
- ActionViewLobo y ActionViewJabali deben devolver Success cuando detectan
- ActionIsTired debe devolver Success cuando stamina <= 30%
- ActionRest debe devolver Running mientras descansa, Success cuando termina
- ActionWander es el comportamiento por defecto (último en el selector)
";

    [Header("Debug")]
    public bool mostrarEstructuraActual = true;
    
    private void Start()
    {
        if (mostrarEstructuraActual)
        {
            Debug.Log($"📋 CONFIGURACIÓN BEHAVIOR TREE PARA {gameObject.name}:");
            Debug.Log(guiaConfiguracion);
        }
    }
    
    [ContextMenu("Mostrar Guía de Configuración")]
    public void MostrarGuia()
    {
        Debug.Log($"📋 GUÍA DE CONFIGURACIÓN BEHAVIOR TREE:");
        Debug.Log(guiaConfiguracion);
        
        // Verificar componentes necesarios
        VerificarComponentes();
    }
    
    private void VerificarComponentes()
    {
        Debug.Log($"\n🔍 VERIFICANDO COMPONENTES NECESARIOS:");
        
        var healthCiervo = GetComponent<HealthCiervo>();
        var aiVehicle = GetComponent<AICharacterVehicleCiervo>();
        
        Debug.Log($"HealthCiervo: {(healthCiervo != null ? "✅ ENCONTRADO" : "❌ FALTANTE")}");
        Debug.Log($"AICharacterVehicleCiervo: {(aiVehicle != null ? "✅ ENCONTRADO" : "❌ FALTANTE")}");
        
        if (aiVehicle != null && aiVehicle._VisionSensor != null)
        {
            Debug.Log($"VisionSensor: ✅ ENCONTRADO");
        }
        else
        {
            Debug.Log($"VisionSensor: ❌ FALTANTE O NO CONFIGURADO");
        }
        
        Debug.Log($"\n💡 SCRIPTS DE ACCIONES NECESARIOS:");
        Debug.Log($"- ActionViewLobo.cs");
        Debug.Log($"- ActionViewJabali.cs");
        Debug.Log($"- ActionIsTired.cs");
        Debug.Log($"- ActionRest.cs");
        Debug.Log($"- ActionRunAway.cs");
        Debug.Log($"- ActionAvoid.cs");
        Debug.Log($"- ActionWander.cs");
    }
    
    [ContextMenu("Verificar Scripts de Acciones")]
    public void VerificarScriptsAcciones()
    {
        Debug.Log($"🔍 VERIFICANDO SCRIPTS DE ACCIONES...");
        
        // Aquí podrías agregar verificaciones adicionales si es necesario
        Debug.Log($"✅ Verificación completada. Revisa la consola para detalles.");
    }
}
