using System.Collections;
using UnityEngine;

public class Archer : Hero
{
    [SerializeField] private float _shootingDelay;
    [SerializeField] private Transform _arrowShootPlacement;

    private CastingShootingObjectPool _arrowPool;
    private Coroutine _shooter = null;

    protected override void OnEnable()
    {
        base.OnEnable();
        _shooter = StartCoroutine(Shoot());
    }

    protected override void Update()
    {
        base.Update();

        if (_shooter == null && enabled == true)
        {
            _shooter = StartCoroutine(Shoot());
        }
    }

    public void SetArrowPool(CastingShootingObjectPool pool)
    {
        _arrowPool = pool;
    }

    private IEnumerator Shoot(float speedModificator = 1)
    {
        yield return new WaitForSeconds(_shootingDelay * speedModificator);

        CastingShootingObject fireball = _arrowPool.GetObjectToCastOrShoot();
        fireball.transform.position = _arrowShootPlacement.transform.position;
        fireball.gameObject.SetActive(true);

        _shooter = null;
    }
}
