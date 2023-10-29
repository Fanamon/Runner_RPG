using UnityEngine;
using UnityEngine.Events;

public abstract class Transition : MonoBehaviour
{
    [SerializeField] private State _targetState;
    [SerializeField] protected Transform PlatformsContainer;

    public event UnityAction<State> ConditionsWereMet;

    protected virtual void OnEnable() { }

    protected void EndState()
    {
        ConditionsWereMet?.Invoke(_targetState);
    }
}
