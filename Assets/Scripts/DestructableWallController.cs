using UnityEngine;


public class DestructableWallController : MonoBehaviour
{
    #region UnityMethods

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerController>();
        if (player)
            Destroy(gameObject);
    }

    #endregion
}
