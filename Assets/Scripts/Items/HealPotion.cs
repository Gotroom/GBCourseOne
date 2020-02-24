using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Healing Potion", menuName = "Inventory/Healing Potion")]
public class HealPotion : InventoryItem
{
    public int HealingPower = 1;

    public override bool Consume()
    {
        return HealthKitController.Heal(HealingPower);
    }

}
