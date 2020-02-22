using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIInventorySlot : MonoBehaviour
{
    [HideInInspector] public InventoryItem _item;


    [SerializeField] private Image _image;

    public void AddItem(InventoryItem item)
    {
        _item = item;
        _image.sprite = item.Image;
        _image.enabled = true;
    }

    public void RemoveItem(InventoryItem item)
    {
        _item = null;
        _image.sprite = null;
        _image.enabled = false;
    }
}
