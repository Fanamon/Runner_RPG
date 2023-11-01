using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Naga : MonoBehaviour
{
    private float _currentHealth;
    private Enemy _scriptableObject;
    private Animator _animator;

    public event UnityAction<float> HealthChanged;

    public void SetNagaParameters(Enemy scriptableObject)
    {
        _scriptableObject = scriptableObject;
        _scriptableObject.MaxHealthChanged += OnMaxHealthChanged;
        _currentHealth = _scriptableObject.UnitHealth;
        _animator = GetComponent<Animator>();
        HealthChanged?.Invoke(_currentHealth);
    }

    private void OnDisable()
    {
        _scriptableObject.MaxHealthChanged -= OnMaxHealthChanged;
    }

    public void RoundHealth()
    {
        _currentHealth = Mathf.Ceil(_currentHealth);
        HealthChanged?.Invoke(_currentHealth);
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage * (1 - _scriptableObject.UnitArmor / 100);

        if (_currentHealth <= 0)
        {
            Die();
        }
        else
        {
            _animator.SetTrigger(AnimatorData.ObstacleParameters.Attack);
        }

        HealthChanged?.Invoke(_currentHealth);
    }

    private void Die()
    {
        _currentHealth = 0;
        HealthChanged?.Invoke(_currentHealth);
        _animator.SetTrigger(AnimatorData.ObstacleParameters.Die);
        this.enabled = false;
    }

    private void OnMaxHealthChanged(float maxHealth)
    {
        _currentHealth = maxHealth;
        HealthChanged?.Invoke(_currentHealth);
    }
}
