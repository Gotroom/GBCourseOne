using UnityEngine;
using UnityEngine.UI;
using System;

public class UIInventorySlot : MonoBehaviour
{
    [HideInInspector] public InventoryItem Item;
    [HideInInspector] public int CountInSlot = 0;


    [SerializeField] private Image _image;
    [SerializeField] private Text _countText;

    private Button _button;

    private bool _isWielded = false;

    private void Awake()
    {
        Item = null;
        _button = GetComponent<Button>();
        //UIInventory.ClearSelection = OnClearSelection;
        UIInventory.ClearSelection = null;
    }

    private void Start()
    {
        
        UIInventory.ClearSelection = OnClearSelection;
    }


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

    public void IncreaseExistingItemCount(int count)
    {
        CountInSlot += count;
        if (_countText != null)
            _countText.text = $"{CountInSlot}";
    }

    public void RemoveItem(InventoryItem item)
    {
        Item = null;
        _image.sprite = null;
        _image.enabled = false;
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

    private void OnSecondaryWeaponUsed()
    {
        if (_isWielded)
        {
            CountInSlot--;
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

    private void OnClearSelection()
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
}
