using UnityEngine;

public class ObstaclesNumberGatheredTransition : Transition
{
    [SerializeField] private int _obstacleNumberToGather;

    private int _obstacleGatheredCount = 0;
    private Obstacle[] _obstacles;

    protected override void OnEnable()
    {
        _obstacles = PlatformsContainer.GetComponentsInChildren<Obstacle>(true);

        foreach (var obstacle in _obstacles)
        {
            obstacle.ObstacleGathered += OnObstacleGathered;
        }
    }

    private void OnDisable()
    {
        foreach (var obstacle in _obstacles)
        {
            obstacle.ObstacleGathered -= OnObstacleGathered;
        }
    }

    private void OnObstacleGathered()
    {
        _obstacleGatheredCount++;

        if (_obstacleGatheredCount == _obstacleNumberToGather)
        {
            EndState();
        }
    }
}
