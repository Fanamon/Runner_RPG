using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New boss", menuName = "Enemy/Create new boss", order = 51)]
public class Boss : Enemy
{
    public event UnityAction<float, float> HealthChanged;

    public override void Initialize()
    {
        base.Initialize();
        HealthChanged?.Invoke(CurrentUnitHealth, MaxUnitHealth);
    }

    public void TakeDamage(float damage)
    {
        CurrentUnitHealth -= damage;
        HealthChanged?.Invoke(CurrentUnitHealth, MaxUnitHealth);
    }
}
