using UnityEngine;

public abstract class ObstacleObject : ScriptableObject
{
    [SerializeField] private string _title;

    public string Title => _title;
}
