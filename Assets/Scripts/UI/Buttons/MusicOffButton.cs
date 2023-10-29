using UnityEngine;

public class MusicOffButton : MusicButton
{
    protected override void OnEnable()
    {
        MusicOffButton.onClick.AddListener(OnButtonClick);
    }

    protected override void OnDisable()
    {
        MusicOffButton.onClick.RemoveListener(OnButtonClick);
    }

    protected override void OnButtonClick()
    {
        AudioSource.Pause();
        MusicOnButton.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
