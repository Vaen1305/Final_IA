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
            healthCiervo.RegenerateStamina(Time.deltaTime);
        }
    }
    
}

