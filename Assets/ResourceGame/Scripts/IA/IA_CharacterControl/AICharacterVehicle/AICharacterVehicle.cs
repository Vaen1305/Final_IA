using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICharacterVehicle : AICharacterControl
{
    // Propiedad virtual que debe ser implementada por las clases derivadas
    public virtual NavMeshAgent Agent => null;
    
    public override void LoadComponent()
    {
        base.LoadComponent();
        
    }

    public virtual void LookToEnemy()
    {

    }
    public virtual void LookToPosition(Vector3 position)
    {

    }
    public virtual void MoveToEnemy()
    {


    }
    public virtual void MoveToPosition(Vector3 position)
    {

    }
    public virtual void MoveToEvadeEnemy()
    {
    }
    public virtual void Wander()
    {

    }
    public virtual void MoveToAllied()
    { 
    }
    public virtual void MoveToResource() { }
}
