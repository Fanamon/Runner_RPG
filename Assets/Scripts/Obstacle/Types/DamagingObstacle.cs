
public abstract class DamagingObstacle : Obstacle
{
    protected Enemy EnemyObject;

    public float TotalDamage { get; protected set; }

    public abstract void TakeDamage(float damage);

    protected virtual void Die() { }

    protected virtual void Awake()
    {
        EnemyObject = ObstacleObject as Enemy;
    }

    protected virtual void OnEnable()
    {
        CircleUpParticle.gameObject.SetActive(true);
    }

    protected virtual void StartCombat(ObstacleEntrySensor obstacleEntrySensor)
    {
        obstacleEntrySensor.CounterattackStarted += OnCounterattackStarted;
        obstacleEntrySensor.OnDamagingObstacleEntered(TotalDamage);
    }

    protected override void DisableObstacle()
    {
        base.DisableObstacle();
        this.enabled = false;
    }

    protected override void ImplementConsequences(ObstacleEntrySensor obstacleEntrySensor)
    {
        StartCombat(obstacleEntrySensor);
    }

    private void OnCounterattackStarted(float damage, ObstacleEntrySensor sensor)
    {
        TakeDamage(damage);
        sensor.CounterattackStarted -= OnCounterattackStarted;
        DisableObstacle();
    }
}
