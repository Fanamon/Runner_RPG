using UnityEngine;

[RequireComponent(typeof(Animator))]
public class GhoulBoss : BossObstacle
{
    private static bool _isAlreadyExist;

    private Animator _animator;

    static GhoulBoss()
    {
        _isAlreadyExist = false;
    }

    public static bool IsAlreadyExist => _isAlreadyExist;

    protected override void OnEnable()
    {
        base.OnEnable();
        _isAlreadyExist = true;
        TotalDamage = BossObject.MeleeUnitDamage;
        _animator = GetComponent<Animator>();
    }

    public override void TakeDamage(float damage)
    {
        BossObject.TakeDamage(Mathf.Floor(damage * (1 - BossObject.UnitArmor / 100)));

        if (BossObject.UnitHealth <= 0)
        {
            Die();
        }
        else
        {
            _animator.SetTrigger(AnimatorData.ObstacleParameters.Attack);
        }
    }

    protected override void DisableObstacle()
    {
        _isAlreadyExist = false;
        base.DisableObstacle();
    }

    protected override void Die()
    {
        BossObject.TakeDamage(BossObject.UnitHealth);
        _animator.SetTrigger(AnimatorData.ObstacleParameters.Die);
        base.Die();
        DisableObstacle();
    }

    protected override void StartCombat(ObstacleEntrySensor obstacleEntrySensor)
    {
        BossObject.TakeDamage(-Mathf.Ceil(TotalDamage * BossObject.LifeStealFromDamagePercent / 100));
        base.StartCombat(obstacleEntrySensor);
    }
}
