using UnityEngine;
using System.Collections;


public class MainCameraController : MonoBehaviour
{
    #region Fields

    private const float AXIS_OFFSET = 1;

    [SerializeField] private GameObject _followedObject;

    private Collider2D _objectCollider;
    private float _horizontalOffset;
    private float _verticalOffset;

    #endregion

    #region UnityMethods

    private void Start()
    {
        _objectCollider = _followedObject.GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        _horizontalOffset = Input.GetAxis("Horizontal");
        _verticalOffset = Input.GetAxis("Vertical");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var position = transform.position;
        position.x = _objectCollider.bounds.center.x + AXIS_OFFSET * _horizontalOffset;
        position.y = _objectCollider.bounds.center.y + AXIS_OFFSET * _verticalOffset;

        transform.position = position;
    }

    #endregion
}
