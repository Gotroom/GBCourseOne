using UnityEngine;
using System.Collections;

public class LevelOneBossController : RangedEnemyController
{

    [SerializeField] private Transform[] _movePoints;

    private Transform _nextPoint;
    private SpriteRenderer _spriteRenderer;

    private bool _isRestingOnPlatform = false;
    private bool _isMovingToNewPoint = false;
    private bool _playerNoticed = true;
    private float _restingTime = 1.5f;

    protected override void Start()
    {
        base.Start();
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
        print(_isDead);
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

            if (!_isAttackOnCooldown && _playerNoticed)
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

}
