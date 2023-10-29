using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class BuffView : MonoBehaviour
{
    [SerializeField] protected BuffingObstacle Buff;
    [SerializeField] private TMP_Text _buffText;
    [SerializeField] private Image _attributeIcon;

    protected virtual void OnEnable()
    {
        Buff.HeroesBuffInitiated += OnHeroesBuffInitiated;
        Buff.Initialize();
    }

    protected virtual void OnDisable()
    {
        Buff.HeroesBuffInitiated -= OnHeroesBuffInitiated;
    }

    private void OnHeroesBuffInitiated(float buffValue, Sprite attributeSprite)
    {
        _buffText.text = buffValue.ToString();
        _attributeIcon.sprite = attributeSprite;
    }
}
