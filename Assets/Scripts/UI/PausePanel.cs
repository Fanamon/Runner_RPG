using UnityEngine;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _pauseButton;

    private void OnEnable()
    {
        _resumeButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _resumeButton.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        Time.timeScale = 1f;
        _pauseButton.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
