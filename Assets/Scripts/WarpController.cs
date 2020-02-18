using UnityEngine;
using System;


public class WarpController : MonoBehaviour
{
    #region Fields

    public static Action<string> WarpToZone;

    #endregion

    #region UnityMethods

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            WarpToZone.Invoke("Endless");
    }

    #endregion
}
