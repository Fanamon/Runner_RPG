using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Ghoul : DamagingObstacle
{
    private float _currentHealth;
    private Animator _animator;

    public event UnityAction<float> HealthChanged;

    protected override void OnEnable()
    {
        base.OnEnable();
        EnemyObject.MaxHealthChanged += OnMaxHealthChanged;
        TotalDamage = EnemyObject.MeleeUnitDamage;
        _animator = GetComponent<Animator>();
    }

    private void OnDisable()
    {
        EnemyObject.MaxHealthChanged -= OnMaxHealthChanged;
    }

    public void InitializeHealth()
    {
        _currentHealth = EnemyObject.UnitHealth;
        HealthChanged?.Invoke(_currentHealth);
    }

    public override void TakeDamage(float damage)
    {
        _currentHealth -= Mathf.Floor(damage * (1 - EnemyObject.UnitArmor / 100));

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

    protected override void Die()
    {
        _currentHealth = 0;
        _animator.SetTrigger(AnimatorData.ObstacleParameters.Die);
        DisableObstacle();
    }

    protected override void StartCombat(ObstacleEntrySensor obstacleEntrySensor)
    {
        _currentHealth += Mathf.Ceil(TotalDamage * EnemyObject.LifeStealFromDamagePercent / 100);
        base.StartCombat(obstacleEntrySensor);
        HealthChanged?.Invoke(_currentHealth);
    }

    private void OnMaxHealthChanged(float maxHealth)
    {
        _currentHealth = maxHealth;
        HealthChanged?.Invoke(_currentHealth);
    }
}
