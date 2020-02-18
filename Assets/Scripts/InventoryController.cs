using UnityEngine;
using System;


public class InventoryController : MonoBehaviour
{
    #region Fields

    public static Action<ConsumableWeapon.Types, int> Consumed;

    [SerializeField] private int _maxMines = 3;
    [SerializeField] private int _minesCount = 2;

    #endregion


    #region UnityMethods

    void Start()
    {
        PlayerController.Consume = OnConsume;
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
                if (_minesCount > 0)
                {
                    hasConsumable = true;
                    _minesCount--;
                    Consumed?.Invoke(ConsumableWeapon.Types.Mine, _minesCount);
                }
                break;
            }
            default:
                break;
        }
        return hasConsumable;
    }

    #endregion
}
