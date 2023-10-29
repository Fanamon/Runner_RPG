using TMPro;
using UnityEngine;

public class HeroView : MonoBehaviour
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private TMP_Text _damageText;
    [SerializeField] private TMP_Text _armorText;

    private Hero _hero;

    public string HeroTitle => _title.text;

    public void Initialize(Hero hero)
    {
        _hero = hero;
        _title.text = _hero.Title;
        _hero.HealthChanged += OnHealthChanged;
        _hero.DamageChanged += OnDamageChanged;
        _hero.ArmorChanged += OnArmorChanged;
    }

    private void OnDisable()
    {
        _hero.HealthChanged -= OnHealthChanged;
        _hero.DamageChanged -= OnDamageChanged;
        _hero.ArmorChanged -= OnArmorChanged;
    }

    private void OnHealthChanged(float currentHealth, float health)
    {
        _healthText.text = $"{currentHealth} / {health}";
    }

    private void OnDamageChanged(float damage)
    {
        _damageText.text = damage.ToString();
    }

    private void OnArmorChanged(float armor)
    {
        _armorText.text = armor.ToString();
    }
}
