using UnityEngine;

public class MusicOnButton : MusicButton
{
    protected override void OnEnable()
    {
        MusicOnButton.onClick.AddListener(OnButtonClick);
    }

    protected override void OnDisable()
    {
        MusicOnButton.onClick.RemoveListener(OnButtonClick);
    }

    protected override void OnButtonClick()
    {
        AudioSource.UnPause();
        MusicOffButton.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
