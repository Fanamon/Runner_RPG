using UnityEngine;

public class FireballOfEnemy : CastingShootingObject
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Obstacle>(out Obstacle obstacle))
        {
            gameObject.SetActive(false);
        }
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Hero>(out Hero hero))
        {
            hero.TakeDamage(Damage);
        }

        gameObject.SetActive(false);
    }

    protected override void Update()
    {
        Move(Vector3.back);
    }
}
