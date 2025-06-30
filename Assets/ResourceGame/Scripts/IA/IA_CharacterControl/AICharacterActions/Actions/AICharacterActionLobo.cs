using UnityEngine;

public class AICharacterActionLobo : AICharacterActionLand
{
    public int damage = 40;
    private void Start()
    {
        this.LoadComponent();
    }

    public void Attack()
    {
        var vision = _VisionSensor as VisionSensorLobo;
        if (vision != null && vision.EnemyView != null && vision.AttackVision.InSight)
        {
            if (Framerate > arrayRate[index])
            {
                index = (index + 1) % arrayRate.Length;
                vision.EnemyView.DoDamage(damage, health);
                Framerate = 0;
            }
            Framerate += Time.deltaTime;
        }
    }

    public void Sleep()
    {
        var healthLobo = health as HealthLobo;
        if (healthLobo != null)
        {
            // Ya no usar stamina - el nuevo sistema maneja esto automáticamente
            Debug.Log($"😴 Lobo durmiendo - Hambre: {healthLobo.hunger:F1} | Sueño: {healthLobo.sleepiness:F1}");
        }
    }
}
