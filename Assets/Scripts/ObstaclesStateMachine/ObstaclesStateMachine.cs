using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObstaclesStateMachine : MonoBehaviour
{
    [SerializeField] private State _firstState;

    private State _currentState;

    public event UnityAction<float, float, float> ObstacleActiveValueChanged;
    public event UnityAction<Dictionary<string, float>> StateChanged;

    public State CurrentState => _currentState;

    public void Initialize()
    {
        ResetState(_firstState);
    }

    private void OnDisable()
    {
        _currentState.StateEnded -= OnStateEnded;
    }

    private void OnStateEnded(State nextState)
    {
        Transit(nextState);
    }

    private void ResetState(State startState)
    {
        if (_currentState != null)
        {
            _currentState.StateEnded -= OnStateEnded;
        }

        _currentState = startState;

        _currentState.Enter();
        _currentState.StateEnded += OnStateEnded;
        ObstacleActiveValueChanged?.Invoke(_currentState.OneObstacleActiveValue, 
            _currentState.TwoObstacleActiveValue, _currentState.NoneObstaclesActiveValue);
        StateChanged?.Invoke(_currentState.GetObstacleChances());
    }

    private void Transit(State nextState)
    {
        if (_currentState != null)
        {
            _currentState.Exit();
            _currentState.StateEnded -= OnStateEnded;
        }

        _currentState = nextState;

        if (_currentState != null)
        {
            _currentState.Enter();
            _currentState.StateEnded += OnStateEnded;
            ObstacleActiveValueChanged?.Invoke(_currentState.OneObstacleActiveValue, 
                _currentState.TwoObstacleActiveValue, _currentState.NoneObstaclesActiveValue);
            StateChanged?.Invoke(_currentState.GetObstacleChances());
        }
    }
}
