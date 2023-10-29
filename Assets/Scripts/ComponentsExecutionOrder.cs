using UnityEngine;

public class ComponentsExecutionOrder : MonoBehaviour
{
    [SerializeField] private PlatformPool _platformPool;
    [SerializeField] private ObstacleChancesChangesObserver _obstacleChancesChangesObserver;
    [SerializeField] private ObstaclesStateMachine _stateMachine;
    [SerializeField] private HeroesPool _heroesPool;
    [SerializeField] private HeroesGroupPlacementsChanger _heroesGroupPlacementsChanger;
    [SerializeField] private HeroesGroup _heroesGroup;
    [SerializeField] private HeroViewPool _heroViewPool;

    private void Awake()
    {
        _platformPool.Initialize();
        _heroesPool.Initialize();
        _heroViewPool.Initialize();
        _heroesGroupPlacementsChanger.Initialize();
        _heroesGroup.Initialize();
        _obstacleChancesChangesObserver.Initialize();
        _stateMachine.Initialize();
    }

    private void OnEnable()
    {
        _platformPool.InitializeRandomPlatformsObstaclePlacementActivity();
        _heroViewPool.InitializeHeroesBuffs();
    }
}
