using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CastingShootingObjectPool : MonoBehaviour
{
    [SerializeField] private CastingShootingObject _prefab;
    [SerializeField] private int _poolCount;
    [SerializeField] private Transform _poolContainer;

    private List<CastingShootingObject> _pool;

    private void Awake()
    {
        GeneratePool();
    }

    public CastingShootingObject GetObjectToCastOrShoot()
    {
        return _pool.FirstOrDefault(fireball => fireball.gameObject.activeSelf == false);
    }

    private void GeneratePool()
    {
        _pool = new List<CastingShootingObject>();

        for (int i = 0; i < _poolCount; i++)
        {
            _pool.Add(Instantiate(_prefab, _poolContainer));
        }
    }
}
