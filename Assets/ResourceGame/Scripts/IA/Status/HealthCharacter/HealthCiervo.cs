using UnityEngine;

public class HealthCiervo : Health
{
    [Header("Necesidades Básicas")]
    public float hunger = 0f; // 0 = sin hambre, 100 = mucha hambre
    public float maxHunger = 100f;
    public float sleepiness = 0f; // 0 = despierto, 100 = muy cansado
    public float maxSleepiness = 100f;
    
    [Header("Velocidades")]
    public float normalSpeed = 3.5f;
    public float fleeSpeed = 7f; // Velocidad cuando huye
    
    [Header("Configuración Tiempo")]
    public float hungerIncreaseRate = 5f; // Hambre aumenta 5 puntos por segundo
    public float sleepinessIncreaseRate = 3f; // Sueño aumenta 3 puntos por segundo
    public float eatingRate = 30f; // Come 30 puntos de hambre por segundo
    public float sleepingRate = 25f; // Duerme 25 puntos de sueño por segundo
    
    [Header("Umbrales")]
    public float hungryThreshold = 30f; // Tiene hambre cuando >= 30
    public float sleepyThreshold = 40f; // Tiene sueño cuando >= 40
    public float fullThreshold = 15f; // Está lleno cuando hambre <= 15
    public float restedThreshold = 10f; // Está descansado cuando sueño <= 10
    
    // Propiedades públicas para el Behavior Tree
    public bool IsHungry { get => hunger >= hungryThreshold; }
    public bool IsSleepy { get => sleepiness >= sleepyThreshold; }
    public bool IsFull { get => hunger <= fullThreshold; }
    public bool IsRested { get => sleepiness <= restedThreshold; }
    public bool IsEating { get; set; } = false;
    public bool IsSleeping { get; set; } = false;
    public bool IsFleeing { get; set; } = false;
    
    private AICharacterVehicleCiervo vehicleCiervo;
    
    private void Start()
    {
        LoadComponent();
        vehicleCiervo = GetComponent<AICharacterVehicleCiervo>();
        
        // Inicializar valores aleatorios
        hunger = Random.Range(0f, 50f);
        sleepiness = Random.Range(0f, 50f);
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
            // Aumentar hambre y sueño con el tiempo
            if (!IsEating)
            {
                hunger = Mathf.Min(maxHunger, hunger + hungerIncreaseRate * Time.deltaTime);
            }
            
            if (!IsSleeping)
            {
                sleepiness = Mathf.Min(maxSleepiness, sleepiness + sleepinessIncreaseRate * Time.deltaTime);
            }
        }
    }
    
    private void UpdateSpeed()
    {
        if (vehicleCiervo != null && vehicleCiervo.Agent != null)
        {
            if (IsFleeing)
            {
                vehicleCiervo.Agent.speed = fleeSpeed;
            }
            else
            {
                vehicleCiervo.Agent.speed = normalSpeed;
            }
        }
    }
    
    private void LogStatus()
    {
        // Log cada 5 segundos para evitar spam
        if (Time.time % 5f < 0.02f)
        {
            string status = IsFleeing ? "HUYENDO" : IsSleeping ? "DURMIENDO" : IsEating ? "COMIENDO" : "NORMAL";
            Debug.Log($"🦌 {gameObject.name}: {status} | Hambre: {hunger:F1}/{maxHunger} | Sueño: {sleepiness:F1}/{maxSleepiness}");
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
    
    public void StartFleeing()
    {
        IsFleeing = true;
        IsEating = false;
        IsSleeping = false;
        Debug.Log($"🏃‍♂️ {gameObject.name}: EMPEZANDO A HUIR!");
    }
    
    public void StopFleeing()
    {
        IsFleeing = false;
        Debug.Log($"🛑 {gameObject.name}: DETENIENDO HUIDA");
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
                aimOffsetGO.transform.localPosition = Vector3.up * 1.2f; // A la altura de los ojos del ciervo
                AimOffset = aimOffsetGO.transform;
                Debug.Log($"✅ {gameObject.name}: AimOffset creado automáticamente");
            }
            else
            {
                AimOffset = aimOffsetChild;
            }
        }
        
        // Configurar tipo de agente
        typeAgent = TypeAgent.Ciervo;
        
        // Inicializar con valores base
        hunger = 20f;
        sleepiness = 15f;
    }
}
