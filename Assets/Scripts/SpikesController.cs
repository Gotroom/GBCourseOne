using UnityEngine;

public class SpikesController : MonoBehaviour
{
    #region UnityMethods

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerController>();
        if (player)
            BaseWeapon.Kill.Invoke();
    }

    #endregion
}
