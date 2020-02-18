using UnityEngine;
using System.Collections;

public class ParallaxBackgound : MonoBehaviour
{

    [SerializeField] Transform _cameraTransform;
    [SerializeField] private float _parallaxEffect;
    [SerializeField] private bool _isAxisYParallax = false;

    private float _cameraPositionAxisX;
    private float _cameraPositionAxisY;
    private float _cameraStartingPositionY;
    private float _length;
    private Vector2 _startPos;
    


    // Use this for initialization
    void Start()
    {
        _startPos = transform.position;
        _length = GetComponent<SpriteRenderer>().bounds.size.x;
        _cameraStartingPositionY = _cameraTransform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        _cameraPositionAxisX = _cameraTransform.position.x;
        _cameraPositionAxisY = _cameraTransform.position.y - _cameraStartingPositionY;

        float distanceX = _cameraPositionAxisX * _parallaxEffect;
        float distanceY = _cameraPositionAxisY * _parallaxEffect;
        float deltaPosition = _cameraPositionAxisX * (1.0f - _parallaxEffect);

        var newPosition = transform.position;
        newPosition.x = _startPos.x + distanceX;
        
        if (_isAxisYParallax)
        {
            newPosition.y = _startPos.y + distanceY;
        }
        transform.position = newPosition;

        print(distanceY + " " + newPosition.y + " " + _startPos);
        if (deltaPosition > _startPos.x + _length)
        {
            _startPos.x += _length;
        }
        else if (deltaPosition < _startPos.x - _length)
        {
            _startPos.x -= _length;
        }
    }
}
