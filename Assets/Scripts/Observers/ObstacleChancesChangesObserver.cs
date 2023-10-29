using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObstacleChancesChangesObserver : MonoBehaviour
{
    [SerializeField] private ObstaclesStateMachine _stateMachine;

    public event UnityAction<float, float, float> ObstacleActiveValuesChanged;
    public event UnityAction<Dictionary<string, float>> ObstacleChancesChanged;

    public void Initialize()
    {
        _stateMachine.ObstacleActiveValueChanged += OnObstacleActiveValueChanged;
        _stateMachine.StateChanged += OnStateChanged;
    }

    private void OnDisable()
    {
        _stateMachine.ObstacleActiveValueChanged -= OnObstacleActiveValueChanged;
        _stateMachine.StateChanged -= OnStateChanged;
    }

    private void OnObstacleActiveValueChanged(float oneObstacleActiveValue, float twoObstacleActiveValue,
        float noneObstaclesActiveValue)
    {
        ObstacleActiveValuesChanged?.Invoke(oneObstacleActiveValue, twoObstacleActiveValue, noneObstaclesActiveValue);
    }

    private void OnStateChanged(Dictionary<string, float> obstacleChances)
    {
        ObstacleChancesChanged?.Invoke(obstacleChances);
    }
}
