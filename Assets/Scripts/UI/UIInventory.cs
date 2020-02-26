using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

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

    private void Start()
    {
        InventoryController.UpdateUI = OnUpdate;
        DoorController.CheckKey = OnCheckKey;
        PlayerController.SecondaryWeaponUsed = OnSecondaryWeaponUsed;
        _slots = GetComponentsInChildren<UIInventorySlot>();
        print(_slots.Length);
    }


    private void Update()
    {
        for(int i = 0; i < _keyCodes.Length; i++)
        {
            if (Input.GetKeyDown(_keyCodes[i]))
            {
                if (_slots[i].Item.Type == InventoryItem.ItemType.Usable)
                {
                    foreach (var slot in _slots)
                    {
                        if (_slots[i] != slot)
                            slot.ClearSelection();
                    }
                }
                _slots[i].OnClick();
            }
        }
    }

    private void OnUpdate(Dictionary<InventoryItem, int> items)
    {
        foreach (var slot in _slots)
        {
            slot.RemoveItem();
        }

        int itterator = 0;
        foreach (var item in items)
        {
            _slots[itterator].AddItem(item.Key, item.Value);
            itterator++;
        }
    }

    private bool OnCheckKey()
    {
        bool keyFound = false;
        foreach (var slot in _slots)
        {
            if (slot.Item != null && slot.Item.Type == InventoryItem.ItemType.Key)
            {
                keyFound = true;
                slot.Use();
            }
        }
        return keyFound;
    }

    private void OnSecondaryWeaponUsed()
    {
        foreach (var slot in _slots)
        {
            if (slot.Item != null && 
                slot.Item.Type == InventoryItem.ItemType.Usable &&
                slot.IsWielded)
            {
                slot.Use();
            }
        }
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