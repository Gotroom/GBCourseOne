using UnityEngine;
using System.Collections;

public class UIInventory : MonoBehaviour
{
    private UIInventorySlot[] _slots;
    // Use this for initialization
    void Start()
    {
        InventoryController.PickedUp = OnPickedUp;
        _slots = GetComponentsInChildren<UIInventorySlot>();
        print(_slots.Length);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private bool OnPickedUp(InventoryItem item)
    {
        //gameObject.GetComponents<>
        int countNew = item.Count;
        for (int i = 0; i < _slots.Length; i++)
        {
            if (_slots[i]._item.Name == item.Name)
            {
                int countToPlaceInExistingSlot = InventoryController.MaxItemsPerSlot - _slots[i].CountInSlot;
                if (countToPlaceInExistingSlot == 0)
                    continue;
                if (countNew - countToPlaceInExistingSlot > 0)
                {
                    _slots[i].IncreaseExistingItemCount(countToPlaceInExistingSlot);
                    countNew -= countToPlaceInExistingSlot;
                }
                else
                {
                    _slots[i].IncreaseExistingItemCount(countNew);
                    countNew -= countNew;
                }
                if (countNew > 0)
                    break;
                return true;
            }
        }
        for (int i = 0; i < _slots.Length; i++)
        {
            if (_slots[i]._item.Image == null)
            {
                _slots[i].AddItem(item, countNew);
                return true;
            }
        }
        return false;
    }
}
