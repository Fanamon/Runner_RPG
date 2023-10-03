using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FireballPool : MonoBehaviour
{
    [SerializeField] private Fireball _fireballPrefab;
    [SerializeField] private int _poolCount;
    [SerializeField] private Transform _poolContainer;

    private List<Fireball> _pool;

    private void Awake()
    {
        GeneratePool();
    }

    public Fireball GetFireball()
    {
        return _pool.FirstOrDefault(fireball => fireball.gameObject.activeSelf == false);
    }

    private void GeneratePool()
    {
        _pool = new List<Fireball>();

        for (int i = 0; i < _poolCount; i++)
        {
            _pool.Add(Instantiate(_fireballPrefab, _poolContainer));
        }
    }
}
