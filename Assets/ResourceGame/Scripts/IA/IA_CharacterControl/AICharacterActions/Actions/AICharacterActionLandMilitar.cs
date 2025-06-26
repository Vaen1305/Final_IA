using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacterActionLandMilitar : AICharacterActionCombat
{
    public int meleeDamage;
    public int ShootDamage;
    private void Start()
    {
        this.LoadComponent();
    }
    public override void LoadComponent()
    {
        base.LoadComponent();
    }
    public override void Attack()
    {
        if (_VisionSensor != null)
        {
            VisionSensorSoldier VSM = ((VisionSensorSoldier)_VisionSensor);
            if (VSM != null && VSM.EnemyView != null && VSM.AttackVision.InSight)
            {
                if (Framerate > arrayRate[index])
                {
                    index++;
                    index = index % arrayRate.Length;
                    VSM.EnemyView.DoDamage(meleeDamage, health);

                    Framerate = 0;
                }
                Framerate += Time.deltaTime;
            }
            else
                Framerate = 0;
        }
    }
    public override void Shoot()
    {
        if (_VisionSensor != null)
        {
            VisionSensorSoldier VSM = ((VisionSensorSoldier)_VisionSensor);
            if (VSM != null && VSM.EnemyView != null && VSM.FireVision.InSight)
            {
                if (Framerate > arrayRate[index])
                {
                    index++;
                    index = index % arrayRate.Length;
                    VSM.EnemyView.DoDamage(ShootDamage, health);

                    Framerate = 0;
                }
                Framerate += Time.deltaTime;
            }
            else
                Framerate = 0;
        }
    }
}
