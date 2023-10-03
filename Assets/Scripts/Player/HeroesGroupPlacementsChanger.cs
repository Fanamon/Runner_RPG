using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(HeroesGroup))]
public class HeroesGroupPlacementsChanger : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _deadHeroes;
    [SerializeField] private Transform[] _heroPlacements;

    private bool _isHeroesOnTheirPlacements = true;
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
        Transform freePlacement = _heroPlacements
            .First(placement => placement.GetComponentInChildren<Hero>() == null);

        hero.transform.SetParent(freePlacement);
        hero.transform.position = freePlacement.position;
        hero.transform.rotation = Quaternion.Euler(Vector3.zero);

        SetHeroesToPlacementsByCombatInitiative();

        hero.GetComponentInChildren<ObstacleEntrySensor>(true).gameObject.SetActive(true);
        hero.enabled = true;
        hero.gameObject.SetActive(true);
        _isHeroesOnTheirPlacements = false;
    }

    private void OnHeroKilled(Hero hero)
    {
        hero.transform.SetParent(_deadHeroes);
        hero.transform.position = _deadHeroes.position;

        SetHeroesToPlacementsByCombatInitiative();

        _isHeroesOnTheirPlacements = false;
    }

    private void SetHeroesToPlacementsByCombatInitiative()
    {
        Hero[] heroesInGroup = _heroPlacements.Where(placement => placement.GetComponentInChildren<Hero>() != null)
                    .Select(placement => placement.GetComponentInChildren<Hero>())
                    .OrderByDescending(hero => hero.CombatInitiative).ToArray();

        for (int i = 0; i < heroesInGroup.Length; i++)
        {
            heroesInGroup[i].transform.SetParent(_heroPlacements[i]);
        }
    }

    private void TryMoveHeroesToPlacements()
    {
        if (_isHeroesOnTheirPlacements == false)
        {
            _isHeroesOnTheirPlacements = true;

            _heroPlacements.Where(placement => placement.GetComponentInChildren<Hero>() != null)
                .ToList().ForEach(placement =>
                {
                    Hero hero = placement.GetComponentInChildren<Hero>();

                    if (hero.transform.position != placement.position)
                    {
                        hero.transform.position = Vector3.MoveTowards(hero.transform.position, placement.position, _speed * Time.deltaTime);
                        _isHeroesOnTheirPlacements = false;
                    }
                });
        }
    }
}
