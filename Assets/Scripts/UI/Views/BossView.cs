using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class BossView : MonoBehaviour
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] protected Slider HealthBar;
    [SerializeField] protected TMP_Text _healthText;
    [SerializeField] private TMP_Text _damageText;
    [SerializeField] private TMP_Text _armorText;
    [SerializeField] private TMP_Text _lifeStealFromDamagePercent;
    [SerializeField] protected Boss Boss;

    protected Coroutine BarValueDisplayer;

    private void Awake()
    {
        _title.text = Boss.Title;
    }

    protected virtual void OnEnable()
    {
        Boss.DamageChanged += OnDamageChanged;
        Boss.ArmorChanged += OnArmorChanged;
        Boss.LifeStealChanged += OnLifeStealChanged;
        Boss.Initialize();
    }

    protected virtual void OnDisable()
    {
        Boss.DamageChanged -= OnDamageChanged;
        Boss.ArmorChanged -= OnArmorChanged;
        Boss.LifeStealChanged -= OnLifeStealChanged;
    }

    protected abstract void OnHealthChanged(float currentHealth, float maxHealth);

    private void OnDamageChanged(float damage)
    {
        _damageText.text = damage.ToString();
    }

    private void OnArmorChanged(float armor)
    {
        _armorText.text = armor.ToString();
    }

    private void OnLifeStealChanged(float lifesteal)
    {
        _lifeStealFromDamagePercent.text = $"{lifesteal} %";
    }
}
