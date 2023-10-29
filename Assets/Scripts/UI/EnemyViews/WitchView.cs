using UnityEngine;

public class WitchView : EnemyView
{
    [SerializeField] private Witch _witch;

    protected override void OnEnable()
    {
        _witch.HealthChanged += OnHealthChanged;
        _witch.InitializeHealth();
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        _witch.HealthChanged -= OnHealthChanged;
        base.OnDisable();
    }

    protected override void OnHealthChanged(float currentHealth)
    {
        _healthText.text = $"{currentHealth}";
    }
}
