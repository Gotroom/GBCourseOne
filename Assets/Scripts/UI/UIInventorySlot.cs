using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIInventorySlot : MonoBehaviour
{
    [HideInInspector] public InventoryItem _item;
    [HideInInspector] public int CountInSlot = 0;


    [SerializeField] private Image _image;
    [SerializeField] private Text _countText;


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
}
