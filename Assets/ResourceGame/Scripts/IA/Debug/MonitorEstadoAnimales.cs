using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Monitor de estado de los animales - Muestra información en pantalla
/// </summary>
public class MonitorEstadoAnimales : MonoBehaviour
{
    [Header("Referencias")]
    public HealthCiervo healthCiervo;
    public HealthLobo healthLobo;
    public HealthJabali healthJabali;
    
    [Header("UI (Opcional)")]
    public TextMeshProUGUI textDisplay;
    
    [Header("Configuración")]
    public bool mostrarEnConsola = true;
    public bool mostrarEnUI = false;
    public float intervaloActualizacion = 2f;
    
    private float ultimaActualizacion = 0f;
    
    private void Start()
    {
        // Encontrar automáticamente los animales si no están asignados
        if (healthCiervo == null)
            healthCiervo = FindObjectOfType<HealthCiervo>();
        if (healthLobo == null)
            healthLobo = FindObjectOfType<HealthLobo>();
        if (healthJabali == null)
            healthJabali = FindObjectOfType<HealthJabali>();
    }
    
    private void Update()
    {
        if (Time.time - ultimaActualizacion >= intervaloActualizacion)
        {
            MostrarEstadoAnimales();
            ultimaActualizacion = Time.time;
        }
    }
    
    private void MostrarEstadoAnimales()
    {
        string reporte = GenerarReporteEstado();
        
        if (mostrarEnConsola)
        {
            Debug.Log(reporte);
        }
        
        if (mostrarEnUI && textDisplay != null)
        {
            textDisplay.text = reporte;
        }
    }
    
    private string GenerarReporteEstado()
    {
        string reporte = "=== ESTADO DE LOS ANIMALES ===\n";
        
        // Estado del Ciervo
        if (healthCiervo != null)
        {
            string estadoCiervo = GetEstadoActual(healthCiervo);
            reporte += $"🦌 CIERVO: {estadoCiervo}\n";
            reporte += $"   Hambre: {healthCiervo.hunger:F1}/{healthCiervo.maxHunger} (Hambriento: {healthCiervo.IsHungry}, Saciado: {healthCiervo.IsFull})\n";
            reporte += $"   Sueño: {healthCiervo.sleepiness:F1}/{healthCiervo.maxSleepiness} (Cansado: {healthCiervo.IsSleepy}, Descansado: {healthCiervo.IsRested})\n";
            reporte += $"   Estados: Comiendo={healthCiervo.IsEating}, Durmiendo={healthCiervo.IsSleeping}, Huyendo={healthCiervo.IsFleeing}\n\n";
        }
        
        // Estado del Lobo
        if (healthLobo != null)
        {
            string estadoLobo = GetEstadoActual(healthLobo);
            reporte += $"🐺 LOBO: {estadoLobo}\n";
            reporte += $"   Hambre: {healthLobo.hunger:F1}/{healthLobo.maxHunger} (Hambriento: {healthLobo.IsHungry}, Saciado: {healthLobo.IsFull})\n";
            reporte += $"   Sueño: {healthLobo.sleepiness:F1}/{healthLobo.maxSleepiness} (Cansado: {healthLobo.IsSleepy}, Descansado: {healthLobo.IsRested})\n";
            reporte += $"   Estados: Cazando={healthLobo.IsHunting}, Durmiendo={healthLobo.IsSleeping}, Evitando={healthLobo.IsAvoidingJabali}\n\n";
        }
        
        // Estado del Jabalí
        if (healthJabali != null)
        {
            string estadoJabali = GetEstadoActual(healthJabali);
            float distanciaTerritorio = Vector3.Distance(healthJabali.transform.position, healthJabali.territoryCenter);
            reporte += $"🐗 JABALÍ: {estadoJabali}\n";
            reporte += $"   Hambre: {healthJabali.hunger:F1}/{healthJabali.maxHunger} (Hambriento: {healthJabali.IsHungry}, Saciado: {healthJabali.IsFull})\n";
            reporte += $"   Sueño: {healthJabali.sleepiness:F1}/{healthJabali.maxSleepiness} (Cansado: {healthJabali.IsSleepy}, Descansado: {healthJabali.IsRested})\n";
            reporte += $"   Estados: Comiendo={healthJabali.IsEating}, Durmiendo={healthJabali.IsSleeping}, Defendiendo={healthJabali.IsDefendingTerritory}\n";
            reporte += $"   Territorio: Distancia={distanciaTerritorio:F1}m, EnTerritorio={healthJabali.IsInTerritory}\n";
        }
        
        return reporte;
    }
    
    private string GetEstadoActual(HealthHuman health)
    {
        if (health.IsDead)
            return "MUERTO";
        
        if (health is HealthCiervo ciervo)
        {
            if (ciervo.IsFleeing) return "HUYENDO";
            if (ciervo.IsSleeping) return "DURMIENDO";
            if (ciervo.IsEating) return "COMIENDO";
            if (ciervo.IsSleepy) return "NECESITA DORMIR";
            if (ciervo.IsHungry) return "NECESITA COMER";
            return "PATRULLANDO";
        }
        
        if (health is HealthLobo lobo)
        {
            if (lobo.IsSleeping) return "DURMIENDO";
            if (lobo.IsHunting) return "CAZANDO";
            if (lobo.IsAvoidingJabali) return "EVITANDO JABALÍ";
            if (lobo.IsSleepy) return "NECESITA DORMIR";
            if (lobo.IsHungry) return "NECESITA CAZAR";
            return "PATRULLANDO";
        }
        
        if (health is HealthJabali jabali)
        {
            if (jabali.IsSleeping) return "DURMIENDO";
            if (jabali.IsEating) return "COMIENDO";
            if (jabali.IsDefendingTerritory) return "DEFENDIENDO";
            if (jabali.IsSleepy) return "NECESITA DORMIR";
            if (jabali.IsHungry) return "NECESITA COMER";
            if (!jabali.IsInTerritory) return "REGRESANDO A TERRITORIO";
            return "PATRULLANDO TERRITORIO";
        }
        
        return "DESCONOCIDO";
    }
}
