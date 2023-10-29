using UnityEngine;
using UnityEngine.UI;

public abstract class MusicButton : MonoBehaviour
{
    [SerializeField] protected AudioSource AudioSource;
    [SerializeField] protected Button MusicOffButton;
    [SerializeField] protected Button MusicOnButton;

    protected abstract void OnEnable();

    protected abstract void OnDisable();

    protected abstract void OnButtonClick();
}
