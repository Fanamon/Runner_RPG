using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossObstacle : Obstacle
{
    [SerializeField] private int _damage;

    protected override void ImplementConsequences(ObstacleEntrySensor obstacleEntrySensor)
    {
        
    }

    protected override void DisableObstacle()
    {

    }
}
