using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Archer : Hero
{
    [SerializeField] private float _meleeDamage;

    private void Start()
    {
        MeleeDamage = _meleeDamage;
    }
}
