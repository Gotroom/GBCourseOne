using UnityEngine;
using System;


public class TriggerController : MonoBehaviour
{
    #region Fields

    public static Action Triggered;

    #endregion


    #region UnityMethods

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Triggered.Invoke();
            Destroy(gameObject);
        }
    }

    #endregion
}
