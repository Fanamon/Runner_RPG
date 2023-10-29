using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class State : MonoBehaviour
{
    [SerializeField][Range(0f, 1f)] private float _oneObstacleActiveValue;
    [SerializeField][Range(0f, 1f)] private float _twoObstacleActiveValue;
    [SerializeField][Range(0f, 1f)] private float _noneObstaclesActiveValue;
    [SerializeField] private List<Transition> _transitions;
    [SerializeField] private ObstacleChanceInfo[] _obstacleChances;

    public event UnityAction<State> StateEnded;

    public float OneObstacleActiveValue => _oneObstacleActiveValue;
    public float TwoObstacleActiveValue => _twoObstacleActiveValue;
    public float NoneObstaclesActiveValue => _noneObstaclesActiveValue;

    protected virtual void OnEnable() { }

    public virtual void Enter()
    {
        if (enabled == false)
        {
            enabled = true;

            foreach (var transition in _transitions)
            {
                transition.enabled = true;
                transition.ConditionsWereMet += OnCoditionsWereMet;
            }
        }
    }

    public void Exit()
    {
        if (enabled == true)
        {
            foreach (var transition in _transitions)
            {
                transition.ConditionsWereMet -= OnCoditionsWereMet;
                transition.enabled = false;
            }

            enabled = false;
        }
    }

    public Dictionary<string, float> GetObstacleChances()
    {
        Dictionary<string, float> obstacleChances = new Dictionary<string, float>();

        foreach (var obstacleChance in _obstacleChances)
        {
            obstacleChances.Add(obstacleChance.Title, obstacleChance.Chance);
        }

        return obstacleChances;
    }

    protected void OnCoditionsWereMet(State targetState)
    {
        StateEnded?.Invoke(targetState);
    }
}

[Serializable]
public class ObstacleChanceInfo
{
    [SerializeField] private ObstacleObject _obstacleObject;
    [SerializeField][Range(0f, 1f)] private float _chance;

    public string Title => _obstacleObject.Title;
    public float Chance => _chance;
}
