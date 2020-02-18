using UnityEngine;


public class MovingBackgroundController : MonoBehaviour
{
    #region Fields

    private const float AXIS_OFFSET = 1;

    private float _horizontalOffset;
    private float _verticalOffset;

    #endregion

    #region UnityMethods

    private void Update()
    {
        _horizontalOffset = Input.GetAxis("Horizontal");
        _verticalOffset = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        var playerPosition = PlayerController.PlayerInstance.transform.position;
        playerPosition.x += AXIS_OFFSET * _horizontalOffset;
        playerPosition.y += AXIS_OFFSET * _verticalOffset;
        transform.position = playerPosition;
    }

    #endregion
}
