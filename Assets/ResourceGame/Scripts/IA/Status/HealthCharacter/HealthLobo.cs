using UnityEngine;

public class HealthLobo : HealthHuman
{
    [Header("Necesidades Básicas")]
    public float hunger = 0f; // 0 = sin hambre, 100 = mucha hambre
    public float maxHunger = 100f;
    public float sleepiness = 0f; // 0 = despierto, 100 = muy cansado
    public float maxSleepiness = 100f;
    
    [Header("Velocidades")]
    public float normalSpeed = 4f;
    public float huntSpeed = 6f; // Velocidad cuando caza
    
    [Header("Configuración Tiempo")]
    public float hungerIncreaseRate = 8f; // Hambre aumenta más rápido (carnívoro)
    public float sleepinessIncreaseRate = 4f; // Sueño aumenta 4 puntos por segundo
    public float huntingEnergyRate = 15f; // Gasta energía cazando
    public float sleepingRate = 30f; // Duerme 30 puntos de sueño por segundo
    
    [Header("Umbrales")]
    public float hungryThreshold = 35f; // Tiene hambre cuando >= 35
    public float sleepyThreshold = 45f; // Tiene sueño cuando >= 45
    public float fullThreshold = 10f; // Está lleno cuando hambre <= 10
    public float restedThreshold = 15f; // Está descansado cuando sueño <= 15
    
    // Propiedades públicas para el Behavior Tree
    public bool IsHungry { get => hunger >= hungryThreshold; }
    public bool IsSleepy { get => sleepiness >= sleepyThreshold; }
    public bool IsFull { get => hunger <= fullThreshold; }
    public bool IsRested { get => sleepiness <= restedThreshold; }
    public bool IsHunting { get; set; } = false;
    public bool IsSleeping { get; set; } = false;
    public bool IsAvoidingJabali { get; set; } = false;
    
    private AICharacterVehicleLobo vehicleLobo;
    
    private void Start()
    {
        LoadComponent();
        vehicleLobo = GetComponent<AICharacterVehicleLobo>();
        
        // Inicializar valores aleatorios
        hunger = Random.Range(0f, 60f);
        sleepiness = Random.Range(0f, 40f);
    }
    
    private void Update()
    {
        UpdateNeeds();
        UpdateSpeed();
        LogStatus();
    }
    
    private void UpdateNeeds()
    {
        if (!IsDead)
        {
            // Aumentar hambre más rápido (carnívoro)
            if (!IsHunting)
            {
                hunger = Mathf.Min(maxHunger, hunger + hungerIncreaseRate * Time.deltaTime);
            }
            else
            {
                // Cazar gasta energía extra
                sleepiness = Mathf.Min(maxSleepiness, sleepiness + huntingEnergyRate * Time.deltaTime);
            }
            
            if (!IsSleeping)
            {
                sleepiness = Mathf.Min(maxSleepiness, sleepiness + sleepinessIncreaseRate * Time.deltaTime);
            }
        }
    }
    
    private void UpdateSpeed()
    {
        if (vehicleLobo != null && vehicleLobo.Agent != null)
        {
            if (IsHunting)
            {
                vehicleLobo.Agent.speed = huntSpeed;
            }
            else
            {
                vehicleLobo.Agent.speed = normalSpeed;
            }
        }
    }
    
    private void LogStatus()
    {
        // Log cada 5 segundos para evitar spam
        if (Time.time % 5f < 0.02f)
        {
            string status = IsHunting ? "CAZANDO" : IsSleeping ? "DURMIENDO" : IsAvoidingJabali ? "EVITANDO JABALÍ" : "PATRULLANDO";
            Debug.Log($"🐺 {gameObject.name}: {status} | Hambre: {hunger:F1}/{maxHunger} | Sueño: {sleepiness:F1}/{maxSleepiness}");
        }
    }
    
    public void Hunt()
    {
        IsHunting = true;
        Debug.Log($"🎯 {gameObject.name}: Cazando ciervo...");
    }
    
    public void EatPrey()
    {
        // Cuando atrapa un ciervo, reduce hambre significativamente
        hunger = Mathf.Max(0, hunger - 60f);
        IsHunting = false;
        Debug.Log($"🍖 {gameObject.name}: Comió ciervo! Hambre reducida a: {hunger:F1}");
    }
    
    public void Sleep()
    {
        IsSleeping = true;
        sleepiness = Mathf.Max(0, sleepiness - sleepingRate * Time.deltaTime);
        Debug.Log($"😴 {gameObject.name}: Durmiendo... Sueño: {sleepiness:F1}");
    }
    
    public void StartHunting()
    {
        IsHunting = true;
        IsSleeping = false;
        IsAvoidingJabali = false;
        Debug.Log($"🎯 {gameObject.name}: EMPEZANDO A CAZAR!");
    }
    
    public void StopHunting()
    {
        IsHunting = false;
        Debug.Log($"🛑 {gameObject.name}: DETENIENDO CAZA");
    }
    
    public void StartAvoidingJabali()
    {
        IsAvoidingJabali = true;
        IsHunting = false;
        IsSleeping = false;
        Debug.Log($"⚠️ {gameObject.name}: EVITANDO JABALÍ!");
    }
    
    public void StopAvoidingJabali()
    {
        IsAvoidingJabali = false;
        Debug.Log($"✅ {gameObject.name}: Ya no evita jabalí");
    }
    
    public void StopSleeping()
    {
        IsSleeping = false;
        Debug.Log($"⏰ {gameObject.name}: Despertó");
    }
    
    public override void LoadComponent()
    {
        base.LoadComponent();
        // Inicializar con valores base
        hunger = 25f;
        sleepiness = 20f;
    }
}
