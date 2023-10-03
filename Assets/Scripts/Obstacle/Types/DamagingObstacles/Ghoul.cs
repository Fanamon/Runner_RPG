using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Ghoul : DamagingObstacle
{
    [SerializeField] private float _health;
    [SerializeField] private float _damage;
    [SerializeField] private float _armor;
    [SerializeField] private float _lifeStealFromDamagePercent;

    private float _currentHealth;
    private Animator _animator;

    public event UnityAction<float> HealthChanged;

    private void OnEnable()
    {
        _currentHealth = _health;
        TotalDamage = _damage;
        _animator = GetComponent<Animator>();
        HealthChanged?.Invoke(_currentHealth);
    }

    protected override void Die()
    {
        _currentHealth = 0;
        _animator.Play("Die");
        DisableObstacle();
    }

    protected override void StartCombat(ObstacleEntrySensor obstacleEntrySensor)
    {
        _currentHealth += Mathf.Ceil(TotalDamage * _lifeStealFromDamagePercent / 100);
        base.StartCombat(obstacleEntrySensor);
        HealthChanged?.Invoke(_currentHealth);
    }

    protected override void TakeDamage(float damage)
    {
        _currentHealth -= Mathf.Floor(damage * (1 - _armor / 100));

        if (_currentHealth <= 0)
        {
            Die();
        }
        else
        {
            _animator.Play("Attack");
        }

        HealthChanged?.Invoke(_currentHealth);
    }
}
