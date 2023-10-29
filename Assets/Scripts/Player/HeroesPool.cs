using UnityEngine;

public class HeroesPool : MonoBehaviour
{
    [SerializeField] private GameObject[] _heroesPrefabs;
    [SerializeField] private Transform _startHeroPosition;
    [SerializeField] private Transform _container;
    [SerializeField] private CastingShootingObjectPool _fireballPool;
    [SerializeField] private CastingShootingObjectPool _arrowPool;

    public void Initialize()
    {
        for (int i = 0; i < _heroesPrefabs.Length; i++)
        {
            GameObject hero = Instantiate(_heroesPrefabs[i], _container);

            if (hero.TryGetComponent<Mage>(out Mage mage))
            {
                mage.SetFireballPool(_fireballPool);
            }
            else if (hero.TryGetComponent<Archer>(out Archer archer))
            {
                archer.SetArrowPool(_arrowPool);
            }
        }
    }

    public int GetHeroesCount()
    {
        return _heroesPrefabs.Length;
    }
}
