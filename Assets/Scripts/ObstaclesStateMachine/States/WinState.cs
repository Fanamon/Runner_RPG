using System.Collections;
using UnityEngine;

public class WinState : State
{
    [SerializeField] private WinView _winView;

    protected override void OnEnable()
    {
        Time.timeScale = 0;
        StartCoroutine(EnableWinView());
    }

    private IEnumerator EnableWinView()
    {
        float delay = 3;

        yield return new WaitForSeconds(delay);

        Time.timeScale = 0;
        _winView.gameObject.SetActive(true);
    }
}
