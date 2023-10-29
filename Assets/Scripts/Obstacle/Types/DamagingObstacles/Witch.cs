using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Witch : DamagingObstacle
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _castPlacement;

    private float _currentHealth;
    private float _spellCastDelay;
    private Coroutine _spellCaster = null;
    private CastingShootingObjectPool _fireballPool;

    public event UnityAction<float> HealthChanged;

    protected override void Awake()
    {
        base.Awake();
        _spellCastDelay = GetAnimatorClipLength("Idle");
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        EnemyObject.MaxHealthChanged += OnMaxHealthChanged;
        TotalDamage = EnemyObject.MeleeUnitDamage;
        _spellCaster = StartCoroutine(CastSpell(0.5f));
    }

    private void OnDisable()
    {
        EnemyObject.MaxHealthChanged -= OnMaxHealthChanged;
    }

    private void Update()
    {
        if (_spellCaster == null)
        {
            _spellCaster = StartCoroutine(CastSpell());
        }
    }

    public void InitializeHealth()
    {
        _currentHealth = EnemyObject.UnitHealth;
        HealthChanged?.Invoke(_currentHealth);
    }

    public void SetFireballPool(CastingShootingObjectPool pool)
    {
        _fireballPool = pool;
    }

    public override void TakeDamage(float damage)
    {
        StopCoroutine(_spellCaster);
        _currentHealth -= Mathf.Floor(damage * (1 - EnemyObject.UnitArmor / 100));

        if (_currentHealth <= 0)
        {
            Die();
        }
        else
        {
            _animator.Play("Attack");
        }

        HealthChanged?.Invoke(_currentHealth);
    }

    protected override void Die()
    {
        _currentHealth = 0;
        _animator.Play("Die");
        DisableObstacle();
    }

    protected override void StartCombat(ObstacleEntrySensor obstacleEntrySensor)
    {
        base.StartCombat(obstacleEntrySensor);
        HealthChanged?.Invoke(_currentHealth);
    }

    private float GetAnimatorClipLength(string clipName)
    {
        AnimationClip[] clips = _animator.runtimeAnimatorController.animationClips;

        return clips.First(clip => clip.name == clipName).length;
    }

    private IEnumerator CastSpell(float speedModificator = 1)
    {
        yield return new WaitForSeconds(_spellCastDelay * speedModificator);

        CastingShootingObject fireball = _fireballPool.GetObjectToCastOrShoot();
        fireball.transform.position = _castPlacement.transform.position;
        fireball.gameObject.SetActive(true);

        _spellCaster = null;
    }

    private void OnMaxHealthChanged(float maxHealth)
    {
        _currentHealth = maxHealth;
        HealthChanged?.Invoke(_currentHealth);
    }
}
