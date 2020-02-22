using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class InventoryController : MonoBehaviour
{
    #region Fields

    public const int MAX_SLOTS = 8;

    public static Action<ConsumableWeapon.Types, int> Consumed;
    public static Func<InventoryItem, bool> PickedUp;

    public static int MaxItemsPerSlot = 3;

    #endregion


    #region UnityMethods

    void Start()
    {
        PlayerController.Consume = OnConsume;
        BasePickupController.PickingUp = OnPickUp;
    }

    #endregion


    #region Methods

    private bool OnConsume(ConsumableWeapon.Types type)
    {
        bool hasConsumable = false;
        switch (type)
        {
            case ConsumableWeapon.Types.Mine:
            {
                //if (_minesCount > 0)
                //{
                //    hasConsumable = true;
                //    _minesCount--;
                //    Consumed?.Invoke(ConsumableWeapon.Types.Mine, _minesCount);
                //}
                break;
            }
            default:
                break;
        }
        return hasConsumable;
    }

    private bool OnPickUp(InventoryItem item)
    {
        return PickedUp.Invoke(item);
    }

    #endregion
}
