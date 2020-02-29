using UnityEngine;
using System;


public class BasePickupController : MonoBehaviour
{
    #region Fields

    public static Action<AudioClip> PlayPickUpSound;
    public static Func<InventoryItem, int, bool> PickingUp;

    public InventoryItem Item;
    public int Count;

    [SerializeField] protected AudioClip _pickupSound;

    #endregion

    #region UnityMethods

    private void Start()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision);
        if (collision.gameObject.CompareTag("Player"))
        {
            if (PickUp())
            {
                Destroy(gameObject);
            }
        }
    }

    #endregion

    #region Methods

    protected virtual bool PickUp()
    {
        return PickingUp.Invoke(Item, Count);
    }

    #endregion
}
