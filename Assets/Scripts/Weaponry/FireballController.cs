using UnityEngine;

public class FireballController : BaseWeapon
{
    #region Fields

    private const string EXPLODE_SOUND_NAME = "MineExplode";

    [SerializeField] private GameObject _smokeAfterExplosion;
    [SerializeField] private LayerMask _layers;
    [SerializeField] private float _destructionRange = 3.0f;
    [SerializeField] private float _damageRange = 15.0f;
    [SerializeField] private int _maxDamage = 3;

    private Vector2 _shootingDirection;
    private Collider2D _collider;

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

    void Update()
    {
        _rigidbody.velocity = Mathf.Abs(_speed) * _shootingDirection;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //PlayExplodeSound();
            EnemiesDestroyed?.Invoke(DealMineDamage(true) + DealMineDamage(false));
            //CreateSmoke();
            Destroy(gameObject);
        }
    }

    #endregion


    #region Methods

    private void PlayExplodeSound()
    {
        FindObjectOfType<SoundManager>().PlaySoundByName(EXPLODE_SOUND_NAME);
    }

    private int DealMineDamage(bool isEpicenter)
    {
        int damage = isEpicenter ? _maxDamage : _defaultDamage;
        float range = isEpicenter ? _destructionRange : _damageRange;
        int destroyed = 0;
        var colliders = Physics2D.OverlapCircleAll(transform.position, range, _layers);
        foreach (var collider in colliders)
        {
            if (EffectOnCollider(collider, damage))
                destroyed++;
        }
        return destroyed;
    }

    private bool EffectOnCollider(Collider2D collider, int damage)
    {
        bool isPlayer = collider.gameObject.CompareTag("Player");
        bool isEnemy = collider.gameObject.CompareTag("Enemy");
        if (collider.gameObject != gameObject && !collider.isTrigger)
        {
            var direction = collider.gameObject.transform.position - transform.position;
            if (isPlayer)
            {
                collider.gameObject.GetComponent<PlayerController>().DealDamage(damage, direction);
                return false;
            }
            else if (isEnemy)
            {
                if (collider.gameObject.GetComponent<EnemyController>().DealDamage(damage, direction))
                    return true;
            }
            else
            {
                collider.gameObject.GetComponent<Rigidbody2D>().AddForce(direction, ForceMode2D.Impulse);
                return false;
            }
        }
        return false;
    }

    private void CreateSmoke()
    {
        //Instantiate(_smokeAfterExplosion, _collider.bounds.min, transform.rotation);
    }

    #endregion
}
