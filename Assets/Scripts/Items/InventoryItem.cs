using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Item", menuName ="Inventory/Item")]
public class InventoryItem : ScriptableObject
{
    public enum ItemType
    {
        Usable,
        Consumable
    }

    public static Action<BaseWeapon> WieldWeapon;

    public Sprite Image;
    public string Name = "New Item";
    public int Count = 1;
    public ItemType Type;

    public virtual bool Consume()
    {
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
