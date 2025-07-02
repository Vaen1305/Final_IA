using UnityEngine;

public class HealthJabali : Health
{
    [Header("Territorio")]
    public Vector3 territoryCenter;
    public float territoryRadius = 15f;
    
    [Header("Necesidades Básicas")]
    public float hunger = 0f; // 0 = sin hambre, 100 = mucha hambre
    public float maxHunger = 100f;
    public float sleepiness = 0f; // 0 = despierto, 100 = muy cansado
    public float maxSleepiness = 100f;
    
    [Header("Velocidades")]
    public float normalSpeed = 3f;
    public float aggressiveSpeed = 5f; // Velocidad cuando ataca
    
    [Header("Configuración Tiempo")]
    public float hungerIncreaseRate = 6f; // Hambre aumenta 6 puntos por segundo
    public float sleepinessIncreaseRate = 3.5f; // Sueño aumenta 3.5 puntos por segundo
    public float eatingRate = 25f; // Come 25 puntos de hambre por segundo
    public float sleepingRate = 20f; // Duerme 20 puntos de sueño por segundo
    public float fightingEnergyRate = 10f; // Gasta energía luchando
    
    [Header("Umbrales")]
    public float hungryThreshold = 25f; // Tiene hambre cuando >= 25
    public float sleepyThreshold = 50f; // Tiene sueño cuando >= 50
    public float fullThreshold = 10f; // Está lleno cuando hambre <= 10
    public float restedThreshold = 20f; // Está descansado cuando sueño <= 20
    
    // Propiedades públicas para el Behavior Tree
    public bool IsHungry { get => hunger >= hungryThreshold; }
    public bool IsSleepy { get => sleepiness >= sleepyThreshold; }
    public bool IsFull { get => hunger <= fullThreshold; }
    public bool IsRested { get => sleepiness <= restedThreshold; }
    public bool IsEating { get; set; } = false;
    public bool IsSleeping { get; set; } = false;
    public bool IsDefendingTerritory { get; set; } = false;
    public bool IsInTerritory { get => Vector3.Distance(transform.position, territoryCenter) <= territoryRadius; }
    
    private AICharacterVehicleJabali vehicleJabali;
    
    private void Start()
    {
        LoadComponent();
        vehicleJabali = GetComponent<AICharacterVehicleJabali>();
        
        // Inicializar valores aleatorios
        hunger = Random.Range(0f, 40f);
        sleepiness = Random.Range(0f, 30f);
    }
    
    private void Update()
    {
        UpdateNeeds();
        UpdateSpeed();
        LogStatus();
        CheckTerritoryBounds();
    }
    
    private void UpdateNeeds()
    {
        if (!IsDead)
        {
            // Aumentar hambre con el tiempo
            if (!IsEating)
            {
                hunger = Mathf.Min(maxHunger, hunger + hungerIncreaseRate * Time.deltaTime);
            }
            
            // Luchar gasta energía extra
            if (IsDefendingTerritory)
            {
                sleepiness = Mathf.Min(maxSleepiness, sleepiness + fightingEnergyRate * Time.deltaTime);
            }
            
            if (!IsSleeping)
            {
                sleepiness = Mathf.Min(maxSleepiness, sleepiness + sleepinessIncreaseRate * Time.deltaTime);
            }
        }
    }
    
    private void UpdateSpeed()
    {
        if (vehicleJabali != null && vehicleJabali.Agent != null)
        {
            if (IsDefendingTerritory)
            {
                vehicleJabali.Agent.speed = aggressiveSpeed;
            }
            else
            {
                vehicleJabali.Agent.speed = normalSpeed;
            }
        }
    }
    
    private void CheckTerritoryBounds()
    {
        // Si se sale del territorio, volver automáticamente
        if (!IsInTerritory && vehicleJabali != null && vehicleJabali.Agent != null)
        {
            Vector3 directionToCenter = (territoryCenter - transform.position).normalized;
            Vector3 returnPoint = territoryCenter + directionToCenter * (territoryRadius * 0.8f);
            vehicleJabali.Agent.SetDestination(returnPoint);
            
            if (Time.time % 3f < 0.02f)
            {
                Debug.Log($"🏠 {gameObject.name}: Regresando al territorio desde distancia: {Vector3.Distance(transform.position, territoryCenter):F1}m");
            }
        }
    }
    
    private void LogStatus()
    {
        // Log cada 6 segundos para evitar spam
        if (Time.time % 6f < 0.02f)
        {
            string status = IsDefendingTerritory ? "DEFENDIENDO" : IsSleeping ? "DURMIENDO" : IsEating ? "COMIENDO" : "PATRULLANDO";
            float distanceFromCenter = Vector3.Distance(transform.position, territoryCenter);
            Debug.Log($"🐗 {gameObject.name}: {status} | Hambre: {hunger:F1}/{maxHunger} | Sueño: {sleepiness:F1}/{maxSleepiness} | Territorio: {distanceFromCenter:F1}m/{territoryRadius}m");
        }
    }
    
    public void Eat()
    {
        IsEating = true;
        hunger = Mathf.Max(0, hunger - eatingRate * Time.deltaTime);
        Debug.Log($"🌿 {gameObject.name}: Comiendo hierba... Hambre: {hunger:F1}");
    }
    
    public void Sleep()
    {
        IsSleeping = true;
        sleepiness = Mathf.Max(0, sleepiness - sleepingRate * Time.deltaTime);
        Debug.Log($"😴 {gameObject.name}: Durmiendo... Sueño: {sleepiness:F1}");
    }
    
    public void StartDefending()
    {
        IsDefendingTerritory = true;
        IsEating = false;
        IsSleeping = false;
        Debug.Log($"⚔️ {gameObject.name}: DEFENDIENDO TERRITORIO!");
    }
    
    public void StopDefending()
    {
        IsDefendingTerritory = false;
        Debug.Log($"🛑 {gameObject.name}: DETENIENDO DEFENSA");
    }
    
    public void StopEating()
    {
        IsEating = false;
        Debug.Log($"✅ {gameObject.name}: Terminó de comer");
    }
    
    public void StopSleeping()
    {
        IsSleeping = false;
        Debug.Log($"⏰ {gameObject.name}: Despertó");
    }
    
    public override void LoadComponent()
    {
        base.LoadComponent();
        
        // Configurar AimOffset si no está asignado
        if (AimOffset == null)
        {
            // Buscar un transform hijo llamado "AimOffset" o crear uno
            Transform aimOffsetChild = transform.Find("AimOffset");
            if (aimOffsetChild == null)
            {
                // Crear AimOffset automáticamente
                GameObject aimOffsetGO = new GameObject("AimOffset");
                aimOffsetGO.transform.SetParent(transform);
                aimOffsetGO.transform.localPosition = Vector3.up * 1.0f; // A la altura de los ojos del jabalí
                AimOffset = aimOffsetGO.transform;
                Debug.Log($"✅ {gameObject.name}: AimOffset creado automáticamente");
            }
            else
            {
                AimOffset = aimOffsetChild;
            }
        }
        
        // Configurar tipo de agente
        typeAgent = TypeAgent.Jabali;
        
        // El centro del territorio es su posición inicial
        territoryCenter = transform.position;
        
        // Inicializar con valores base
        hunger = 15f;
        sleepiness = 25f;
    }
    
    // Método para visualizar el territorio en el editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(territoryCenter, territoryRadius);
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(territoryCenter, 0.5f);
    }
}
