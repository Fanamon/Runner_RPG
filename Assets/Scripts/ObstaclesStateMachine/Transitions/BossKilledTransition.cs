
using UnityEngine;

public class BossKilledTransition : Transition
{
    private BossObstacle[] _bossObstacles;

    protected override void OnEnable()
    {
        _bossObstacles = PlatformsContainer.GetComponentsInChildren<BossObstacle>(true);

        foreach (var bossObstacle in _bossObstacles)
        {
            bossObstacle.BossKilled += OnBossKilled;
        }
    }

    private void OnDisable()
    {
        foreach (var bossObstacle in _bossObstacles)
        {
            bossObstacle.BossKilled -= OnBossKilled;
        }
    }

    private void OnBossKilled()
    {
        EndState();
    }
}
