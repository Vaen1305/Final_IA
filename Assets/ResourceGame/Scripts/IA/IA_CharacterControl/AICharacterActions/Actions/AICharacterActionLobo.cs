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
            healthLobo.RegenerateStamina(Time.deltaTime);
        }
    }
}
