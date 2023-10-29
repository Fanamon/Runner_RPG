using UnityEngine;

public class BossState : State
{
    [SerializeField] private BossView _bossView;

    protected override void OnEnable()
    {
        _bossView.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        _bossView.gameObject.SetActive(false);
    }
}
