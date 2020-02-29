using UnityEngine;


[CreateAssetMenu(fileName = "Scroll", menuName = "Inventory/Scroll")]
public class Scroll : InventoryItem
{
    #region Fields

    public ProjectileWeaponController Controller;

    #endregion

    #region Methods

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

    #endregion
}
