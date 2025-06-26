using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHuman : Health
{
    [Header("IfCanView")]
    public bool IfCanView = true;
     
    public override void LoadComponent()
    {
        base.LoadComponent();
    }


    public void Hide()
    {
        IfCanView = false;
    }
    public void Show()
    {
        IfCanView = true;
    }
}
