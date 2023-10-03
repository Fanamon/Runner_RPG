using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObstacleEntrySensor : MonoBehaviour
{
    public event UnityAction<float> HeroesGroupCombatStarted;
    public event UnityAction<float, ObstacleEntrySensor> CounterattackStarted;
    public event UnityAction<Hero> HeroInvited;

    public void OnDamagingObstacleEntered(float damage)
    {
        HeroesGroupCombatStarted?.Invoke(damage);
    }

    public void OnCounterattacked(float damage)
    {
        CounterattackStarted?.Invoke(damage, this);
    }

    public void OnHeroObstacleEntered(Hero hero)
    {
        HeroInvited?.Invoke(hero);
    }
}
