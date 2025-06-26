using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthResource : Health
{
    private void Start()
    {
        this.LoadComponent();
    }
    public override void LoadComponent()
    {
        base.LoadComponent();
    }

}
