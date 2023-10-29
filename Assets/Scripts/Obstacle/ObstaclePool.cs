using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstaclePool : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private GameObject[] _obstaclePrefabs;

    private CameraViewObserver _cameraViewObserver;
    private CastingShootingObjectPool _fireballPool;
    private HeroesPool _heroesPool;
    private ObstacleChancesChangesObserver _obstacleChancesChangesObserver;
    private Dictionary<GameObject, float> _obstaclesWithChances;

    private void Awake()
    {
        _obstacleChancesChangesObserver.ObstacleChancesChanged += OnObstacleChancesChanged;
    }

    private void OnDisable()
    {
        _obstacleChancesChangesObserver.ObstacleChancesChanged -= OnObstacleChancesChanged;
    }

    private void Update()
    {
        TryDisableObstaclesOutOfCameraView();
    }

    public void SetAdditionalObjects(CameraViewObserver cameraViewObserver, CastingShootingObjectPool fireballPool,
        HeroesPool heroesPool, ObstacleChancesChangesObserver obstacleChancesChangesObserver)
    {
        _cameraViewObserver = cameraViewObserver;
        _fireballPool = fireballPool;
        _heroesPool = heroesPool;
        _obstacleChancesChangesObserver = obstacleChancesChangesObserver;
    }

    public void SetRandomObstacle()
    {
        if (HeroObstacle.IsAlreadyExist == false && GhoulBoss.IsAlreadyExist == false)
        {
            EnableRandomObstacle(_obstaclesWithChances);
        }
        else if (HeroObstacle.IsAlreadyExist == false)
        {
            EnableRandomObstacle(_obstaclesWithChances.Where
                (obstacleWithChance => obstacleWithChance.Key.TryGetComponent<GhoulBoss>(out var bossComponent) == false).
                ToDictionary(obstacleWithChance => obstacleWithChance.Key, obstacleWithChance => obstacleWithChance.Value));
        }
        else if (GhoulBoss.IsAlreadyExist == false)
        {
            EnableRandomObstacle(_obstaclesWithChances.Where
                (obstacleWithChance => obstacleWithChance.Key.TryGetComponent<HeroObstacle>(out var heroObstacleComponent) == false).
                ToDictionary(obstacleWithChance => obstacleWithChance.Key, obstacleWithChance => obstacleWithChance.Value));
        }
        else
        {
            EnableRandomObstacle(_obstaclesWithChances.Where
                (obstacleWithChance => obstacleWithChance.Key.TryGetComponent<HeroObstacle>(out var heroObstacleComponent) == false 
                && obstacleWithChance.Key.TryGetComponent<GhoulBoss>(out var bossComponent) == false).
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