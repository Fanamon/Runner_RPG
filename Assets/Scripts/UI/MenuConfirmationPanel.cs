using UnityEngine;
using UnityEngine.UI;

public class MenuConfirmationPanel : MonoBehaviour
{
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _resumeButton;

    private void OnEnable()
    {
        _exitButton.onClick.AddListener(OnExitButtonClick);
        _resumeButton.onClick.AddListener(OnResumeButtonClick);
    }

    private void OnDisable()
    {
        _exitButton.onClick.RemoveListener(OnExitButtonClick);
        _resumeButton.onClick.RemoveListener(OnResumeButtonClick);
    }

    private void OnExitButtonClick()
    {
        
    }

    private void OnResumeButtonClick()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
