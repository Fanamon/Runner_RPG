using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamagingObstacle : Obstacle
{
    protected float TotalDamage;

    protected virtual void Die() { }

    protected virtual void StartCombat(ObstacleEntrySensor obstacleEntrySensor)
    {
        obstacleEntrySensor.CounterattackStarted += OnCounterattackStarted;
        obstacleEntrySensor.OnDamagingObstacleEntered(TotalDamage);
    }

    protected override void ImplementConsequences(ObstacleEntrySensor obstacleEntrySensor)
    {
        StartCombat(obstacleEntrySensor);
    }

    protected override void DisableObstacle()
    {
        this.enabled = false;
    }

    private void OnCounterattackStarted(float damage, ObstacleEntrySensor sensor)
    {
        TakeDamage(damage);
        sensor.CounterattackStarted -= OnCounterattackStarted;
        DisableObstacle();
    }

    protected abstract void TakeDamage(float damage);
}
