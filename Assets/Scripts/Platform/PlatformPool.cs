using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatformPool : MonoBehaviour
{
    [Header("Additional Objects")]
    [SerializeField] protected CameraViewObserver CameraViewObserver;
    [SerializeField] private CastingShootingObjectPool _fireballPool;
    [SerializeField] private HeroesPool _heroesPool;
    [SerializeField] private ObstacleChancesChangesObserver _obstacleChancesChangesObserver;

    [Header("Platforms")]
    [SerializeField] private int _platformsCount;
    [SerializeField] private GameObject _platformPrefab;
    [SerializeField] private Transform _container;

    [Header("Obstacles")]
    [SerializeField] private float _startObstaclePositionZ;
    [SerializeField] private float _stepObstaclePositionZ;
    [SerializeField] private float[] _obstaclePositionsX;

    protected float PlatformLength;
    protected float PlatformHeight;
    protected Vector3 CurrentPlatformPosition;
    protected Vector3 StepPlatformPosition;
    protected Queue<GameObject> Platforms = new Queue<GameObject>();

    private float _oneObstacleActiveValue;
    private float _twoObstaclesActiveValue;
    private float _noneObstaclesActiveValue;

    public void Initialize()
    {
        float currentObstaclePositionZ = _startObstaclePositionZ;

        _obstacleChancesChangesObserver.ObstacleActiveValuesChanged += OnObstacleActiveValuesChanged;
        CurrentPlatformPosition = transform.position;

        for (int i = 0; i < _platformsCount; i++)
        {
            CreatePlatform(ref currentObstaclePositionZ);
        }
    }

    private void OnDisable()
    {
        _obstacleChancesChangesObserver.ObstacleActiveValuesChanged -= OnObstacleActiveValuesChanged;
    }

    public void InitializeRandomPlatformsObstaclePlacementActivity()
    {
        int initialObstacleLine = 3;

        Platforms.Peek().GetComponent<ObstaclePlacements>().RandomizeObstaclesActivity(initialObstacleLine);

        for (int i = 1; i < Platforms.Count; i++)
        {
            Platforms.ToList()[i].GetComponent<ObstaclePlacements>().RandomizeObstaclesActivity();
        }
    }

    private void OnObstacleActiveValuesChanged(float oneObstacleActiveValue, float twoObstaclesActiveValue,
        float noneObstaclesActiveValue)
    {
        _oneObstacleActiveValue = oneObstacleActiveValue;
        _twoObstaclesActiveValue = twoObstaclesActiveValue;
        _noneObstaclesActiveValue = noneObstaclesActiveValue;

        Platforms.ToList().ForEach(platform =>
        {
            platform.GetComponent<ObstaclePlacements>().SetObstacleActiveValues(_oneObstacleActiveValue,
            _twoObstaclesActiveValue, _noneObstaclesActiveValue);
        });
    }

    private void CreatePlatform(ref float currentObstaclePositionZ)
    {
        GameObject generatedPlatform = Instantiate(_platformPrefab, CurrentPlatformPosition, transform.rotation, _container);

        if (Platforms.Count == 0)
        {
            PlatformLength = generatedPlatform.GetComponentInChildren<RoadPlatform>().transform.localScale.z;
            StepPlatformPosition = new Vector3(0, 0, PlatformLength);
        }

        generatedPlatform.GetComponent<ObstaclePlacements>().GenerateObstaclePlacements(ref currentObstaclePositionZ, 
            _stepObstaclePositionZ, GetTempArrayOfObstaclePositionsX(), CameraViewObserver, _fireballPool, _heroesPool,
            _obstacleChancesChangesObserver);
        Platforms.Enqueue(generatedPlatform);
        CurrentPlatformPosition += StepPlatformPosition;
    }

    private float[] GetTempArrayOfObstaclePositionsX()
    {
        float[] tempArray = new float[_obstaclePositionsX.Length];

        for (int i = 0; i < tempArray.Length; i++)
        {
            tempArray[i] = _obstaclePositionsX[i];
        }

        return tempArray;
    }
}