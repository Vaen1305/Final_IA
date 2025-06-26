using UnityEngine;

public class AICharacterActionJabali : AICharacterActionLand
{
    public int damage = 25;
    private void Start()
    {
        this.LoadComponent();
    }

    public void Attack()
    {
        if (_VisionSensor != null && _VisionSensor.EnemyView != null)
        {
            if (Framerate > arrayRate[index])
            {
                index = (index + 1) % arrayRate.Length;
                _VisionSensor.EnemyView.DoDamage(damage, health);
                Framerate = 0;
            }
            Framerate += Time.deltaTime;
        }
    }
}
