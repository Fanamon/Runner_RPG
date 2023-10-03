using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HeroObstacle : Obstacle
{
    private static bool _isAlreadyExist;

    private Hero _hero;
    private HeroesPool _heroesPool;

    public event UnityAction<GameObject> ObstacleDestroyed;

    static HeroObstacle()
    {
        _isAlreadyExist = false;
    }

    public static bool IsAlreadyExist => _isAlreadyExist;

    private void OnEnable()
    {
        Hero[] heroes = _heroesPool.GetComponentsInChildren<Hero>(true);
        int randomNumber = Random.Range(0, heroes.Length);

        _isAlreadyExist = true;
        _hero = heroes[randomNumber];
        _hero.transform.SetParent(transform);
        _hero.transform.position = transform.position;
        _hero.transform.rotation = transform.rotation;
        _hero.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        _isAlreadyExist = false;
    }

    public void ReturnHeroToPool()
    {
        _hero.gameObject.SetActive(false);
        _hero.transform.SetParent(_heroesPool.transform);
        _hero.transform.position = _heroesPool.transform.position;
        _hero.transform.rotation = _heroesPool.transform.rotation;
    }

    public void SetHeroesPool(HeroesPool heroesPool)
    {
        _heroesPool = heroesPool;
    }

    protected override void ImplementConsequences(ObstacleEntrySensor obstacleEntrySensor)
    {
        obstacleEntrySensor.OnHeroObstacleEntered(_hero);
        DisableObstacle();
    }

    protected override void DisableObstacle()
    {
        ObstacleDestroyed?.Invoke(gameObject);
        Destroy(gameObject);
    }
}
