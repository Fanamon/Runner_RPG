using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Naga : MonoBehaviour
{
    [SerializeField] private float _healthModificator = 1;
    [SerializeField] private float _damageModificator = 1;
    [SerializeField] private float _armorModificator = 1;

    private float _health;
    private float _damage;
    private float _armor;

    private float _currentHealth;
    private Animator _animator;

    public event UnityAction<float> HealthChanged;
    public event UnityAction<float> DamageChanged;
    public event UnityAction<float> ArmorChanged;

    public void SetNagaParameters(float health, float damage, float armor)
    {
        _health = health * _healthModificator;
        _damage = damage * _damageModificator;
        _armor = armor * _armorModificator;
        _currentHealth = _health;
        _animator = GetComponent<Animator>();
        HealthChanged?.Invoke(_currentHealth);
        DamageChanged?.Invoke(_damage);
        ArmorChanged?.Invoke(_armor);
    }

    public float Damage => _damage;

    public void RoundHealth()
    {
        _currentHealth = Mathf.Ceil(_currentHealth);
        HealthChanged?.Invoke(_currentHealth);
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage * (1 - _armor / 100);

        if (_currentHealth <= 0)
        {
            Die();
        }
        else
        {
            _animator.Play("Attack");
        }
    }

    private void Die()
    {
        _currentHealth = 0;
        HealthChanged?.Invoke(_currentHealth);
        _animator.Play("Die");
        this.enabled = false;
    }
}
