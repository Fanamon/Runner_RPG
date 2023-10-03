using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HeroesPool : MonoBehaviour
{
    [SerializeField] private GameObject[] _heroesPrefabs;
    [SerializeField] private Transform _startHeroPosition;
    [SerializeField] private Transform _container;

    public void Initialize()
    {
        for (int i = 0; i < _heroesPrefabs.Length; i++)
        {
            Instantiate(_heroesPrefabs[i], _container);
        }
    }
}
