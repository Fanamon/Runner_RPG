using System.Collections;
using UnityEngine;

public class FireballOfHero : CastingShootingObject
{
    [SerializeField] private float _existanceDuration;

    private Coroutine _existanceDurationCounter;

    private void OnEnable()
    {
        _existanceDurationCounter = StartCoroutine(CountExistanceDuration());
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<DamagingObstacle>(out DamagingObstacle damagingObstacle))
        {
            damagingObstacle.TakeDamage(Damage);
        }

        StopCoroutine(_existanceDurationCounter);
        gameObject.SetActive(false);
    }

    protected override void OnCollisionEnter(Collision collision) { }

    protected override void Update()
    {
        Move(Vector3.forward);
    }

    private IEnumerator CountExistanceDuration()
    {
        yield return new WaitForSeconds(_existanceDuration);

        gameObject.SetActive(false);
    }
}
