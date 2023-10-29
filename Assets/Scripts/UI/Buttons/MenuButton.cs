using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MenuButton : MonoBehaviour
{
    [SerializeField] private MenuConfirmationPanel _menuConfirmationPanel;
    
    private Button _menuButton;

    private void Awake()
    {
        _menuButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _menuButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _menuButton.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        Time.timeScale = 0;
        _menuConfirmationPanel.gameObject.SetActive(true);
    }
}
