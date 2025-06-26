using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSoldier : HealthCombat
{
    private void Start()
    {
        this.LoadComponent();
    }
    public override void LoadComponent()
    {
        base.LoadComponent();
    }
    public override void DoDamage(int dmg, Health hit)
    {
        base.DoDamage(dmg, hit);
        Debug.Log($"Soldier Recibi danno de : {hit.gameObject.name}" + " damage: " + dmg);
    }
}
