using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacterActionLand : AICharacterAction
{
    protected WeaponsManager _weaponsManager;
    public override void LoadComponent()
    {
        base.LoadComponent();
    }

    #region Action
    public virtual void FirePlay()
    {
        _weaponsManager.Fire();
    }
    public virtual void StopFire()
    {
        _weaponsManager.StopFire();
    }
    #endregion
}
