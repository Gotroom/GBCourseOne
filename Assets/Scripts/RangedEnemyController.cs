using UnityEngine;
using System.Collections;

public class RangedEnemyController : EnemyController
{
    [SerializeField] protected ProjectileWeaponController _weapon;
    [SerializeField] protected GameObject _weaponSprite;
    [SerializeField] protected Transform _weaponPosition;

    protected override void AttackPlayer()
    {
        _isAttackOnCooldown = true;
        Invoke("AttackCooldown", _attackCooldown);
        _animator.SetTrigger("AttackTrigger");
        _weaponSprite.SetActive(true);
    }

    protected override void AttackCooldown()
    {
        _isAttackOnCooldown = false;
        Instantiate(_weapon, _weaponPosition.position, _weaponPosition.rotation);
    }
}
