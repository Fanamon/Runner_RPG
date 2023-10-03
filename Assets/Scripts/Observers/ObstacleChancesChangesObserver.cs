using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObstacleChancesChangesObserver : MonoBehaviour
{
    [SerializeField] private ObstacleChanceInfo[] _obstacleChances;

    public event UnityAction<Dictionary<string, float>> ObstacleChancesChanged;

    public void Activate()
    {
        OnStateChanged();
    }

    private void OnStateChanged()
    {
        Dictionary<string, float> obstacleChances = new Dictionary<string, float>();

        foreach (var obstacleChance in _obstacleChances)
        {
            obstacleChances.Add(obstacleChance.Name, obstacleChance.Chance);
        }

        ObstacleChancesChanged?.Invoke(obstacleChances);
    }
}

[Serializable]
public class ObstacleChanceInfo
{
    [SerializeField] private string _name;
    [SerializeField] private float _chance;

    public string Name => _name;
    public float Chance => _chance;
}
