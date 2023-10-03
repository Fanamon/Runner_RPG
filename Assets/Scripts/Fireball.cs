using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _speed;

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (TryGetComponent<Hero>(out Hero hero))
        {
            hero.TakeDamage(_damage);
        }

        gameObject.SetActive(false);
    }

    private void Update()
    {
        transform.Translate(Vector3.back * _speed * Time.deltaTime, Space.World);
    }
}
