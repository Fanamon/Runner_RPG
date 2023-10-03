using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentsExecutionOrder : MonoBehaviour
{
    [SerializeField] private PlatformPool _platformPool;
    [SerializeField] private ObstacleChancesChangesObserver _obstacleChancesChangesObserver;
    [SerializeField] private HeroesPool _heroesPool;
    [SerializeField] private HeroesGroupPlacementsChanger _heroesGroupPlacementsChanger;
    [SerializeField] private HeroesGroup _heroesGroup;

    private void Awake()
    {
        _platformPool.Initialize();
        _heroesPool.Initialize();
        _heroesGroupPlacementsChanger.Initialize();
        _heroesGroup.Initialize();
        _obstacleChancesChangesObserver.Activate();
    }

    private void OnEnable()
    {
        _platformPool.InitializeRandomPlatformsObstaclePlacementActivity();
    }
}
