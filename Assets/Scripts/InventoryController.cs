using UnityEngine;
using System;
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
        if (PlayerDataController.instance != null)
        {
            foreach (var item in PlayerDataController.instance.ItemsList)
            {
                Items.Add(item.Key, item.Value);
                _spaceLeft -= item.Value;
            }
        }
        BasePickupController.PickingUp = OnPickUp;
        InventoryItem.Consumed = OnConsumed;
        InventoryItem.Used = OnUsed;
    }

    #endregion


    #region Methods

    private bool OnPickUp(InventoryItem item, int count)
    {
        if (Items.ContainsKey(item))
        {
            AddExisting(item, count);
        }
        else
        {
            AddNew(item, count);
        }
        return true;
    }

    private void AddExisting(InventoryItem item, int count)
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
    }

    private void AddNew(InventoryItem item, int count)
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
    }

    public void FillLoaded()
    {
        UpdateUI?.Invoke(Items);
    }

    private void OnConsumed(InventoryItem item)
    {
        if (Items.ContainsKey(item))
        {
            Items[item]--;
            if (Items[item] == 0)
                Items.Remove(item);
            UpdateUI?.Invoke(Items);
        }
    }

    private void OnUsed(InventoryItem item)
    {
        if (Items.ContainsKey(item))
        {
            Items[item]--;
            if (Items[item] == 0)
                Items.Remove(item);
            UpdateUI?.Invoke(Items);
        }
    }

    #endregion
}
