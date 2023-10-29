using UnityEngine;

public class GhoulView : EnemyView
{
    [SerializeField] private Ghoul _ghoul;

    protected override void OnEnable()
    {
        _ghoul.HealthChanged += OnHealthChanged;
        _ghoul.InitializeHealth();
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        _ghoul.HealthChanged -= OnHealthChanged;
        base.OnDisable();
    }

    protected override void OnHealthChanged(float currentHealth)
    {
        _healthText.text = $"{currentHealth}";
    }
}