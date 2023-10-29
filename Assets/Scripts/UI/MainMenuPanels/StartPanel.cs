using UnityEngine;
using UnityEngine.UI;

public class StartPanel : MonoBehaviour
{
    [SerializeField] private ChooseHeroPanel _chooseHeroPanel;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _exitButton;

    private void OnEnable()
    {
        _startButton.onClick.AddListener(OnStartButtonClick);
        _exitButton.onClick.AddListener(OnExitButtonClick);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(OnStartButtonClick);
        _exitButton.onClick.RemoveListener(OnExitButtonClick);
    }

    private void OnStartButtonClick()
    {
        _chooseHeroPanel.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    private void OnExitButtonClick()
    {
        Application.Quit();
    }
}
