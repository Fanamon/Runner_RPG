using UnityEngine;
using UnityEngine.Events;

public class HeroBuff : BuffingObstacle
{
    public event UnityAction<float, int> HeroesBuffEntered;

    protected override void ImplementConsequences(ObstacleEntrySensor obstacleEntrySensor)
    {
        HeroesBuffEntered?.Invoke(HeroesBuffValue, HeroesAttributeNumber);
        DisableObstacle();
    }
}
