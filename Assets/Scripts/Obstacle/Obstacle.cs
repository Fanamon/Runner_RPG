using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    [SerializeField] private string _title;

    public string Title => _title;

    protected void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ObstacleEntrySensor obstacleEntrySensor))
        {
            ImplementConsequences(obstacleEntrySensor);
        }
    }

    protected abstract void ImplementConsequences(ObstacleEntrySensor obstacleEntrySensor);

    protected abstract void DisableObstacle();
}
