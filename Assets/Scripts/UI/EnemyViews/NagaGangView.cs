using TMPro;
using UnityEngine;

public class NagaGangView : EnemyView
{
    [SerializeField] protected TMP_Text _healthTextMiddle;
    [SerializeField] protected TMP_Text _healthTextRight;
    [SerializeField] private NagaGang _nagaGang;
    [SerializeField] private Naga[] _nagas;

    protected override void OnEnable()
    {
        _nagas[(int)NagaPosition.Left].HealthChanged += OnHealthChanged;
        _nagas[(int)NagaPosition.Middle].HealthChanged += OnHealthMiddleChanged;
        _nagas[(int)NagaPosition.Right].HealthChanged += OnHealthRightChanged;
        _nagaGang.InitializeNagas();
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        _nagas[(int)NagaPosition.Left].HealthChanged -= OnHealthChanged;
        _nagas[(int)NagaPosition.Middle].HealthChanged -= OnHealthMiddleChanged;
        _nagas[(int)NagaPosition.Right].HealthChanged -= OnHealthRightChanged;
        base.OnDisable();
    }

    protected override void OnHealthChanged(float currentHealth)
    {
        _healthText.text = $"{currentHealth}";
    }

    private void OnHealthMiddleChanged(float currentHealth)
    {
        _healthTextMiddle.text = $"{currentHealth}";
    }

    private void OnHealthRightChanged(float currentHealth)
    {
        _healthTextRight.text = $"{currentHealth}";
    }

    enum NagaPosition
    {
        Left = 0,
        Middle = 1,
        Right = 2,
    }
}
