using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ObstaclePool : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private GameObject[] _obstaclePrefabs;

    private CameraViewObserver _cameraViewObserver;
    private FireballPool _fireballPool;
    private HeroesPool _heroesPool;
    private ObstacleChancesChangesObserver _obstacleChancesChangesObserver;
    private Coroutine _heroReplacer;
    private Dictionary<GameObject, float> _obstaclesWithChances;

    private void Awake()
    {
        _obstacleChancesChangesObserver.ObstacleChancesChanged += OnObstacleChancesChanged;
    }

    private void OnDisable()
    {
        _obstacleChancesChangesObserver.ObstacleChancesChanged -= OnObstacleChancesChanged;

        foreach (var obstacleWithChances in _obstaclesWithChances)
        {
            if (obstacleWithChances.Key.TryGetComponent<HeroObstacle>(out var heroObstacle))
            {
                heroObstacle.ObstacleDestroyed -= OnObstacleDestroyed;
            }
        }
    }

    private void Update()
    {
        TryDisableObstaclesOutOfCameraView();
    }

    public void SetAdditionalObjects(CameraViewObserver cameraViewObserver, FireballPool fireballPool,
        HeroesPool heroesPool, ObstacleChancesChangesObserver obstacleChancesChangesObserver)
    {
        _cameraViewObserver = cameraViewObserver;
        _fireballPool = fireballPool;
        _heroesPool = heroesPool;
        _obstacleChancesChangesObserver = obstacleChancesChangesObserver;
    }

    public void SetRandomObstacle()
    {
        if (HeroObstacle.IsAlreadyExist == false)
        {
            EnableRandomObstacle(_obstaclesWithChances);
        }
        else
        {
            EnableRandomObstacle(_obstaclesWithChances.Where
                (obstacleWithChance => obstacleWithChance.Key.TryGetComponent<HeroObstacle>(out var component) == false).
                ToDictionary(obstacleWithChance => obstacleWithChance.Key, obstacleWithChance => obstacleWithChance.Value));
        }
    }

    private void EnableRandomObstacle(Dictionary<GameObject, float> obstaclesWithChances)
    {
        float obstacleChanceSum = obstaclesWithChances.Values.Sum();
        float randomValue = Random.Range(0, obstacleChanceSum);

        obstacleChanceSum = 0;

        foreach (var obstacleWithChance in obstaclesWithChances)
        {
            obstacleChanceSum += obstacleWithChance.Value;

            if (randomValue < obstacleChanceSum)
            {
                EnableObstacle(obstacleWithChance.Key);
                break;
            }
        }
    }

    private void OnObstacleChancesChanged(Dictionary<string, float> obstacleChances)
    {
        if (_obstaclesWithChances == null)
        {
            GenerateObstacles(obstacleChances);
        }
        else
        {
            SetObstacleChances(obstacleChances);
        }
    }

    private void OnObstacleDestroyed(GameObject obstacle)
    {
        obstacle.GetComponent<HeroObstacle>().ObstacleDestroyed -= OnObstacleDestroyed;
        _obstaclesWithChances.Remove(obstacle);
    }

    private void EnableObstacle(GameObject obstacle)
    {
        obstacle.GetComponent<Obstacle>().enabled = true;
        obstacle.SetActive(true);
    }

    private void TryDisableObstaclesOutOfCameraView()
    {
        if (_cameraViewObserver.IsPositionLowerDisablePointZ(transform.position.z))
        {
            _obstaclesWithChances.Keys.Where(obstacle => obstacle.activeSelf == true).ToList()
                .ForEach(obstacle =>
                {
                    if (obstacle.TryGetComponent<HeroObstacle>(out var component))
                    {
                        component.ReturnHeroToPool();
                    }

                    obstacle.SetActive(false);
                });
        }
    }

    private void GenerateObstacles(Dictionary<string, float> obstacleChances)
    {
        _obstaclesWithChances = new Dictionary<GameObject, float>();

        foreach (var obstacle in _obstaclePrefabs)
        {
            GameObject generatedObstacle = Instantiate(obstacle, _container);

            if (generatedObstacle.TryGetComponent<Witch>(out var witch))
            {
                witch.SetFireballPool(_fireballPool);
            }
            else if (generatedObstacle.TryGetComponent<HeroObstacle>(out var heroObstacle))
            {
                heroObstacle.ObstacleDestroyed += OnObstacleDestroyed;
                heroObstacle.SetHeroesPool(_heroesPool);
            }

            _obstaclesWithChances.Add(generatedObstacle, 
                obstacleChances[generatedObstacle.GetComponent<Obstacle>().Title]);
        }
    }

    private void SetObstacleChances(Dictionary<string, float> obstacleChances)
    {
        foreach (var obstacleChance in obstacleChances)
        {
            _obstaclesWithChances[_obstaclesWithChances.Keys
                .First(key => key.GetComponent<Obstacle>().Title == obstacleChance.Key)] = obstacleChance.Value;
        }
    }
}