using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroesEnemiesBuffView : BuffView
{
    [SerializeField] private TMP_Text _enemyTitle;
    [SerializeField] private TMP_Text _enemyBuffText;
    [SerializeField] private Image _enemyAttributeIcon;

    protected override void OnEnable()
    {
        (Buff as HeroesEnemiesBuff).EnemyBuffInitiated += OnEnemyBuffInitiated;
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        (Buff as HeroesEnemiesBuff).EnemyBuffInitiated -= OnEnemyBuffInitiated;
    }

    private void OnEnemyBuffInitiated(string enemyTitle, float buffValue, Sprite attributeSprite)
    {
        _enemyTitle.text = enemyTitle;
        _enemyBuffText.text = buffValue.ToString();
        _enemyAttributeIcon.sprite = attributeSprite;
    }
}
