using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstaclePlacements : MonoBehaviour
{
    [SerializeField] private GameObject _obstaclePlacementPrefab;
    [SerializeField] private GameObject _obstaclesPrefab;
    [SerializeField] private GameObject _obstacleLinePrefab;

    private float _oneObstacleActiveValue;
    private float _twoObstaclesActiveValue;
    private float _noneObstaclesActiveValue;

    private List<GameObject[]> _obstacles = new List<GameObject[]>();

    public void SetObstacleActiveValues(float oneObstacleActiveValue, float twoObstaclesActiveValue, 
        float noneObstaclesActiveValue)
    {
        _oneObstacleActiveValue = oneObstacleActiveValue;
        _twoObstaclesActiveValue = twoObstaclesActiveValue;
        _noneObstaclesActiveValue = noneObstaclesActiveValue;
    }

    public void RandomizeObstaclesActivity(int lineIndex = 0)
    {
        float obstacleActiveValuesSum = _oneObstacleActiveValue + _twoObstaclesActiveValue + _noneObstaclesActiveValue;

        for (int i = lineIndex; i < _obstacles.Count; i++)
        {
            float obstacleActiveValue = Random.Range(0, obstacleActiveValuesSum);

            if (obstacleActiveValue < _oneObstacleActiveValue)
            {
                int obstaclePlacementPositionIndex = Random.Range(0, _obstacles[i].Length);

                _obstacles[i][obstaclePlacementPositionIndex].GetComponent<ObstaclePool>()
                    .SetRandomObstacle();
            }
            else if (obstacleActiveValue < _oneObstacleActiveValue + _twoObstaclesActiveValue)
            {
                int obstacleUnplacementPositionIndex = Random.Range(0, _obstacles[i].Length);

                _obstacles[i].Where((value, index) => index != obstacleUnplacementPositionIndex).
                    ToList().ForEach(obstacle =>
                    {
                        obstacle.GetComponent<ObstaclePool>().SetRandomObstacle();
                    });
            }
        }
    }

    public void GenerateObstaclePlacements(ref float currentObstaclePositionZ, float stepPositionZ, 
        float[] obstaclePlacementPositionsX, CameraViewObserver cameraViewObserver,
        CastingShootingObjectPool fireballPool, HeroesPool heroesPool, ObstacleChancesChangesObserver obstacleChancesChangesObserver)
    {
        GameObject obstacles = Instantiate(_obstaclesPrefab, transform);

        while (currentObstaclePositionZ < transform.position.z)
        {
            _obstacles.Add(CreateLineOfObstacles(currentObstaclePositionZ, obstacles.transform, 
                obstaclePlacementPositionsX, cameraViewObserver, fireballPool, heroesPool, 
                obstacleChancesChangesObserver));

            currentObstaclePositionZ += stepPositionZ;
        }
    }

    private GameObject[] CreateLineOfObstacles(float currentObstaclePositionZ, Transform objectObstacles, 
        float[] obstaclePositionsX, CameraViewObserver cameraViewObserver, CastingShootingObjectPool fireballPool,
        HeroesPool heroesPool, ObstacleChancesChangesObserver obstacleChancesChangesObserver)
    {
        GameObject obstacleLine = Instantiate(_obstacleLinePrefab, new Vector3(objectObstacles.position.x,
            objectObstacles.position.y, currentObstaclePositionZ), objectObstacles.rotation, objectObstacles);
        GameObject[] lineOfObstacles = new GameObject[obstaclePositionsX.Length];

        for (int i = 0; i < lineOfObstacles.Length; i++)
        {
            lineOfObstacles[i] = CreateObstaclePlacement(obstaclePositionsX[i], obstacleLine, cameraViewObserver, 
                fireballPool, heroesPool, obstacleChancesChangesObserver);
        }

        return lineOfObstacles;
    }

    private GameObject CreateObstaclePlacement(float obstaclePositionsX, GameObject obstacleLine, 
        CameraViewObserver cameraViewObserver, CastingShootingObjectPool fireballPool, HeroesPool heroesPool,
        ObstacleChancesChangesObserver obstacleChancesChangesObserver)
    {
        GameObject obstaclePlacement = Instantiate(_obstaclePlacementPrefab, obstacleLine.transform);
        obstaclePlacement.transform.position =
            new Vector3(obstaclePositionsX, obstacleLine.transform.position.y, obstacleLine.transform.position.z);
        obstaclePlacement.GetComponent<ObstaclePool>().SetAdditionalObjects(cameraViewObserver, fireballPool,
            heroesPool, obstacleChancesChangesObserver);
        obstaclePlacement.SetActive(true);

        return obstaclePlacement;
    }
}