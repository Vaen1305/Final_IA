using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IACharacterHumanAction : AICharacterActionLand, IIACharacterHumanAction
{

    protected Collider _collider;

    public override void LoadComponent()
    {
        _collider = GetComponent<Collider>();
        
        _VisionSensor = GetComponent<VisionSensor>();

        base.LoadComponent();

    }
    public void InCave(ItemCave _cave)
    {
       
        _VisionSensor.enabled = false;
        health.enabled = false;
        _collider.enabled = false;
        ((HealthHuman)health).IfCanView = false;
        

    }
    public void OutCave(ItemCave _cave)
    {
        _VisionSensor.enabled = true;
        _collider.enabled = true;
        ((HealthHuman)health).IfCanView = true;
    }
}
