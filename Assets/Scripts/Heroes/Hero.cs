using UnityEngine;
using UnityEngine.Events;

public abstract class Hero : MonoBehaviour
{
    [SerializeField] private string _title;
    [SerializeField] private float _health;
    [SerializeField] private float _meleeDamage;
    [SerializeField] private float _armor;
    [SerializeField] private float _combatInitiative;
    [SerializeField] private Animator _animator;

    private CameraViewObserver _cameraViewObserver;
    private float _currentHealth;

    public event UnityAction<float, float> HealthChanged;
    public event UnityAction<float> DamageChanged;
    public event UnityAction<float> ArmorChanged;
    public event UnityAction<Hero> Disabled;
    public event UnityAction<Hero> Killed;

    public string Title => _title;
    public float MeleeDamage => _meleeDamage;
    public float CombatInitiative => _combatInitiative;

    private void Awake()
    {
        _currentHealth = _health;
    }

    protected virtual void OnEnable()
    {
        HealthChanged?.Invoke(_currentHealth, _health);
        DamageChanged?.Invoke(MeleeDamage);
        ArmorChanged?.Invoke(_armor);
        _animator.Play("Run");
    }

    protected virtual void Update()
    {
        TryDisableObjectOutOfCameraView();
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= Mathf.Floor(damage * (100 - _armor) / 100);

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;

            Die();
        }

        HealthChanged?.Invoke(_currentHealth, _health);
    }

    public void TakeHealthChange(float healthChangeValue)
    {
        _currentHealth += healthChangeValue;
        _health += healthChangeValue;
        HealthChanged?.Invoke(_currentHealth, _health);
    }

    public void TakeDamageChange(float damageChangeValue)
    {
        _meleeDamage += damageChangeValue;
        DamageChanged?.Invoke(_meleeDamage);
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
        Killed?.Invoke(this);
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
