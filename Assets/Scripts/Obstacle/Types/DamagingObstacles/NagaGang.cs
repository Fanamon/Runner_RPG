using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NagaGang : DamagingObstacle
{
    [SerializeField] private float _nagaStartHealth;
    [SerializeField] private float _nagaStartDamage;
    [SerializeField] private float _nagaStartArmor;
    [SerializeField] private Naga[] _nagas;

    private void OnEnable()
    {
        ResetNagas();
    }

    private void OnDisable()
    {
        _nagas.Where(naga => naga.enabled).ToList().ForEach(naga => naga.RoundHealth());
    }

    protected override void TakeDamage(float damage)
    {
        float damagePoint = 1;

        for (int i = 0; i < damage; i++)
        {
            if (_nagas.FirstOrDefault(naga => naga.enabled) == null)
            {
                DisableObstacle();
                break;
            }

            _nagas.First(naga => naga.enabled).TakeDamage(damagePoint);
        }
    }

    private void ResetNagas()
    {
        TotalDamage = 0;

        foreach (var naga in _nagas)
        {
            naga.enabled = true;
            naga.SetNagaParameters(_nagaStartHealth, _nagaStartDamage, _nagaStartArmor);
            TotalDamage += naga.Damage;
        }
    }
}
