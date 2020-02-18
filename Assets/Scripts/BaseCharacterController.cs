using UnityEngine;
using System;


public class BaseCharacterController : MonoBehaviour
{
    #region Fields

    public static Action Flipped;

    public float BulletVelocity = 10f;
    public float ThrowingPower = 10f;

    [SerializeField] protected Animator _animator;

    [SerializeField] protected float _speed = 5;
    [SerializeField] protected float _jumpSpeed = 15;
    [SerializeField] protected int _maxHealth = 5;

    protected Rigidbody2D _rigidBody;
    protected Collider2D _collider;
    protected Vector2 _jumpForce;
    
    protected float _horizontalMove = 0.0f;
    protected bool _isFacingRight = true;
    protected bool _isJumping = false;
    protected bool _isAirborne = false;
    protected bool _isHurt = false;
    protected bool _areControlsAllowed = true;
    protected int _health;

    #endregion

    #region UnityMethods

    virtual protected void Start()
    {
        _rigidBody = transform.GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    #endregion

    #region Methods

    virtual protected void Flip()
    {
        _isFacingRight = !_isFacingRight;
        if (_isFacingRight)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        Flipped?.Invoke();
    }



    public virtual bool DealDamage(int damage)
    {
        _health -= damage;
        return false;
    }

    #endregion
}
