using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AICharacterActionLandZombie : AICharacterActionCombat
{
    public int damage;
    
    private void Start()
    {
        this.LoadComponent();
    }
    public void UpgradeDamage()
    {
        if (health.IsDead) return;
        float b = health._bonus.Percent();
        if (b > 0.5f)
            damage *= 2;
        
    }
    public override void LoadComponent()
    {
        base.LoadComponent();
    }
    public override void Attack()
    {
        if (_VisionSensor != null)
        {
            VisionSensorZombie VSZ = ((VisionSensorZombie)_VisionSensor);
            if (VSZ != null &&VSZ.EnemyView!=null && VSZ.AttackVision.InSight)
            {
                if (Framerate > arrayRate[index])
                {
                    index++;
                    index = index % arrayRate.Length;
                    VSZ.EnemyView.DoDamage(damage, health);
                    
                    Framerate = 0;
                }
                Framerate += Time.deltaTime;
            }
            else
                Framerate = 0;
        }
    }
}
