using UnityEngine;
using System;

public class PowerUpController : BasePickupController
{
    #region Fields

    public static Func<bool, float, bool> ApplyPowerUp;

    [SerializeField] private float _duration = 10.0f;

    #endregion

    #region Methods

    protected override bool PickUp()
    {
        if (ApplyPowerUp.Invoke(true, _duration))
        {
            PlayPickUpSound.Invoke(_pickupSound);
            return true;
        }
        return false;
    }

    #endregion
}
