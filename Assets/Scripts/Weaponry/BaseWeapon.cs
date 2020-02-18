using UnityEngine;
using System;

public class BaseWeapon : MonoBehaviour
{
    #region Fields

    public static Action<int> EnemiesDestroyed;
    public static Action Kill;

    [HideInInspector] public float Speed { get { return _speed; } set { _speed = value; } }
    [HideInInspector] public int DamageMultiplier { get { return _damageMultiplier; } set { _damageMultiplier = value; } }

    [SerializeField] protected float _speed = 10.0f;
    [SerializeField] protected int _defaultDamage = 1;
    [SerializeField] protected int _damageMultiplier = 1;

    protected Rigidbody2D _rigidbody;

    #endregion

    #region UnityMethods

    protected virtual void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    #endregion
}
