using UnityEngine;
using System;

public class BasePickupController : MonoBehaviour
{
    #region Fields

    public static Action<AudioClip> PlayPickUpSound;
    public static Predicate<InventoryItem> PickingUp;

    public InventoryItem Item;

    [SerializeField] protected AudioClip _pickupSound;

    #endregion

    #region UnityMethods

    private void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (PickUp())
            {
               // Destroy(gameObject.transform.parent.gameObject);
            }
        }
    }

    #endregion

    #region Methods

    protected virtual bool PickUp()
    {
        PickingUp.Invoke(Item);
        return true;
    }

    #endregion
}
