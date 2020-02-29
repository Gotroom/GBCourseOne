using UnityEngine;
using System;


public class TriggerController : MonoBehaviour
{
    #region Fields

    public static Action Triggered;
    public static Action TriggeredMusic;

    #endregion


    #region UnityMethods

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Triggered.Invoke();
            TriggeredMusic.Invoke();
            Destroy(gameObject);
        }
    }

    #endregion
}
