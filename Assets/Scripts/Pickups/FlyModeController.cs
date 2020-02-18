using UnityEngine;
using System;

public class FlyModeController : BasePickupController
{
    #region Fields

    public static Func<bool, float, bool> ApplyFlyingMode;

    [SerializeField] private float _duration = 10.0f;

    #endregion

    #region Methods

    protected override bool PickUp()
    {
        if (ApplyFlyingMode.Invoke(true, _duration))
        {
            PlayPickUpSound.Invoke(_pickupSound);
            return true;
        }
        return false;
    }

    #endregion
}
