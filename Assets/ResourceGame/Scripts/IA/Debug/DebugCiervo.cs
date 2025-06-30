using UnityEngine;

/// <summary>
/// Debug específico para el Ciervo - Monitorea todos sus comportamientos
/// </summary>
public class DebugCiervo : MonoBehaviour
{
    [Header("Referencias")]
    public HealthCiervo healthCiervo;
    public AICharacterVehicleCiervo vehicleCiervo;
    
    [Header("Configuración")]
    public bool debugActivo = true;
    public float intervaloReporte = 1f; // Cada segundo
    
    private float ultimoReporte = 0f;
    
    private void Start()
    {
        if (healthCiervo == null)
            healthCiervo = GetComponent<HealthCiervo>();
        if (vehicleCiervo == null)
            vehicleCiervo = GetComponent<AICharacterVehicleCiervo>();
            
        Debug.Log($"🦌 DEBUG CIERVO INICIADO: {gameObject.name}");
    }
    
    private void Update()
    {
        if (!debugActivo || healthCiervo == null)
            return;
            
        if (Time.time - ultimoReporte >= intervaloReporte)
        {
            ReportarEstado();
            ultimoReporte = Time.time;
        }
        
        // Debug crítico en tiempo real
        VerificarEstadoCritico();
    }
    
    private void ReportarEstado()
    {
        string estado = "🦌 CIERVO ESTADO:\n";
        estado += $"  ❤️ Salud: Vivo={!healthCiervo.IsDead}\n";
        estado += $"  🍽️ Hambre: {healthCiervo.hunger:F1}/{healthCiervo.maxHunger} (Hambriento: {healthCiervo.IsHungry}, Saciado: {healthCiervo.IsFull})\n";
        estado += $"  😴 Sueño: {healthCiervo.sleepiness:F1}/{healthCiervo.maxSleepiness} (Cansado: {healthCiervo.IsSleepy}, Descansado: {healthCiervo.IsRested})\n";
        estado += $"  🏃 Estados: Comiendo={healthCiervo.IsEating}, Durmiendo={healthCiervo.IsSleeping}, Huyendo={healthCiervo.IsFleeing}\n";
        
        if (vehicleCiervo != null && vehicleCiervo.Agent != null)
        {
            estado += $"  🎯 Agent: Activo={vehicleCiervo.Agent.enabled}, EnNavMesh={vehicleCiervo.Agent.isOnNavMesh}, Parado={vehicleCiervo.Agent.isStopped}\n";
            estado += $"  📍 Posición: {transform.position}\n";
            if (vehicleCiervo.Agent.hasPath)
            {
                estado += $"  🛤️ Destino: {vehicleCiervo.Agent.destination} (Dist: {vehicleCiervo.Agent.remainingDistance:F1}m)\n";
            }
        }
        
        Debug.Log(estado);
    }
    
    private void VerificarEstadoCritico()
    {
        // Verificar si está atascado
        if (vehicleCiervo != null && vehicleCiervo.Agent != null)
        {
            if (vehicleCiervo.Agent.velocity.magnitude < 0.1f && vehicleCiervo.Agent.hasPath && !vehicleCiervo.Agent.isStopped)
            {
                Debug.LogWarning($"⚠️ {gameObject.name}: POSIBLE ATASCO - Velocidad muy baja con destino activo");
            }
            
            if (!vehicleCiervo.Agent.isOnNavMesh)
            {
                Debug.LogError($"❌ {gameObject.name}: FUERA DEL NAVMESH!");
            }
        }
        
        // Verificar valores extremos
        if (healthCiervo.hunger > healthCiervo.maxHunger * 0.9f)
        {
            Debug.LogWarning($"⚠️ {gameObject.name}: HAMBRE CRÍTICA ({healthCiervo.hunger:F1})");
        }
        
        if (healthCiervo.sleepiness > healthCiervo.maxSleepiness * 0.9f)
        {
            Debug.LogWarning($"⚠️ {gameObject.name}: SUEÑO CRÍTICO ({healthCiervo.sleepiness:F1})");
        }
    }
    
    // Método para llamar desde otros scripts cuando hay problemas
    public void ReportarProblema(string descripcion)
    {
        Debug.LogError($"❌ PROBLEMA EN CIERVO {gameObject.name}: {descripcion}");
        ReportarEstado();
    }
}
