using UnityEngine;
using UnityEngine.UI;
using System;

public class UIInventorySlot : MonoBehaviour
{
    #region Fields

    public static Action<InventoryItem> ItemUsed;

    [HideInInspector] public InventoryItem Item;
    [HideInInspector] public int CountInSlot = 0;

    public bool IsWielded = false;

    [SerializeField] private Image _image;
    [SerializeField] private Text _countText;

    private Button _button;


    #endregion


    #region UnityMethods

    private void Awake()
    {
        Item = null;
        _button = GetComponent<Button>();
    }

    private void OnDisable()
    {
        PlayerController.SecondaryWeaponUsed = null;
    }

    #endregion


    #region Methods

    public void AddItem(InventoryItem item, int count)
    {
        Item = item;
        _image.sprite = item.Image;
        _image.enabled = true;
        _countText.enabled = true;
        CountInSlot = count;
        _countText.text = $"{CountInSlot}";
        
    }

    public void RemoveItem()
    {
        ClearSelection();
        Item = null;
        _image.sprite = null;
        _image.enabled = false;
        _countText.text = $"{0}";
        _countText.enabled = false;
    }

    public void OnClick()
    {
        if(Item != null)
        {
            if (Item.Type == InventoryItem.ItemType.Consumable)
            {
                ProcessConsumable();
            }
            else if (Item.Type == InventoryItem.ItemType.Usable)
            {
                ProcessUsable();
            }
        }
    }

    private void ProcessConsumable()
    {
        CountInSlot--;
        print("Consuming " + Item.Name);
        Item.Consume();
        if (CountInSlot > 0)
        {
            _countText.text = $"{CountInSlot}";
        }
        else
        {
            Item = null;
            _image.sprite = null;
            _image.enabled = false;
            _countText.enabled = false;
        }
    }

    private void ProcessUsable()
    {
        OnWield(!IsWielded);
    }

    public void Use()
    {
        CountInSlot--;
        Item.Use();
        if (CountInSlot > 0)
        {
            _countText.text = $"{CountInSlot}";
        }
        else
        {
            OnWield(false);
            Item = null;
            _image.sprite = null;
            _image.enabled = false;
            _countText.enabled = false;
        }
    }

    public void ClearSelection()
    {
        OnWield(false);
    }

    private void OnWield(bool wield)
    {
        if (Item != null)
        {
            IsWielded = wield;
            if (wield)
            {
                _button.OnSelect(null);
                Item.Wield();
            }
            else
            {
                _button.OnDeselect(null);
                Item.Unwield();
            }
        }
    }

    #endregion
}
