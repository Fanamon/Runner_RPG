using UnityEngine;
using UnityEngine.Events;

public abstract class BossObstacle : DamagingObstacle
{
    protected Boss BossObject;

    public event UnityAction BossKilled;

    protected override void Awake()
    {
        BossObject = ObstacleObject as Boss;
    }

    protected override void Die()
    {
        BossKilled?.Invoke();
    }
}
