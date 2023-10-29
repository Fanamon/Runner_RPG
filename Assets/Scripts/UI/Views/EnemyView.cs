using TMPro;
using UnityEngine;

public abstract class EnemyView : MonoBehaviour
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] protected TMP_Text _healthText;
    [SerializeField] private TMP_Text _damageText;
    [SerializeField] private TMP_Text _armorText;
    [SerializeField] private TMP_Text _lifeStealFromDamagePercent;
    [SerializeField] private Enemy _enemy;

    private void Awake()
    {
        _title.text = _enemy.Title;
    }

    protected virtual void OnEnable()
    {
        _enemy.DamageChanged += OnDamageChanged;
        _enemy.ArmorChanged += OnArmorChanged;
        _enemy.LifeStealChanged += OnLifeStealChanged;
        _enemy.Initialize();
    }

    protected virtual void OnDisable()
    {
        _enemy.DamageChanged -= OnDamageChanged;
        _enemy.ArmorChanged -= OnArmorChanged;
        _enemy.LifeStealChanged -= OnLifeStealChanged;
    }

    protected abstract void OnHealthChanged(float currentHealth);

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
