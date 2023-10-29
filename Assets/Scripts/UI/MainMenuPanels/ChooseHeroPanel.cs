using Necrotor.TypedScenes;
using UnityEngine;
using UnityEngine.UI;

public class ChooseHeroPanel : MonoBehaviour
{
    [SerializeField] private StartPanel _startPanel;
    [SerializeField] private Button _knightButton;
    [SerializeField] private Button _archerButton;
    [SerializeField] private Button _mageButton;
    [SerializeField] private Button _backToMenuButton;
    [SerializeField] private StartHeroConfig _startHeroConfig;

    private void OnEnable()
    {
        _knightButton.onClick.AddListener(OnKnightButtonClick);
        _archerButton.onClick.AddListener(OnArcherButtonClick);
        _mageButton.onClick.AddListener(OnMageButtonClick);
        _backToMenuButton.onClick.AddListener(OnBackToMenuButtonClick);
    }

    private void OnDisable()
    {
        _knightButton.onClick.RemoveListener(OnKnightButtonClick);
        _archerButton.onClick.RemoveListener(OnArcherButtonClick);
        _mageButton.onClick.RemoveListener(OnMageButtonClick);
        _backToMenuButton.onClick.RemoveListener(OnBackToMenuButtonClick);
    }

    private void OnKnightButtonClick()
    {
        _startHeroConfig.SetStartHeroNumber((int)HeroNumber.Knight);
        GameplayScene.Load();
    }

    private void OnArcherButtonClick()
    {
        _startHeroConfig.SetStartHeroNumber((int)HeroNumber.Archer);
        GameplayScene.Load();
    }

    private void OnMageButtonClick()
    {
        _startHeroConfig.SetStartHeroNumber((int)HeroNumber.Mage);
        GameplayScene.Load();
    }

    private void OnBackToMenuButtonClick()
    {
        _startPanel.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}

enum HeroNumber
{
    Knight = 0,
    Archer = 1,
    Mage = 2,
}