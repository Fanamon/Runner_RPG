using UnityEngine;

public abstract class CastingShootingObject : MonoBehaviour
{
    [SerializeField] protected float Damage;
    [SerializeField] private float _speed;

    protected abstract void OnTriggerEnter(Collider other);

    protected abstract void OnCollisionEnter(Collision collision);

    protected abstract void Update();

    protected void Move(Vector3 direction)
    {
        transform.Translate(direction * _speed * Time.deltaTime, Space.World);
    }
}
