using UnityEngine;
using Necrotor.TypedScenes;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameLoader : MonoBehaviour
{
    [SerializeField] private List<Button> _exitToMenuButtons;

    private void OnEnable()
    {
        foreach (var exitToMenuButton in _exitToMenuButtons)
        {
            exitToMenuButton.onClick.AddListener(OnButtonClick);
        }
    }

    private void OnDisable()
    {
        foreach (var exitToMenuButton in _exitToMenuButtons)
        {
            exitToMenuButton.onClick.RemoveListener(OnButtonClick);
        }
    }

    private void OnButtonClick()
    {
        Time.timeScale = 1;
        MainMenu.Load();
    }
}
