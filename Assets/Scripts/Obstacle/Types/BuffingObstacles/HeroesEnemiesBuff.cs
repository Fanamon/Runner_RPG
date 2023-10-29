using System.Collections.Generic;
using UnityEngine;
using Attributes;
using UnityEngine.Events;

public class HeroesEnemiesBuff : BuffingObstacle
{
    [SerializeField] private Enemy[] _enemies;

    private int _enemyAttributeNumber;
    private float _enemyBuffValue;
    private Sprite _enemyAttributeSprite;
    private Enemy _enemyToBuff;

    public event UnityAction<string, float, Sprite> EnemyBuffInitiated;

    public override void Initialize()
    {
        int enemyIndex = Random.Range(0, _enemies.Length);

        _enemyToBuff = _enemies[enemyIndex];
        _enemyBuffValue = GetRandomizeBuffValue(ref _enemyAttributeNumber, ref _enemyAttributeSprite);
        EnemyBuffInitiated?.Invoke(_enemyToBuff.Title, _enemyBuffValue, _enemyAttributeSprite);

        base.Initialize();
    }

    protected override void ImplementConsequences(ObstacleEntrySensor obstacleEntrySensor)
    {
        obstacleEntrySensor.OnHeroesEnemiesBuffEntered(HeroesBuffValue, HeroesAttributeNumber);
        BuffEnemyAttribute(_enemyAttributeNumber);
        DisableObstacle();
    }

    private void BuffEnemyAttribute(int attributeNumber)
    {
        switch (attributeNumber)
        {
            case (int)Attribute.Health:
                _enemyToBuff.IncreaseHealth(_enemyBuffValue);
                break;

            case (int)Attribute.Damage:
                _enemyToBuff.IncreaseDamage(_enemyBuffValue);
                break;

            case (int)Attribute.Armor:
                _enemyToBuff.IncreaseArmor(_enemyBuffValue);
                break;
        }
    }
}