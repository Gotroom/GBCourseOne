using UnityEngine;

public class EnemyController : BaseCharacterController
{
    #region Fields

    private const float VIEWING_ANGLE = 30;

    [SerializeField] private LayerMask _environmentMask;
    [SerializeField] private float _fieldOfView = 5f;
    [SerializeField] private bool _stayingStill = false;

    private Vector2 _rightCliff;
    private Vector2 _leftCliff;

    private float _playerSpottedTimeout = 3.0f;
    private float _initX;
    private float _attackCooldown = 1.0f;
    private float _obstacleDistance = 2.0f;
    private bool _playerSpotted = false;
    private bool _inAttackRange = false;
    private bool _isAttackOnCooldown = false;

    #endregion

    #region UnityMethods

    protected override void Start()
    {
        base.Start();
        _health = _maxHealth;
        _leftCliff = new Vector2(-1, -1f);
        _rightCliff = new Vector2(1, -1f);
    }

    void Update()
    {
        print(_playerSpotted);
        if (CheckForPlayer() && !_playerSpotted)
        {
            _playerSpotted = true;
            Invoke("PlayerLostTimeout", _playerSpottedTimeout);
        }
        if (CheckForObstacle())
        {
            if (_playerSpotted)
            {
                _jumpForce = Vector2.up * _jumpSpeed;
                _isJumping = true;
            }
            else
                Flip();
        }
    }

    void FixedUpdate()
    {
        if (!_inAttackRange)
        {
            var speed = _speed * (_stayingStill ? 0.0f : 1.0f);
            transform.position += Vector3.right * _speed * Time.deltaTime;
            _animator.SetBool("IsChasing", _playerSpotted);
            _animator.SetFloat("Speed", _stayingStill ? 0.0f : 1.0f);
        }
        else if (!_isAttackOnCooldown)
        {
            PlayerController.PlayerInstance.DealDamage(1, PlayerController.PlayerInstance.transform.position - transform.position);
            _isAttackOnCooldown = true;
            Invoke("AttackCooldown", _attackCooldown);
            _animator.SetTrigger("AttackTrigger");
        }
        if (_isJumping && !_isAirborne)
        {
            _rigidBody.AddForce(_jumpForce, ForceMode2D.Impulse);
            _isJumping = false;
            _isAirborne = true;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            _isJumping = false;
            _isAirborne = false;
        }
    }

    #endregion

    #region Methods

    private void AttackCooldown()
    {
        _isAttackOnCooldown = false;
        _animator.SetBool("isAttacking", false);
    }

    private void PlayerLostTimeout()
    {
        _playerSpotted = false;
    }

    private bool CheckForObstacle()
    {
        var sidesCast = Physics2D.Raycast(_collider.bounds.center, _isFacingRight ? Vector2.right : Vector2.left,
            _obstacleDistance, _environmentMask);
        var bottomCast = Physics2D.Raycast(_collider.bounds.center, _isFacingRight ? _rightCliff : _leftCliff,
            _obstacleDistance, _environmentMask);
        if ((sidesCast.collider && !sidesCast.collider.CompareTag("Player")) || !bottomCast.collider)
        {
            return true;
        }
        return false;
    }

    private bool CheckForPlayer()
    {
        _inAttackRange = false;
        var direction = PlayerController.PlayerInstance.transform.position - transform.position;
        if (!_playerSpotted)
        {
            if ((direction.x < 0 && _isFacingRight) ||
            (direction.x > 0 && !_isFacingRight) ||
            Vector2.Angle(direction, _isFacingRight ? Vector2.right : Vector2.left) > VIEWING_ANGLE)
            {
                return false;
            }
        }
        else
        {
            if ((direction.x < 0 && _isFacingRight) ||
            (direction.x > 0 && !_isFacingRight))
            {
                if (!_isAirborne)
                    Flip();
            }
        }

        var rayTowardsPlayer = Physics2D.Raycast(_collider.bounds.center, direction, _fieldOfView, _environmentMask);
        Debug.DrawRay(_collider.bounds.center, direction.normalized * 1.5f, Color.red);
        if (rayTowardsPlayer)
        {
            if (!rayTowardsPlayer.collider.gameObject.CompareTag("Player"))
            {
                return false;
            }
            else
            {
                var attackRay = Physics2D.Raycast(_collider.bounds.center, direction, 1.5f, _environmentMask);
                if (attackRay && attackRay.collider.gameObject.CompareTag("Player"))
                {
                    print(attackRay.collider.gameObject.name);
                    _inAttackRange = true;
                }
                else
                    _inAttackRange = false;
                return true;
            }
        }
        return false;
    }

    public override bool DealDamage(int damage)
    {
        base.DealDamage(damage);
        return CheckForDeath();
    }

    public bool DealDamage(int damage, Vector2 direction)
    {
        base.DealDamage(damage);
        if (CheckForDeath())
        {
            return true;
        }
        else
        {
            direction.x = (direction.x / Mathf.Abs(direction.x)) * _speed;
            direction.y = _jumpSpeed;
            _rigidBody.velocity = direction;
            return false;
        }
    }

    private bool CheckForDeath()
    {
        if (_health <= 0)
        {
            PlayerController.PlayerInstance.Kills++;
            Destroy(gameObject);
            return true;
        }
        return false;
    }

    protected override void Flip()
    {
        base.Flip();
        _speed *= -1;
    }

    #endregion
}
