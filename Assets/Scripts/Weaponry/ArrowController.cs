using UnityEngine;
using System.Collections;

public class ArrowController : ProjectileWeaponController
{
    #region UnityMethods

    protected override void Start()
    {
        base.Start();
        if(_isUsedByEnemy)
        {
            var distance = Vector3.Distance(PlayerController.PlayerInstance.transform.position, transform.position);
            var direction = Vector3.Normalize(PlayerController.PlayerInstance.transform.position - transform.position);
            var velo = Vector2.one;
            velo.y = Mathf.Asin(distance * _rigidbody.gravityScale / (2 * _speed)) + direction.y;
            velo.x = Mathf.Deg2Rad * (90 - (Mathf.Rad2Deg * velo.y)) * direction.x;
            _rigidbody.AddForce(velo * _speed, ForceMode2D.Impulse);
        }
        
    }

    protected override void Update()
    {
        return;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    #endregion
}
