using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New enemy", menuName = "Enemy/Create new enemy", order = 51)]
public class Enemy : ObstacleObject
{
    [SerializeField] protected float MaxUnitHealth;
    [SerializeField] private float _meleeUnitDamage;
    [SerializeField] private float _unitArmor;
    [SerializeField] private float _lifeStealFromDamagePercent;

    protected float CurrentUnitHealth;
    private float _currentMeleeUnitDamage;
    private float _currentUnitArmor;
    private float _currrentLifeStealFromDamagePercent;

    public event UnityAction<float> MaxHealthChanged;
    public event UnityAction<float> DamageChanged;
    public event UnityAction<float> ArmorChanged;
    public event UnityAction<float> LifeStealChanged;

    public float UnitHealth => CurrentUnitHealth;
    public float MeleeUnitDamage => _currentMeleeUnitDamage;
    public float UnitArmor => _currentUnitArmor;
    public float LifeStealFromDamagePercent => _currrentLifeStealFromDamagePercent;

    private void OnEnable()
    {
        CurrentUnitHealth = MaxUnitHealth;
        _currentMeleeUnitDamage = _meleeUnitDamage;
        _currentUnitArmor = _unitArmor;
        _currrentLifeStealFromDamagePercent = _lifeStealFromDamagePercent;
    }

    public virtual void Initialize()
    {
        MaxHealthChanged?.Invoke(CurrentUnitHealth);
        DamageChanged?.Invoke(_currentMeleeUnitDamage);
        ArmorChanged?.Invoke(_currentUnitArmor);
        LifeStealChanged?.Invoke(_currrentLifeStealFromDamagePercent);
    }

    public void IncreaseHealth(float increaseValue)
    {
        CurrentUnitHealth += increaseValue;
        MaxHealthChanged?.Invoke(CurrentUnitHealth);
    }

    public void IncreaseDamage(float increaseValue)
    {
        _currentMeleeUnitDamage += increaseValue;
        DamageChanged?.Invoke(_currentMeleeUnitDamage);
    }

    public void IncreaseArmor(float increaseValue)
    {
        _currentUnitArmor += increaseValue;
        ArmorChanged?.Invoke(_currentUnitArmor);
    }

    public void IncreaseLifeSteal(float increaseValue)
    {
        _currrentLifeStealFromDamagePercent += increaseValue;
        LifeStealChanged?.Invoke(_currrentLifeStealFromDamagePercent);
    }
}
