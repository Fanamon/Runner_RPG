using UnityEngine;

public class AllHeroesJoinedTransition : Transition
{
    [SerializeField] private HeroesPool _heroesPool;

    private int _stateEndCount = 0;
    private int _heroObstacleRequireCount;
    private HeroObstacle[] _heroObstacles;

    protected override void OnEnable()
    {
        if (_heroObstacles != null)
        {
            foreach (var heroObstacle in _heroObstacles)
            {
                heroObstacle.HeroJoined += OnHeroJoined;
            }
        }
    }

    private void OnDisable()
    {
        foreach (var heroObstacle in _heroObstacles)
        {
            heroObstacle.HeroJoined -= OnHeroJoined;
        }
    }

    private void Start()
    {
        _heroObstacles = PlatformsContainer.GetComponentsInChildren<HeroObstacle>(true);
        _heroObstacleRequireCount = _heroesPool.GetHeroesCount() - 1;

        OnEnable();
    }

    private void OnHeroJoined()
    {
        _stateEndCount++;

        if (_stateEndCount == _heroObstacleRequireCount)
        {
            EndState();
        }
    }
}
