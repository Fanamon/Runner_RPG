using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Hero : MonoBehaviour
{
    [SerializeField] protected float Damage;
    [SerializeField] private float _health;
    [SerializeField] private float _armor;
    [SerializeField] private float _combatInitiative;
    [SerializeField] private Animator _animator;

    private CameraViewObserver _cameraViewObserver;
    private float _currentHealth;

    public event UnityAction<float, float> HealthChanged;
    public event UnityAction<float> ArmorChanged;
    public event UnityAction<Hero> Disabled;

    public float MeleeDamage { get; protected set; }
    public float CombatInitiative => _combatInitiative;

    private void Awake()
    {
        _currentHealth = _health;
        HealthChanged?.Invoke(_currentHealth, _health);
    }

    private void OnEnable()
    {
        _animator.Play("Run");
    }

    private void Update()
    {
        TryDisableObjectOutOfCameraView();
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= Mathf.Floor(damage * (1 - _armor / 100));

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;

            Die();
        }

        HealthChanged?.Invoke(_currentHealth, _health);
    }

    public void TakeArmorChange(float armorChangeValue)
    {
        _armor += armorChangeValue;
        ArmorChanged?.Invoke(_armor);
    }

    public void SetCameraViewObserver(CameraViewObserver cameraViewObserver)
    {
        _cameraViewObserver = cameraViewObserver;
    }

    public void Reset()
    {
        _currentHealth = _health;
    }

    private void Die()
    {
        Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit);
        transform.SetParent(hit.transform);
        _animator.Play("Die");
    }

    private void TryDisableObjectOutOfCameraView()
    {
        if (_cameraViewObserver.IsPositionLowerDisablePointZ(transform.position.z))
        {
            Disabled?.Invoke(this);
            gameObject.SetActive(false);
        }
    }
}
