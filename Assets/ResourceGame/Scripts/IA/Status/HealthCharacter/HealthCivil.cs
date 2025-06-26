using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCivil : HealthHuman
{
    [Header("Resources Inventory")]
    public int ResourceBackpack = 0;
    public int maxResourceBackpack = 50;
    private void Start()
    {
        this.LoadComponent();
        ResourceBackpack = 0;
    }
    public override void LoadComponent()
    {
        base.LoadComponent();
    }
    
    public override void DoDamage(int dmg, Health hit)
    {
        
        base.DoDamage(dmg, hit);
        Debug.Log($"Civil Recibi danno de : {hit.gameObject.name}" + " damage: "+dmg);
    }
}
