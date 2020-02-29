using UnityEngine;


public class LevelOneBossController : RangedEnemyController
{
    #region Fields

    public bool PlayerNoticed { set { _playerNoticed = value; } }

    [SerializeField] private Transform[] _movePoints;
    [SerializeField] private AudioClip _triggeredSound;

    private Transform _nextPoint;
    private SpriteRenderer _spriteRenderer;

    private bool _isRestingOnPlatform = false;
    private bool _isMovingToNewPoint = false;
    private bool _playerNoticed = false;
    private float _restingTime = 1.5f;

    #endregion


    #region UnityMethods

    protected override void Start()
    {
        base.Start();
        TriggerController.Triggered += OnTriggeredBossFight;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Update()
    {
        if (!_isDead)
        {
            if (CheckForPlayer() && !_playerSpotted)
            {
                _playerSpotted = true;
                Invoke("PlayerLostTimeout", _playerSpottedTimeout);
            }
        }
    }

    // Use this for initialization
    protected override void FixedUpdate()
    {
        if (!_isDead)
        {
            if (!_isRestingOnPlatform && !_isMovingToNewPoint)
            {
                GetNewPoint();
            }
            if (_isMovingToNewPoint)
            {
                MoveToPoint();
            }
            if (Vector3.Distance(transform.position,_nextPoint.position) < 1.0f)
            {
                RestingOnPlatform();
            }
        }
    }

    #endregion


    #region Methods

    private void GetNewPoint()
    {
        var randomPoint = Random.Range(0, _movePoints.Length);
        _nextPoint = _movePoints[randomPoint];
        _isMovingToNewPoint = true;
    }

    private void MoveToPoint()
    {
        transform.position += Vector3.Normalize(_nextPoint.position - transform.position) * _speed * Time.fixedDeltaTime;
        Rotate(_nextPoint.position, false);
    }

    private void RestingOnPlatform()
    {
        if (!_isRestingOnPlatform)
        {
            _isRestingOnPlatform = true;
            _isMovingToNewPoint = false;
            Invoke("RestingTimeout", _restingTime);
        }
        else
        {
            if (_playerNoticed)
            {
                Rotate(PlayerController.PlayerInstance.transform.position, false);
            }
            else
            {
                Rotate(Vector3.zero, true);

            }

            if (!_isAttackOnCooldown && _playerNoticed && !_isDead)
            {
                AttackPlayer();
            }
        }
    }

    private void Rotate(Vector3 to, bool isDefault)
    {
        if (!isDefault)
        {
            Vector2 movingDirection = to - transform.position;
            float angle = Mathf.Atan2(movingDirection.y, movingDirection.x) * Mathf.Rad2Deg;
            var eulerAngle = Quaternion.Euler(0, 0, angle);
            if (eulerAngle.z > 0.5 || eulerAngle.z < -0.5)
                _spriteRenderer.flipY = true;
            else
                _spriteRenderer.flipY = false;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            _spriteRenderer.flipY = false;
            transform.rotation = Quaternion.AngleAxis(0.0f, Vector3.forward);
        }
    }

    private void RestingTimeout()
    {
        _isRestingOnPlatform = false;
    }

    private void OnTriggeredBossFight()
    {
        _soundManager.PlaySound(_triggeredSound);
        _playerNoticed = true;
    }

    protected override void AttackPlayer()
    {
        _isAttackOnCooldown = true;
        Invoke("AttackCooldown", _attackCooldown);
        _animator.SetTrigger("AttackTrigger");
    }

    protected override void AttackCooldown()
    {
        _isAttackOnCooldown = false;
        Instantiate(_weapon, _weaponPosition.position, _weaponPosition.rotation);
    }

    #endregion
}
