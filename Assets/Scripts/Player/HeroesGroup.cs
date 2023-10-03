using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class HeroesGroup : MonoBehaviour
{
    [SerializeField] private CameraViewObserver _cameraViewObserver;
    [SerializeField] private HeroesPool _heroesPool;

    private int _startHeroNumber = 0;

    private Hero[] _heroes;
    private ObstacleEntrySensor[] _sensors;

    public event UnityAction<Hero> HeroJoined;
    public event UnityAction<Hero> HeroKilled;

    public void Initialize()
    {
        _heroes = _heroesPool.GetComponentsInChildren<Hero>(true);
        _sensors = _heroesPool.GetComponentsInChildren<ObstacleEntrySensor>(true);

        foreach (var hero in _heroes)
        {
            hero.SetCameraViewObserver(_cameraViewObserver);
            hero.Disabled += OnHeroDisabled;
        }

        foreach (var sensor in _sensors)
        {
            sensor.HeroesGroupCombatStarted += OnHeroesGroupCombatStarted;
            sensor.HeroInvited += OnHeroInvited;
        }

        HeroJoined?.Invoke(GetComponentsInChildren<Hero>(true)[_startHeroNumber]);
    }

    private void OnDisable()
    {
        foreach (var hero in _heroes)
        {
            hero.Disabled -= OnHeroDisabled;
        }

        foreach (var sensor in _sensors)
        {
            sensor.HeroesGroupCombatStarted -= OnHeroesGroupCombatStarted;
            sensor.HeroInvited -= OnHeroInvited;
        }
    }

    private void OnHeroDisabled(Hero hero)
    {
        hero.Reset();
        HeroKilled?.Invoke(hero);
    }

    private void OnHeroInvited(Hero hero)
    {
        HeroJoined?.Invoke(hero);
    }

    private void OnHeroesGroupCombatStarted(float damage)
    {
        Hero takingDamageHero = null;
        Hero[] availableHeroes = _heroes.Where(hero => hero.gameObject.activeSelf == true)
            .OrderByDescending(hero => hero.CombatInitiative).ToArray();

        float combatInitiativeIteratedSum = 0;
        float combatInitiativeRandomValue = GetCombatInitiativeRandomValue(availableHeroes);

        for (int i = 0; i < availableHeroes.Length; i++)
        {
            combatInitiativeIteratedSum += availableHeroes[i].CombatInitiative;

            if (combatInitiativeRandomValue < combatInitiativeIteratedSum)
            {
                takingDamageHero = availableHeroes[i];
                break;
            }
        }

        takingDamageHero.TakeDamage(damage);
        Counterattack(availableHeroes);
    }

    private void Counterattack(Hero[] heroes)
    {
        float damage = GetTotalHeroesDamage(heroes);

        foreach (var sensor in _sensors)
        {
            sensor.OnCounterattacked(damage);
        }
    }

    private float GetCombatInitiativeRandomValue(Hero[] heroes)
    {
        float totalCombatInitiative = 0;

        foreach (var hero in heroes)
        {
            totalCombatInitiative += hero.CombatInitiative;
        }

        return Random.Range(0, totalCombatInitiative);
    }

    private float GetTotalHeroesDamage(Hero[] heroes)
    {
        return heroes.Where(hero => hero.gameObject.activeSelf == true).Select(hero => hero.MeleeDamage).Sum();
    }
}
