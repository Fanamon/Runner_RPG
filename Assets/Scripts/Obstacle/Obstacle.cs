using UnityEngine;
using UnityEngine.Events;

public abstract class Obstacle : MonoBehaviour
{
    [SerializeField] protected ObstacleObject ObstacleObject;
    [SerializeField] protected ParticleSystem CircleUpParticle;

    public event UnityAction ObstacleGathered;

    public string Title => ObstacleObject.Title;

    protected void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ObstacleEntrySensor obstacleEntrySensor)  && enabled)
        {
            ImplementConsequences(obstacleEntrySensor);
            ObstacleGathered?.Invoke();
        }
    }

    protected virtual void DisableObstacle()
    {
        CircleUpParticle.gameObject.SetActive(false);
    }

    protected abstract void ImplementConsequences(ObstacleEntrySensor obstacleEntrySensor);
}
