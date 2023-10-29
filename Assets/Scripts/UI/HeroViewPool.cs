using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HeroViewPool : MonoBehaviour
{
    [SerializeField] private GameObject _heroView;
    [SerializeField] private Transform _container;
    [SerializeField] private HeroesPool _heroesPool;
    [SerializeField] private Transform _platformContainer;

    private List<HeroView> _heroViews = new List<HeroView>();
    private HeroBuff[] _heroesBuffs;

    public void Initialize()
    {
        int heroesCount = _heroesPool.GetHeroesCount();

        for (int i = 0; i < heroesCount; i++)
        {
            _heroViews.Add(Instantiate(_heroView, _container).GetComponent<HeroView>());
            _heroViews[i].GetComponentInChildren<BuffButton>(true).BuffButtonClicked += OnBuffButtonClicked;
        }
    }

    public void InitializeHeroesBuffs()
    {
        _heroesBuffs = _platformContainer.GetComponentsInChildren<HeroBuff>(true);

        foreach (var heroesBuff in _heroesBuffs)
        {
            heroesBuff.HeroesBuffEntered += OnHeroesBuffEntered;
        }
    }

    private void OnDisable()
    {
        foreach (var heroesBuff in _heroesBuffs)
        {
            heroesBuff.HeroesBuffEntered -= OnHeroesBuffEntered;
        }

        foreach (var heroView in _heroViews)
        {
            _heroView.GetComponentInChildren<BuffButton>(true).BuffButtonClicked -= OnBuffButtonClicked;
        }
    }

    public void EnableHeroView(Hero hero)
    {
        HeroView viewToEnable = _heroViews.First(view => view.gameObject.activeSelf == false);
        viewToEnable.gameObject.SetActive(true);
        viewToEnable.Initialize(hero);
        viewToEnable.GetComponentInChildren<BuffButton>(true).Initialize(hero);
    }

    public void DisableHeroView(Hero hero)
    {
        HeroView viewToDisable = _heroViews.First(view => view.HeroTitle == hero.Title);
        viewToDisable.gameObject.SetActive(false);
    }

    private void OnHeroesBuffEntered(float buffValue, int attributeNumber)
    {
        foreach (var heroView in _heroViews)
        {
            heroView.GetComponentInChildren<Button>(true).gameObject.SetActive(true);
            heroView.GetComponentInChildren<BuffButton>().InitializeBuff(buffValue, attributeNumber);
        }
    }

    private void OnBuffButtonClicked()
    {
        foreach (var heroView in _heroViews)
        {
            heroView.GetComponentInChildren<BuffButton>().gameObject.SetActive(false);
        }
    }
}
