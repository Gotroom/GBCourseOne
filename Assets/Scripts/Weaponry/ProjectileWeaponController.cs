using UnityEngine;


public class ProjectileWeaponController : BaseWeapon
{
    #region Fields

    [SerializeField] protected Animator _animator;

    protected Vector2 _shootingDirection;

    #endregion

    #region UnityMethods

    protected override void Start()
    {
        base.Start();
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        _shootingDirection = worldMousePos - transform.position;
        _shootingDirection.Normalize();
        var look = Mathf.Atan2(_shootingDirection.y, _shootingDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, look);
    }

    protected virtual void Update()
    {
        _rigidbody.velocity = Mathf.Abs(_speed) * _shootingDirection;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.GetComponent<EnemyController>().DealDamage(_defaultDamage * _damageMultiplier))
                EnemiesDestroyed.Invoke(1);
        }
        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Foreground")) Destroy(gameObject);
    }

    #endregion
}
