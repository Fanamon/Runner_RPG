using Necrotor.TypedScenes;
using UnityEngine;
using UnityEngine.UI;

public class WinView : MonoBehaviour
{
    [SerializeField] private Button _restartButton;

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        Time.timeScale = 1;
        GameplayScene.Load();
    }
}
