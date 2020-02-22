using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public enum ItemType
    {
        Usable,
        Consumable
    }

    public Sprite Image;
    public string Name = "New Item";
    public int Count = 1;
    public ItemType Type;
}
