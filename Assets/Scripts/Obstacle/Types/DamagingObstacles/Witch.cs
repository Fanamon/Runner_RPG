using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Witch : DamagingObstacle
{
    [SerializeField] private float _health;
    [SerializeField] private float _meleeDamage;
    [SerializeField] private float _armor;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _castPlacement;

    private float _currentHealth;
    private float _spellCastSpeed;
    private Coroutine _spellCaster = null;
    private FireballPool _fireballPool;

    public event UnityAction<float> HealthChanged;

    private void OnEnable()
    {
        _currentHealth = _health;
        TotalDamage = _meleeDamage;
        HealthChanged?.Invoke(_currentHealth);
        _spellCastSpeed = GetAnimatorClipLength("Idle");
        _spellCaster = StartCoroutine(CastSpell(0.5f));
    }

    private void Update()
    {
        if (_spellCaster == null)
        {
            _spellCaster = StartCoroutine(CastSpell());
        }
    }

    public void SetFireballPool(FireballPool pool)
    {
        _fireballPool = pool;
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

    protected override void TakeDamage(float damage)
    {
        StopCoroutine(_spellCaster);
        _currentHealth -= Mathf.Floor(damage * (1 - _armor / 100));

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

    private float GetAnimatorClipLength(string clipName)
    {
        AnimationClip[] clips = _animator.runtimeAnimatorController.animationClips;

        return clips.First(clip => clip.name == clipName).length;
    }

    private IEnumerator CastSpell(float speedModificator = 1)
    {
        yield return new WaitForSeconds(_spellCastSpeed * speedModificator);

        Fireball fireball = _fireballPool.GetFireball();
        fireball.transform.position = _castPlacement.transform.position;
        fireball.gameObject.SetActive(true);

        _spellCaster = null;
    }
}
