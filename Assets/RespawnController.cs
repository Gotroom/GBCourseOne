using UnityEngine;
using System;


public class RespawnController : MonoBehaviour
{
    public static Action FallDown;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FallDown.Invoke();
        }
    }
}
