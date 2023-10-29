using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Attributes;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public class BuffButton : MonoBehaviour
{
    [SerializeField] private Color _healthBuffColor;
    [SerializeField] private Color _damageBuffColor;
    [SerializeField] private Color _armorBuffColor;

    private int _heroesAttributeNumber = 0;
    private float _heroesBuffValue = 0;
    private Color _currentColor = Color.white;

    private Hero _hero;
    private Button _button;
    private Image _image;

    public event UnityAction BuffButtonClicked;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();
    }

    public void Initialize(Hero hero)
    {
        _hero = hero;
    }

    public void InitializeBuff(float buffValue, int attributeNumber)
    {
        _heroesAttributeNumber = attributeNumber;
        _heroesBuffValue = buffValue;

        switch (_heroesAttributeNumber)
        {
            case (int)Attribute.Health:
                _currentColor = _healthBuffColor;
                break;

            case (int)Attribute.Damage:
                _currentColor = _damageBuffColor;
                break;

            case (int)Attribute.Armor:
                _currentColor = _armorBuffColor;
                break;
        }

        if (transform.parent.gameObject.activeSelf)
        {
            _image.color = _currentColor;
        }
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
        _image.color = _currentColor;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        switch (_heroesAttributeNumber)
        {
            case (int)Attribute.Health:
                _hero.TakeHealthChange(_heroesBuffValue);
                break;

            case (int)Attribute.Damage:
                _hero.TakeDamageChange(_heroesBuffValue);
                break;

            case (int)Attribute.Armor:
                _hero.TakeArmorChange(_heroesBuffValue);
                break;
        }

        BuffButtonClicked?.Invoke();
    }
}
