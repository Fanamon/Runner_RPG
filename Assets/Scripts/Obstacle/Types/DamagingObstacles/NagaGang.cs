using System.Linq;
using UnityEngine;

public class NagaGang : DamagingObstacle
{
    [SerializeField] private Naga[] _nagas;

    public void InitializeNagas()
    {
        EnemyObject.DamageChanged += OnDamageChanged;

        foreach (var naga in _nagas)
        {
            naga.enabled = true;
            naga.SetNagaParameters(EnemyObject);
        }
    }

    public override void TakeDamage(float damage)
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

    private void OnDisable()
    {
        EnemyObject.DamageChanged -= OnDamageChanged;

        _nagas.Where(naga => naga.enabled).ToList().ForEach(naga => naga.RoundHealth());
    }

    private void OnDamageChanged(float damage)
    {
        TotalDamage = damage * _nagas.Length;
    }
}
