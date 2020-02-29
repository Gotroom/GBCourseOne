using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class ProjectileWeaponController : BaseWeapon
{
    #region Fields

    [SerializeField] protected Animator _animator;

    [SerializeField] protected bool _isUsedByEnemy = false;
    [SerializeField] protected bool _isAnimated = true;

    protected Vector2 _shootingDirection;

    #endregion


    #region UnityMethods

    protected override void Start()
    {
        base.Start();
        if (!_isUsedByEnemy)
        {
#if UNITY_STANDALONE_WIN
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            _shootingDirection = worldMousePos - transform.position;
            _shootingDirection.Normalize();
            var look = Mathf.Atan2(_shootingDirection.y, _shootingDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, look);
#endif
#if UNITY_ANDROID
            if (Input.touches.Length > 0)
            {
                _shootingDirection = Camera.main.ScreenToWorldPoint(Input.touches[Input.touches.Length - 1].position) - transform.position;

                _shootingDirection.Normalize();
                var look = Mathf.Atan2(_shootingDirection.y, _shootingDirection.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, look);
            }
#endif
        }
    }

    protected virtual void Update()
    {
        _rigidbody.velocity = Mathf.Abs(_speed) * _shootingDirection;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isUsedByEnemy)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<PlayerController>().DealDamage(_defaultDamage * _damageMultiplier, Vector2.right * (PlayerController.PlayerInstance.transform.position - transform.position));
            }
            if (!collision.gameObject.CompareTag("Enemy") && !collision.gameObject.CompareTag("Foreground")) Destroy(gameObject);
        }
        else
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                if (collision.gameObject.GetComponent<EnemyController>().DealDamage(_defaultDamage * _damageMultiplier))
                    EnemiesDestroyed.Invoke(1);
            }
            if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Foreground")) Destroy(gameObject);
        }
    }

#endregion
}
