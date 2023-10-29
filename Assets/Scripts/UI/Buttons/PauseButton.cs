using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PauseButton : MonoBehaviour
{
    [SerializeField] private PausePanel _pausePanel;

    private Button _pauseButton;

    private void Awake()
    {
        _pauseButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _pauseButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _pauseButton.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        Time.timeScale = 0;
        _pausePanel.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
