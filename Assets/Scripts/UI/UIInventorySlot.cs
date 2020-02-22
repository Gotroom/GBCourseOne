using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIInventorySlot : MonoBehaviour
{
    [HideInInspector] public InventoryItem _item;
    [HideInInspector] public int CountInSlot = 0;


    [SerializeField] private Image _image;
    [SerializeField] private Text _countText;

    private void Start()
    {
        _item = null;
    }


    public void AddItem(InventoryItem item, int count)
    {
        _item = item;
        _image.sprite = item.Image;
        _image.enabled = true;
        _countText.enabled = true;
        CountInSlot += count;
        _countText.text = $"{CountInSlot}";
    }

    public void IncreaseExistingItemCount(int count)
    {
        CountInSlot += count;
        _countText.text = $"{CountInSlot}";
    }

    public void RemoveItem(InventoryItem item)
    {
        _item = null;
        _image.sprite = null;
        _image.enabled = false;
    }

    public void OnClick()
    {
        CountInSlot--;
        if (CountInSlot > 0)
        {
            _countText.text = $"{CountInSlot}";
        }
        else
        {
            _item = null;
            _image.sprite = null;
            _image.enabled = false;
            _countText.enabled = false;
        }
    }
}
