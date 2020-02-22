﻿using UnityEngine;

public class FireballController : ProjectileWeaponController
{
    #region Fields

    private const string EXPLODE_SOUND_NAME = "MineExplode";

    [SerializeField] private GameObject _smokeAfterExplosion;
    [SerializeField] private LayerMask _layers;
    [SerializeField] private float _destructionRange = 3.0f;
    [SerializeField] private float _damageRange = 15.0f;
    [SerializeField] private int _maxDamage = 3;

    private Collider2D _collider;

    #endregion


    #region UnityMethods

    protected override void Start()
    {
        base.Start();
        _collider = GetComponent<Collider2D>();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Untagged"))
        {
            PlayExplodeSound();
            EnemiesDestroyed?.Invoke(DealMineDamage(true) + DealMineDamage(false));
            //CreateSmoke();
            _speed = 0;
            _animator.Play("Fireball_Impact");
            _collider.enabled = false;
            Destroy(gameObject, 0.5f);
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