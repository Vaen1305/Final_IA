using UnityEngine;

public class HealthLobo : HealthHuman
{
    [Header("Stamina")]
    public float stamina = 100f;
    public float maxStamina = 100f;
    public float staminaDepletionRate = 10f; // per second while chasing
    public float staminaRegenRate = 5f; // per second while sleeping

    public bool IsTired { get => stamina <= 20; }

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
