using System.Collections;
using UnityEngine;

public class GhoulBossView : BossView
{
    protected override void OnEnable()
    {
        Boss.HealthChanged += OnHealthChanged;
        Boss.Initialize();
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        Boss.HealthChanged -= OnHealthChanged;
        base.OnDisable();
    }

    protected override void OnHealthChanged(float currentHealth, float maxHealth)
    {
        if (BarValueDisplayer != null)
        {
            StopCoroutine(BarValueDisplayer);
        }

        BarValueDisplayer = StartCoroutine(ChangeBarValue(currentHealth, maxHealth));
    }

    private IEnumerator ChangeBarValue(float currentHealth, float maxHealth)
    {
        float currentValuePercentage = currentHealth / maxHealth;

        _healthText.text = $"{currentHealth} / {maxHealth}";

        while (HealthBar.value != currentValuePercentage)
        {
            HealthBar.value = Mathf.MoveTowards(HealthBar.value, currentValuePercentage, Time.deltaTime);

            yield return null;
        }
    }
}
