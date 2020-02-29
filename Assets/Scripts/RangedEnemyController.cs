using UnityEngine;


public class RangedEnemyController : EnemyController
{
    #region Fields

    [SerializeField] protected ProjectileWeaponController _weapon;
    [SerializeField] protected GameObject _weaponSprite;
    [SerializeField] protected Transform _weaponPosition;
    [SerializeField] protected AudioClip _attackSound;

    [SerializeField] protected float _projectileCreateDelay = 0.5f;

    #endregion


    #region Methods

    protected override void AttackPlayer()
    {
        _isAttackOnCooldown = true;
        if (_attackSound != null)
            _soundManager.PlaySound(_attackSound);
        Invoke("AttackCooldown", _attackCooldown);
        Invoke("ProjectileCreateCooldown", _projectileCreateDelay);
        _animator.SetTrigger("AttackTrigger");
        _weaponSprite.SetActive(true);
    }

    protected override void AttackCooldown()
    {
        _isAttackOnCooldown = false;
    }

    protected void ProjectileCreateCooldown()
    {
        Instantiate(_weapon, _weaponPosition.position, _weaponPosition.rotation);
    }

    #endregion
}
