using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System;
using System.Collections.Generic;


public class PlayerController : BaseCharacterController
{
    #region Fields

    private const float WALLS_CHECK_OFFSET = 0.3f;
    private const float MAX_AXIS_TO_FLIP = 0.1f;
    private const float RESPAWN_AXIS_Y_OFFSET = 30.0f;
    private const float RESPAWN_AXIS_X_OFFSET = 5.0f;

    public static PlayerController PlayerInstance => _PlayerInstance;
    public static Action<bool> FlyingModeApplied;
    public static Action<bool> PowerUpApplied;
    public static Action<int, int> HealthChanged;
    public static Action SecondaryWeaponUsed;

    [HideInInspector] public bool PreventFiring = false;
    [HideInInspector] public int Health => _health;
    [HideInInspector] public int Kills { get { return _killCounter; } set { _killCounter = value; } }
    
    private static PlayerController _PlayerInstance;

    [SerializeField] private InventoryController _inventory;
    [SerializeField] private BaseWeapon _mainWeapon;
    [SerializeField] private Transform _weaponPosition;
    [SerializeField] private Transform _spellPosition;
    [SerializeField] private LayerMask _groundLayers;

    public BaseWeapon _secondaryWeapon;

    private float _obstacleDistance = 0.15f;
    private float _fallMiltiplier = 2.5f;
    private float _lowJumpMultiplier = 2.0f;
    private float _backwardSpeed;
    private bool _isPoweredUp = false;
    private bool _isFlying = false;
    private bool _isPaused = false;
    private bool _isDead = false;
    private int _killCounter = 0;

    #endregion

    #region UnityMethods

    override protected void Start()
    {
        base.Start();
        var savedData = PlayerDataController.instance;
        if (savedData != null)
        {
            _health = savedData.PlayerHealth;
        }
        else
        {
            _health = _maxHealth;
        }
        _PlayerInstance = this;
        _backwardSpeed = _speed / 2.0f;
        _isDead = false;
        BaseWeapon.Kill = OnKill;
        HealthKitController.Heal = OnHeal;
        PowerUpController.ApplyPowerUp = OnPowerUp;
        FlyModeController.ApplyFlyingMode = OnFlyingMode;
        InventoryItem.WieldWeapon = OnWieldWeapon;
        UIController.Paused = OnPaused;
        RespawnController.FallDown = OnFallDown;
        _secondaryWeapon = null;
    }

    void Update()
    {
        if (!_isPaused && !_isDead)
        {
            _horizontalMove = CrossPlatformInputManager.GetAxis("Horizontal");
            Debug.Log(_horizontalMove);
            _isAirborne = !CheckGrounded();

            if (!_isAirborne)
                _areControlsAllowed = true;

            if (CrossPlatformInputManager.GetButtonDown("Jump") && (!_isAirborne || _isFlying))
            {
                _isJumping = true;
            }

            ProcessPowerJump();

            ProcessMousePosition();

            if (!PreventFiring)
            {
                if (CrossPlatformInputManager.GetButtonDown("Fire1"))
                {
                    if (_secondaryWeapon == null)
                    {
                        Instantiate(_mainWeapon, _weaponPosition.position, _weaponPosition.rotation);
                        _animator.SetTrigger("attackTrigger");
                    }
                    else
                    {
                        Instantiate(_secondaryWeapon, _spellPosition.position, Quaternion.identity);
                        _animator.SetTrigger("superAttackTrigger");
                        SecondaryWeaponUsed.Invoke();
                    }
                }
                if (_mainWeapon)
                {
                    _mainWeapon.Speed = _isFacingRight ? BulletVelocity : BulletVelocity * -1;
                    _mainWeapon.DamageMultiplier = _isPoweredUp ? 2 : 1;
                }
                if (_secondaryWeapon)
                {
                    _secondaryWeapon.Speed = _isFacingRight ? BulletVelocity : BulletVelocity * -1;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        float speed = GetSpeed();
        if (_areControlsAllowed)
        {
            if (CheckWallsHit())
                _rigidBody.velocity = new Vector2(0, _rigidBody.velocity.y);
            else
                _rigidBody.velocity = new Vector2(_horizontalMove * speed, _rigidBody.velocity.y);
        }

        if ((!_isAirborne || _isFlying) && _isJumping)
        {
            _isJumping = false;
            _rigidBody.velocity += Vector2.up * _jumpSpeed;
            _isAirborne = true;
        }

        _animator.SetBool("isJumping", _isAirborne);
        _animator.SetFloat("Speed", Mathf.Abs(_horizontalMove));
        _animator.SetBool("isMovingBackwards", speed < _speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            DealDamage(1, transform.position - collision.gameObject.transform.position);
        }
    }

#endregion

#region Methods

    private void OnKill()
    {
        _health = 0;
        HealthChanged?.Invoke(_health, _maxHealth);
        _speed = 0;
        _jumpSpeed = 0;
        _isDead = true;
        _animator.SetBool("isDead", true);
        _animator.Play("Wizard_Die");
    }

    private bool OnHeal(int healingPower)
    {
        if (_health < _maxHealth)
        {
            _health++;
            HealthChanged?.Invoke(_health, _maxHealth);
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool OnPowerUp(bool enable, float duration)
    {
        if (_isPoweredUp != enable)
        {
            _isPoweredUp = enable;
            Invoke("PowerUpExpired", duration);
            PowerUpApplied.Invoke(_isPoweredUp);
            return true;
        }
        return false;
    }

    private void OnWieldWeapon(BaseWeapon weapon)
    {
        _secondaryWeapon = weapon;
    }

    private void PowerUpExpired()
    {
        PowerUpApplied.Invoke(false);
        _isPoweredUp = false;
    }

    private bool OnFlyingMode(bool enable, float duration)
    {
        if (_isFlying != enable)
        {
            _isFlying = enable;
            Invoke("FlyingExpired", duration);
            FlyingModeApplied.Invoke(_isFlying);
            return true;
        }
        return false;
    }

    private void OnFallDown()
    {
        var position = transform.position;
        position.y += RESPAWN_AXIS_Y_OFFSET;
        position.x -= RESPAWN_AXIS_X_OFFSET;
        transform.position = position;
        _rigidBody.velocity = Vector2.zero;
    }

    private void FlyingExpired()
    {
        FlyingModeApplied.Invoke(false);
        _isFlying = false;
    }

    private void OnPaused(bool enabled)
    {
        _isPaused = enabled;
    }

    protected override void Flip()
    {
        base.Flip();
    }

    private bool CheckGrounded()
    {
        var size = _collider.bounds.size;
        size.x = _collider.bounds.extents.x;
        RaycastHit2D groundHit = Physics2D.BoxCast(_collider.bounds.center, size, 0f, Vector2.down, _obstacleDistance, _groundLayers);
        return groundHit.collider != null;
    }

    private bool CheckWallsHit()
    {
        var position = _collider.bounds.center;
        position += Vector3.up * WALLS_CHECK_OFFSET;
        position += _horizontalMove > 0.0f ? Vector3.right * WALLS_CHECK_OFFSET : Vector3.left * WALLS_CHECK_OFFSET;
        var size = _collider.bounds.size;
        size.y -= WALLS_CHECK_OFFSET;
        RaycastHit2D wallsHit = Physics2D.BoxCast(position, size, 0f, _horizontalMove > 0 ? Vector2.right : Vector2.left, _obstacleDistance, _groundLayers);
        Debug.DrawRay(position, _horizontalMove > 0 ? Vector2.right * _obstacleDistance : Vector2.left * _obstacleDistance, Color.blue);
        return wallsHit;
    }

    private void ProcessMousePosition()
    {
        Vector3 shootingDirection = Vector3.zero;
#if UNITY_STANDALONE_WIN
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            shootingDirection = worldMousePos - transform.position;
            shootingDirection.Normalize();
#endif

#if UNITY_ANDROID
        if (Input.touches.Length > 0)
        {
            shootingDirection = Camera.main.ScreenToWorldPoint(Input.touches[Input.touches.Length - 1].position) - transform.position;
            shootingDirection.Normalize();
        }
#endif

        if (shootingDirection.x <= -MAX_AXIS_TO_FLIP && _isFacingRight)
        {
            Flip();
        }
        if (shootingDirection.x >= MAX_AXIS_TO_FLIP && !_isFacingRight)
        {
            Flip();
        }

        var look = Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg;
    }

    private void ProcessPowerJump()
    {
        if (_rigidBody.velocity.y < 0)
        {
            _rigidBody.velocity += Vector2.up * Physics2D.gravity.y * (_fallMiltiplier - 1) * Time.deltaTime;
        }
        else if (_rigidBody.velocity.y > 0 && !CrossPlatformInputManager.GetButton("Jump"))
        {
            _rigidBody.velocity += Vector2.up * Physics2D.gravity.y * (_lowJumpMultiplier) * Time.deltaTime;
        }
    }

    private float GetSpeed()
    {
        return _speed;
    }

    public void DealDamage(int damage, Vector2 direction)
    {
        base.DealDamage(damage);
        HealthChanged?.Invoke(_health, _maxHealth);
        if (_health <= 0)
        {
            OnKill();
        }
        else
        {
            _animator.Play("Wizard_Hurt");
            _areControlsAllowed = false;
            _rigidBody.AddForce(direction,ForceMode2D.Impulse);
        }
    }

#endregion
}
