using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthCombat : HealthHuman
{
    public override void LoadComponent()
    {
        base.LoadComponent();
    }
    public override void DoDamage(int dmg, Health hit)
    {
        base.DoDamage(dmg, hit);
        if (IsDead)
        {
            hit._bonus.AddBonus(_bonus);
            hit._bonus.Point = 0;
        }
    }
}