using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class UIInventory : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    #region Fields

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

    #endregion


    #region UnityMethods

    private void Awake()
    {
        InventoryController.UpdateUI = OnUpdate;
        DoorController.CheckKey = OnCheckKey;
        PlayerController.SecondaryWeaponUsed = OnSecondaryWeaponUsed;
    }

    private void Start()
    {
        _slots = GetComponentsInChildren<UIInventorySlot>();
        var items = new Dictionary<InventoryItem, int>();
        if (PlayerDataController.instance != null)
        {
            foreach (var item in PlayerDataController.instance.ItemsList)
            {
                items.Add(item.Key, item.Value);
            }
        }
        OnUpdate(items);
    }

    private void OnDisable()
    {
        InventoryController.UpdateUI = null;
        DoorController.CheckKey = null;
        PlayerController.SecondaryWeaponUsed = null;
    }

#if UNITY_STANDALONE_WIN
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
#endif

    #endregion

    #region Methods

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

    #endregion


    #region IPointerExitHandler

    public void OnPointerEnter(PointerEventData eventData)
    {
#if UNITY_STANDALONE_WIN
        PlayerController.PlayerInstance.PreventFiring = true;
#endif
    }

    public void OnPointerExit(PointerEventData eventData)
    {
#if UNITY_STANDALONE_WIN
        PlayerController.PlayerInstance.PreventFiring = false;
#endif
    }

    #endregion
}