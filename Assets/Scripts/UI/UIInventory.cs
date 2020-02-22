using UnityEngine;
using System.Collections;

public class UIInventory : MonoBehaviour
{
    private UIInventorySlot[] _slots;
    // Use this for initialization
    void Start()
    {
        InventoryController.PickedUp = OnPickedUp;
        _slots = GetComponentsInChildren<UIInventorySlot>();
        print(_slots.Length);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnPickedUp(InventoryItem item, int count)
    {
        //gameObject.GetComponents<>
        for (int i = 0; i < _slots.Length - 1; i++)
        {
            if (_slots[i]._item.Image == null)
            {
                _slots[i].AddItem(item);
                break;
            }
        }
    }
}
