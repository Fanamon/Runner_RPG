using Attributes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BuffingObstacle : Obstacle
{
    [SerializeField] private Sprite _heart;
    [SerializeField] private Sprite _knife;
    [SerializeField] private Sprite _shield;
    [SerializeField] private List<float> _buffValues;

    protected int HeroesAttributeNumber;
    protected float HeroesBuffValue;
    private Sprite _heroesAttributeSprite;

    public event UnityAction<float, Sprite> HeroesBuffInitiated;

    private void OnEnable()
    {
        CircleUpParticle.gameObject.SetActive(true);
    }

    public virtual void Initialize()
    {
        HeroesBuffValue = GetRandomizeBuffValue(ref HeroesAttributeNumber, ref _heroesAttributeSprite);
        HeroesBuffInitiated?.Invoke(HeroesBuffValue, _heroesAttributeSprite);
    }

    protected override void DisableObstacle()
    {
        base.DisableObstacle();
        gameObject.SetActive(false);
    }

    protected float GetRandomizeBuffValue(ref int attributeNumber, ref Sprite attributeSprite)
    {
        int randomNumber = Random.Range(0, _buffValues.Count);
        attributeNumber = Random.Range((int)Attribute.Health, (int)Attribute.Armor + 1);

        switch (attributeNumber)
        {
            case (int)Attribute.Health:
                attributeSprite = _heart;
                break;

            case (int)Attribute.Damage:
                attributeSprite = _knife;
                break;

            case (int)Attribute.Armor:
                attributeSprite = _shield;
                break;
        }

        return _buffValues[randomNumber];
    }
}
