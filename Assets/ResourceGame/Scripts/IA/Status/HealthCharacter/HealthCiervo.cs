using UnityEngine;

public class HealthCiervo : HealthHuman
{
    [Header("Stamina")]
    public float stamina = 100f;
    public float maxStamina = 100f;
    public float staminaDepletionRate = 5f; // per second while running
    public float staminaRegenRate = 10f; // per second while resting

    public bool IsTired { get => stamina <= 20; } // Threshold for being tired

    public override void LoadComponent()
    {
        base.LoadComponent();
        stamina = maxStamina;
    }

    public void DepleteStamina(float deltaTime)
    {
        stamina = Mathf.Max(0, stamina - staminaDepletionRate * deltaTime);
    }

    public void RegenerateStamina(float deltaTime)
    {
        stamina = Mathf.Min(maxStamina, stamina + staminaRegenRate * deltaTime);
    }
}
