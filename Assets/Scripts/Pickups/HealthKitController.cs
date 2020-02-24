using UnityEngine;
using System;


public class HealthKitController : BasePickupController
{
    #region Fields

    public static Predicate<int> Heal;

    [SerializeField] protected int _healingPower = 1; //protected to create larger healing kits

    #endregion

    #region Methods

    protected override bool PickUp()
    {
        return PickingUp.Invoke(Item, Count);
    }

    #endregion
}
