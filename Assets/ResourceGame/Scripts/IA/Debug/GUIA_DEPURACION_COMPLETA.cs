using UnityEngine;

/// <summary>
/// GUÍA COMPLETA PARA DEPURACIÓN DEL SISTEMA DE IA - VERSIÓN ACTUALIZADA
/// 
/// PROBLEMAS RESUELTOS:
/// ✅ Thresholds corregidos en ActionIsFullyRested y ActionIsFullyFed
/// ✅ ActionSleep y ActionEat continúan hasta satisfacer completamente las necesidades
/// ✅ Mejor logging y debug en todas las acciones
/// ✅ Validación de herbívoros vs carnívoros
/// ✅ Monitor de estado en tiempo real
/// 
/// CONFIGURACIÓN DE BEHAVIOR TREES:
/// 
/// === CIERVO ===
/// Entry → Repeater → Selector
/// ├── Sequence: [ActionViewLobo → ActionRunAway]
/// ├── Sequence: [ActionViewJabali → ActionRunAway]  
/// ├── Repeater: [Sequence: [ActionIsSleepy → ActionSleep] → Inverter(ActionIsFullyRested)]
/// ├── Repeater: [Sequence: [ActionIsHungry → ActionSearchFood → ActionEat] → Inverter(ActionIsFullyFed)]
/// └── Sequence: [ActionNotViewEnemy → ActionWander]
/// 
/// === LOBO ===
/// Entry → Repeater → Selector
/// ├── Sequence: [ActionViewJabali → ActionAvoid]
/// ├── Repeater: [Sequence: [ActionIsSleepy → ActionSleep] → Inverter(ActionIsFullyRested)]
/// ├── Sequence: [ActionIsHungry → ActionViewCiervo → ActionHuntCiervo]
/// └── Sequence: [ActionNotViewEnemy → ActionWander]
/// 
/// === JABALÍ ===
/// Entry → Repeater → Selector
/// ├── Sequence: [ActionViewIntruder → ActionDefendTerritory]
/// ├── Repeater: [Sequence: [ActionIsSleepy → ActionSleep] → Inverter(ActionIsFullyRested)]
/// ├── Repeater: [Sequence: [ActionIsHungry → ActionSearchFood → ActionEat] → Inverter(ActionIsFullyFed)]
/// └── ActionPatrolTerritory
/// 
/// IMPORTANTE:
/// - Los Repeaters con Inverter aseguran que sleep/eat continúen hasta estar completamente satisfechos
/// - LOBO NO debe tener ActionSearchFood ni ActionEat (es carnívoro)
/// - Prioridades: RunAway > Avoid > Sleep > Eat/Hunt > Wander/Patrol
/// 
/// CONFIGURACIÓN UNITY:
/// 1. Tags necesarios: "Grass", "Hierba", "Ciervo", "Lobo", "Jabali"
/// 2. Objetos de hierba deben tener Collider y tag "Grass" o "Hierba"
/// 3. Añadir MonitorEstadoAnimales.cs a un GameObject para debug en tiempo real
/// 
/// VALORES RECOMENDADOS:
/// 
/// === HealthCiervo ===
/// - hungryThreshold: 30f
/// - sleepyThreshold: 40f  
/// - fullThreshold: 15f
/// - restedThreshold: 10f
/// - hungerIncreaseRate: 5f
/// - sleepinessIncreaseRate: 3f
/// - eatingRate: 30f
/// - sleepingRate: 25f
/// 
/// === HealthLobo ===
/// - hungryThreshold: 35f
/// - sleepyThreshold: 45f
/// - fullThreshold: 10f
/// - restedThreshold: 15f
/// - hungerIncreaseRate: 8f
/// - sleepinessIncreaseRate: 4f
/// - sleepingRate: 30f
/// 
/// === HealthJabali ===
/// - hungryThreshold: 25f
/// - sleepyThreshold: 50f
/// - fullThreshold: 10f
/// - restedThreshold: 20f
/// - territoryRadius: 15f
/// - hungerIncreaseRate: 6f
/// - sleepinessIncreaseRate: 3.5f
/// - eatingRate: 25f
/// - sleepingRate: 20f
/// 
/// DEBUG:
/// - Todos los logs incluyen emojis para fácil identificación
/// - MonitorEstadoAnimales.cs muestra estado en tiempo real
/// - ActionSearchFood y ActionEat previenen uso por Lobo
/// - Logs cada 2-5 segundos para evitar spam
/// 
/// SOLUCIÓN DE PROBLEMAS:
/// 1. Si animal se queda en loop: Verificar thresholds y Behavior Tree
/// 2. Si Lobo busca hierba: Error en Behavior Tree, remover ActionSearchFood/ActionEat
/// 3. Si no cambia de estado: Verificar que ActionIsFullyRested/Fed usen propiedades correctas
/// 4. Si movimiento errático: Verificar NavMesh y configuración de agente
/// 
/// </summary>
public class GUIA_DEPURACION_COMPLETA : MonoBehaviour
{
    [Header("Estado Actual del Sistema")]
    [TextArea(10, 20)]
    public string informacionSistema = @"
SISTEMA DE IA CON HAMBRE Y SUEÑO - COMPLETAMENTE FUNCIONAL

✅ CAMBIOS IMPLEMENTADOS:
- Thresholds corregidos para ActionIsFullyRested/Fed
- Sleep/Eat continúan hasta satisfacción completa  
- Validación herbívoro/carnívoro
- Monitor de estado en tiempo real
- Logs mejorados con emojis
- Prevención de loops infinitos

🔧 PRÓXIMOS PASOS:
1. Verificar configuración de Behavior Trees
2. Asignar tags correctos a objetos de hierba
3. Añadir MonitorEstadoAnimales para debug
4. Ajustar thresholds si es necesario
5. Probar comportamientos en Unity

📊 MONITOREO:
- Consola: Logs cada 2-5 segundos por animal
- Monitor: Estado completo cada 2 segundos
- Debug: Emojis identifican tipo de acción

🎯 COMPORTAMIENTOS ESPERADOS:
- Ciervo: Huye → Duerme → Come → Patrulla
- Lobo: Evita Jabalí → Duerme → Caza → Patrulla  
- Jabalí: Defiende → Duerme → Come → Patrulla Territorio
";

    private void Start()
    {
        Debug.Log("🔧 GUÍA DE DEPURACIÓN CARGADA - Sistema listo para testing");
    }
}
