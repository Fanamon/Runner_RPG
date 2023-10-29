using UnityEngine;
using System.Linq;
using System.Collections.Generic;

[RequireComponent(typeof(HeroesGroup))]
public class HeroesGroupPlacementsChanger : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _deadHeroes;
    [SerializeField] private Transform[] _heroPlacements;
    [SerializeField] private HeroViewPool _heroViewPool;

    private HeroesGroup _heroesGroup;

    public void Initialize()
    {
        _heroesGroup = GetComponent<HeroesGroup>();
        _heroesGroup.HeroJoined += OnHeroJoined;
        _heroesGroup.HeroKilled += OnHeroKilled;
    }

    private void OnDisable()
    {
        _heroesGroup.HeroJoined += OnHeroJoined;
        _heroesGroup.HeroKilled -= OnHeroKilled;
    }

    private void Update()
    {
        TryMoveHeroesToPlacements();
    }

    private void OnHeroJoined(Hero hero)
    {
        _heroViewPool.EnableHeroView(hero);
        Transform freePlacement = _heroPlacements
            .First(placement => placement.GetComponentInChildren<Hero>() == null);

        hero.transform.SetParent(freePlacement);
        SetHeroesToPlacementsByCombatInitiative();
        hero.transform.position = hero.transform.parent.position;
        hero.transform.rotation = Quaternion.Euler(Vector3.zero);
        hero.GetComponentInChildren<ObstacleEntrySensor>(true).gameObject.SetActive(true);
        hero.enabled = true;
        hero.gameObject.SetActive(true);
    }

    private void OnHeroKilled(Hero hero)
    {
        _heroViewPool.DisableHeroView(hero);
        hero.transform.SetParent(_deadHeroes);
        hero.transform.position = _deadHeroes.position;

        SetHeroesToPlacementsByCombatInitiative();
    }

    private void SetHeroesToPlacementsByCombatInitiative()
    {
        Hero[] heroesInGroup = _heroPlacements.Where(placement => placement.GetComponentInChildren<Hero>(true) != null)
                    .Select(placement => placement.GetComponentInChildren<Hero>(true))
                    .OrderByDescending(hero => hero.CombatInitiative).ToArray();

        for (int i = 0; i < heroesInGroup.Length; i++)
        {
            heroesInGroup[i].transform.SetParent(_heroPlacements[i]);
        }
    }

    private void TryMoveHeroesToPlacements()
    {
        if (TryFindUnplacementHeroes(out List<Hero> heroes))
        {
            heroes.ForEach(hero =>
            {
                hero.transform.position = Vector3.MoveTowards(hero.transform.position,
                hero.transform.parent.position, _speed * Time.deltaTime);
            });
        }
    }

    private bool TryFindUnplacementHeroes(out List<Hero> heroes)
    {
        heroes = _heroPlacements.Where(placement => placement.GetComponentInChildren<Hero>(true) != null).ToList()
            .Where(placement => placement.GetComponentInChildren<Hero>(true).transform.position != placement.position)
            .Select(placement => placement.GetComponentInChildren<Hero>(true)).ToList();

        return heroes != null;
    }
}
