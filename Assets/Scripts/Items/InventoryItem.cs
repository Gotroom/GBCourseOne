using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Item", menuName ="Inventory/Item")]
public class InventoryItem : ScriptableObject
{
    public enum ItemType
    {
        Usable,
        Consumable,
        Key
    }

    public static Action<BaseWeapon> WieldWeapon;
    public static Action<InventoryItem> Consumed;
    public static Action<InventoryItem> Used;

    public Sprite Image;
    public string Name = "New Item";
    public int Count = 1;
    public ItemType Type;

    public virtual bool Consume()
    {
        Consumed?.Invoke(this);
        return true;
    }

    public virtual bool Use()
    {
        Used?.Invoke(this);
        return true;
    }

    public virtual bool Wield()
    {
        return true;
    }

    public virtual bool Unwield()
    {
        return true;
    }
}
