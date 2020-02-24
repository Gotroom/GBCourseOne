using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Scroll", menuName = "Inventory/Scroll")]
public class Scroll : InventoryItem
{

    public ProjectileWeaponController Controller;

    public override bool Wield()
    {
        WieldWeapon.Invoke(Controller);
        return true;
    }

    public override bool Unwield()
    {
        WieldWeapon.Invoke(null);
        return true;
    }
}
