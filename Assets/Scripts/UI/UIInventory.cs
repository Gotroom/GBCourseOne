using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class UIInventory : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private UIInventorySlot[] _slots;

    private KeyCode[] _keyCodes =
    {
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
        KeyCode.Alpha5,
        KeyCode.Alpha6,
        KeyCode.Alpha7,
        KeyCode.Alpha8
    };
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
        for(int i = 0; i < _keyCodes.Length; i++)
        {
            if (Input.GetKeyDown(_keyCodes[i]))
            {
                _slots[i].OnClick();
            }
        }
    }

    private bool OnPickedUp(InventoryItem item)
    {
        //gameObject.GetComponents<>
        int countNew = item.Count;
        for (int i = 0; i < _slots.Length; i++)
        {
            if (_slots[i]._item != null && _slots[i]._item.Name == item.Name)
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
            if (_slots[i]._item == null)
            {
                _slots[i].AddItem(item, countNew);
                return true;
            }
        }
        return false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PlayerController.PlayerInstance.PreventFiring = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PlayerController.PlayerInstance.PreventFiring = false;
    }
}