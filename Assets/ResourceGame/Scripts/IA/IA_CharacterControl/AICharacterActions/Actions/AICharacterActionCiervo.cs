using UnityEngine;

public class AICharacterActionCiervo : AICharacterActionLand
{
    private void Start()
    {
        this.LoadComponent();
    }

    public void Rest()
    {
        var healthCiervo = health as HealthCiervo;
        if (healthCiervo != null)
        {
            // Ya no usar stamina - el nuevo sistema maneja esto automáticamente
            Debug.Log($"😴 Ciervo descansando - Hambre: {healthCiervo.hunger:F1} | Sueño: {healthCiervo.sleepiness:F1}");
        }
    }
    
}

