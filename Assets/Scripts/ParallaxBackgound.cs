using UnityEngine;
using System.Collections;

public class ParallaxBackgound : MonoBehaviour
{
    #region Fields

    [SerializeField] Transform _cameraTransform;

    [SerializeField] private float _parallaxEffect;
    [SerializeField] private float _animateSpeed = 1.0f;
    [SerializeField] private bool _isAxisYParallax = false;
    [SerializeField] private bool _animateBackground = false;

    private float _cameraPositionAxisX;
    private float _cameraPositionAxisY;
    private float _cameraStartingPositionY;
    private float _length;
    private Vector2 _startPos;

    #endregion


    #region UnityMethods

    void Start()
    {
        _startPos = transform.position;
        _length = GetComponent<SpriteRenderer>().bounds.size.x;
        _cameraStartingPositionY = _cameraTransform.position.y;
    }

    void FixedUpdate()
    {
        _cameraPositionAxisX = _cameraTransform.position.x;
        _cameraPositionAxisY = _cameraTransform.position.y - _cameraStartingPositionY;

        float distanceX = _cameraPositionAxisX * _parallaxEffect;
        float distanceY = _cameraPositionAxisY * _parallaxEffect;
        float deltaPosition = _cameraPositionAxisX * (1.0f - _parallaxEffect);

        var newPosition = transform.position;
        
        if (_animateBackground)
        {
            transform.position = Animate(newPosition);
        }
        else
        {
            transform.position = MoveWithParallax(newPosition, distanceX, distanceY);
        }

        DisplaceBackground(deltaPosition);
    }

    #endregion


    #region Methods

    private Vector3 Animate(Vector3 position)
    {
        position.x = _startPos.x + _animateSpeed;
        _startPos.x += _animateSpeed;
        return position;
    }

    private Vector3 MoveWithParallax(Vector3 position, float deltaX, float deltaY)
    {
        position.x = _startPos.x + deltaX;

        if (_isAxisYParallax)
        {
            position.y = _startPos.y + deltaY;
        }
        return position;
    }

    private void DisplaceBackground(float delta)
    {
        if (delta > _startPos.x + _length)
        {
            _startPos.x += _length;
        }
        else if (delta < _startPos.x - _length)
        {
            _startPos.x -= _length;
        }
    }

    #endregion
}
