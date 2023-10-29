using System.Collections;
using UnityEngine;

public class Mage : Hero
{
    [SerializeField] private float _spellCastDelay;
    [SerializeField] private Transform _castPlacement;

    private CastingShootingObjectPool _fireballPool;
    private Coroutine _spellCaster = null;

    protected override void OnEnable()
    {
        base.OnEnable();
        _spellCaster = StartCoroutine(CastSpell());
    }

    protected override void Update()
    {
        base.Update();

        if (_spellCaster == null && enabled == true)
        {
            _spellCaster = StartCoroutine(CastSpell());
        }
    }

    public void SetFireballPool(CastingShootingObjectPool pool)
    {
        _fireballPool = pool;
    }

    private IEnumerator CastSpell(float speedModificator = 1)
    {
        yield return new WaitForSeconds(_spellCastDelay * speedModificator);

        CastingShootingObject fireball = _fireballPool.GetObjectToCastOrShoot();
        fireball.transform.position = _castPlacement.transform.position;
        fireball.gameObject.SetActive(true);

        _spellCaster = null;
    }
}
