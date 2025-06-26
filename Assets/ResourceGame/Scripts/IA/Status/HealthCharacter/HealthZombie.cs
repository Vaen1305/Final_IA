using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthZombie : HealthCombat
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
        Debug.Log($"Zombie Recibi danno de : {hit.gameObject.name}" + " damage: " + dmg);
    }
}
