using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;

public class UIInventory : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static Action ClearSelection;

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

    private void Start()
    {
        InventoryController.UpdateUI = OnUpdate;
        _slots = GetComponentsInChildren<UIInventorySlot>();
        print(_slots.Length);
    }


    private void Update()
    {
        for(int i = 0; i < _keyCodes.Length; i++)
        {
            if (Input.GetKeyDown(_keyCodes[i]))
            {
                if (_slots[i].Item != null && _slots[i].Item.Type == InventoryItem.ItemType.Usable)
                {
                    ClearSelection.Invoke();
                }
                _slots[i].OnClick();
            }
        }
    }

    private void OnUpdate(Dictionary<InventoryItem, int> items)
    {
        int itterator = 0;
        foreach (var item in items)
        {
            _slots[itterator].AddItem(item.Key, item.Value);
            itterator++;
        }
        //int countNew = item.Count;
        //for (int i = 0; i < _slots.Length; i++)
        //{
        //    if (_slots[i].Item != null && _slots[i].Item.Name == item.Name)
        //    {
        //        int countToPlaceInExistingSlot = InventoryController.MaxItemsPerSlot - _slots[i].CountInSlot;
        //        if (countToPlaceInExistingSlot == 0)
        //            continue;
        //        if (countNew - countToPlaceInExistingSlot > 0)
        //        {
        //            _slots[i].IncreaseExistingItemCount(countToPlaceInExistingSlot);
        //            countNew -= countToPlaceInExistingSlot;
        //        }
        //        else
        //        {
        //            _slots[i].IncreaseExistingItemCount(countNew);
        //            countNew -= countNew;
        //        }
        //        if (countNew > 0)
        //            break;
        //        return true;
        //    }
        //}
        //for (int i = 0; i < _slots.Length; i++)
        //{
        //    if (_slots[i].Item == null)
        //    {
        //        _slots[i].AddItem(item, countNew);
        //        return true;
        //    }
        //}
        //return false;
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