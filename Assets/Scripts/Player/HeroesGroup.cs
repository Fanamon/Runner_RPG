using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Attributes;
using System.Collections;

public class HeroesGroup : MonoBehaviour
{
    [SerializeField] private CameraViewObserver _cameraViewObserver;
    [SerializeField] private HeroesPool _heroesPool;
    [SerializeField] private FailureView _failureView;
    [SerializeField] private StartHeroConfig _startHeroConfig;

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
            hero.Killed += OnHeroKilled;
        }

        foreach (var sensor in _sensors)
        {
            sensor.HeroesGroupCombatStarted += OnHeroesGroupCombatStarted;
            sensor.HeroInvited += OnHeroInvited;
            sensor.HeroesGroupBuffInvoked += OnHeroesGroupBuffInvoked;
        }

        HeroJoined?.Invoke(GetComponentsInChildren<Hero>(true)[_startHeroConfig.StartHeroNumber]);
    }

    private void OnDisable()
    {
        foreach (var hero in _heroes)
        {
            hero.Disabled -= OnHeroDisabled;
            hero.Killed -= OnHeroKilled;
        }

        foreach (var sensor in _sensors)
        {
            sensor.HeroesGroupCombatStarted -= OnHeroesGroupCombatStarted;
            sensor.HeroInvited -= OnHeroInvited;
            sensor.HeroesGroupBuffInvoked -= OnHeroesGroupBuffInvoked;
        }
    }

    private void OnHeroDisabled(Hero hero)
    {
        hero.Reset();
        HeroKilled?.Invoke(hero);
    }

    private void OnHeroKilled(Hero killedHero)
    {
        Hero[] availableHeroes = _heroes.Where(hero => hero.enabled == true && hero.gameObject.activeSelf == true 
        && hero != killedHero)
            .OrderByDescending(hero => hero.CombatInitiative).ToArray();

        if (availableHeroes.Length == 0)
        {
            StartCoroutine(EnableFailureView());
        }
    }

    private void OnHeroInvited(Hero hero)
    {
        HeroJoined?.Invoke(hero);
    }

    private void OnHeroesGroupCombatStarted(float damage)
    {
        Hero takingDamageHero = null;
        Hero[] availableHeroes = _heroes.Where(hero => hero.enabled == true && hero.gameObject.activeSelf == true)
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

    private void OnHeroesGroupBuffInvoked(float buffValue, int attributeNumber)
    {
        foreach (var hero in _heroes)
        {
            switch (attributeNumber)
            {
                case (int)Attribute.Health:
                    hero.TakeHealthChange(buffValue);
                    break;

                case (int)Attribute.Damage:
                    hero.TakeDamageChange(buffValue);
                    break;

                case (int)Attribute.Armor:
                    hero.TakeArmorChange(buffValue);
                    break;
            }
        }
    }

    private void Counterattack(Hero[] heroes)
    {
        float damage = GetTotalHeroesDamage(heroes);

        foreach (var sensor in _sensors)
        {
            sensor.OnCounterattacked(damage);
        }
    }

    private IEnumerator EnableFailureView()
    {
        float delay = 3;

        yield return new WaitForSeconds(delay);

        Time.timeScale = 0;
        _failureView.gameObject.SetActive(true);
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
        return heroes.Where(hero => hero.gameObject.activeSelf == true && hero.enabled == true)
            .Select(hero => hero.MeleeDamage).Sum();
    }
}
