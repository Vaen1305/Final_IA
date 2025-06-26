using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TypeItem { Resource1, Resource2, Resource3, Cave}
public class Item : MonoBehaviour
{
    //BaseItem
    public TypeItem type;
    public Transform AimOffset;

    public virtual void LoadComponent()
    {
        AimOffset = transform;
    }

}
