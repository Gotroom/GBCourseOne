using UnityEngine;


public class MovingBackgroundController : MonoBehaviour
{
    #region UnityMethods

    private void FixedUpdate()
    {
        var playerPosition = PlayerController.PlayerInstance.transform.position;
        transform.position = playerPosition;
    }

    #endregion
}
