using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemResource : Item
{
    public int weightResource;
    private void Start()
    {
        this.LoadComponent();
    }
    public override void LoadComponent()
    {
        base.LoadComponent();
        switch (type)
        {
            case TypeItem.Resource1:
                weightResource = 10;
                break;
            case TypeItem.Resource2:
                 weightResource = 20;
                break;
            case TypeItem.Resource3:
                weightResource = 30;
                break;
        }
    }
    public void CollectResource(HealthCivil collector)
    {
        if (collector.ResourceBackpack + weightResource > collector.maxResourceBackpack)
        {
            //Crear codigo para que ignore el objeto
            Destroy(this.gameObject);
        }
        else
        {
            collector.ResourceBackpack += weightResource;
            Destroy(this.gameObject);
        }
    }
}
