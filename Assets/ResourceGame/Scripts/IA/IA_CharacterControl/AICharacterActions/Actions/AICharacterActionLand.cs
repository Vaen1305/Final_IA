using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacterActionLand : AICharacterAction
{
    public override void LoadComponent()
    {
        base.LoadComponent();
    }

    #region Action
    // Métodos básicos para acciones de animales terrestres
    public virtual void Rest()
    {
        // Implementación básica de descanso
    }
    
    public virtual void Eat()
    {
        // Implementación básica de comer
    }
    #endregion
}
