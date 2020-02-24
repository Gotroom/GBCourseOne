using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class InventoryController : MonoBehaviour
{
    #region Fields

    public const int MAX_SLOTS = 8;

    public static Action<ConsumableWeapon.Types, int> Consumed;
    public static Action<Dictionary<InventoryItem, int>> UpdateUI;

    public static int MaxSpace = 80;
    public Dictionary<InventoryItem, int> Items;

    private int _spaceLeft = MaxSpace;

    #endregion


    #region UnityMethods

    void Start()
    {
        Items = new Dictionary<InventoryItem, int>(MAX_SLOTS);
        foreach (var item in PlayerDataController.instance.ItemsList)
        {
            Items.Add(item.Key, item.Value);
        }
        BasePickupController.PickingUp = OnPickUp;
        FillLoaded();
    }

    #endregion


    #region Methods

    private bool OnPickUp(InventoryItem item, int count)
    {
        print(item.Count);
        int countNew = count;

        if (Items.ContainsKey(item))
        {

            if (count < _spaceLeft)
            {
                Items[item] += count;
                _spaceLeft -= count;
            }
            else
            {
                Items[item] += _spaceLeft;
                _spaceLeft -= _spaceLeft;
            }

            UpdateUI.Invoke(Items);
            return true;
        }
        else
        {
            if (count < _spaceLeft)
            {
                Items.Add(item, count);
                _spaceLeft -= count;
            }
            else
            {
                Items.Add(item, _spaceLeft);
                _spaceLeft -= _spaceLeft;
            }
            UpdateUI.Invoke(Items);
            return true;
        }
    }

    public void FillLoaded()
    {
        UpdateUI?.Invoke(Items);
    }

    #endregion
}
