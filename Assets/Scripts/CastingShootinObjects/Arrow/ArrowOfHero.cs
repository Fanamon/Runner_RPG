using UnityEngine;

public class ArrowOfHero : CastingShootingObject
{
    [SerializeField] private float _existanceDuration;

    private float _existanceTimeCount = 0;

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<DamagingObstacle>(out DamagingObstacle damagingObstacle))
        {
            damagingObstacle.TakeDamage(Damage);
        }

        _existanceTimeCount = 0;
        gameObject.SetActive(false);
    }

    protected override void OnCollisionEnter(Collision collision) { }

    protected override void Update()
    {
        Move(Vector3.forward);
        _existanceTimeCount += Time.deltaTime;

        if (_existanceTimeCount >= _existanceDuration)
        {
            _existanceTimeCount = 0;
            gameObject.SetActive(false);
        }
    }
}
