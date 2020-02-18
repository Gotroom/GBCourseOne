using UnityEngine;
using System;

public abstract class BasePickupController : MonoBehaviour
{
    #region Fields

    public static Action<AudioClip> PlayPickUpSound;

    [SerializeField] protected AudioClip _pickupSound;

    #endregion

    #region UnityMethods

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (PickUp())
            {
                Destroy(gameObject.transform.parent.gameObject);
            }
        }
    }

    #endregion

    #region Methods

    protected abstract bool PickUp();

    #endregion
}
