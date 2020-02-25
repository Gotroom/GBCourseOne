using UnityEngine;
using UnityEngine.UI;
using System;

public class UIInventorySlot : MonoBehaviour
{
    #region Fields

    public static Action<InventoryItem> ItemUsed;

    [HideInInspector] public InventoryItem Item;
    [HideInInspector] public int CountInSlot = 0;

    [SerializeField] private Image _image;
    [SerializeField] private Text _countText;

    private Button _button;

    private bool _isWielded = false;

    #endregion


    #region UnityMethods

    private void Awake()
    {
        Item = null;
        _button = GetComponent<Button>();
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
        PlayerController.SecondaryWeaponUsed += OnSecondaryWeaponUsed;
    }

    public void RemoveItem()
    {
        Item = null;
        _image.sprite = null;
        _image.enabled = false;
        OnWield(false);
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
        OnWield(!_isWielded);
    }

    private void OnSecondaryWeaponUsed(BaseWeapon weaponController)
    {
        var scroll = Item as Scroll;
        if (_isWielded && scroll.Controller == weaponController)
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
    }

    public void ClearSelection()
    {
        OnWield(false);
    }

    private void OnWield(bool wield)
    {
        if (Item != null)
        {
            _isWielded = wield;
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
