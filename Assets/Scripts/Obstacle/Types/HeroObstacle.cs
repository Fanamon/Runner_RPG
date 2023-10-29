using UnityEngine;
using UnityEngine.Events;

public class HeroObstacle : Obstacle
{
    private static bool _isAlreadyExist;

    private Hero _hero;
    private HeroesPool _heroesPool;

    public event UnityAction HeroJoined;

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
        CircleUpParticle.gameObject.SetActive(true);
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

    protected override void DisableObstacle()
    {
        base.DisableObstacle();
        gameObject.SetActive(false);
    }

    protected override void ImplementConsequences(ObstacleEntrySensor obstacleEntrySensor)
    {
        _hero.gameObject.SetActive(false);
        obstacleEntrySensor.OnHeroObstacleEntered(_hero);
        HeroJoined?.Invoke();
        DisableObstacle();
    }
}
