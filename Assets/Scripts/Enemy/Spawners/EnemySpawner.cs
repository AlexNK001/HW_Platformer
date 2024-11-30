using Enemy;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyEntryPoint _prefab;
    [SerializeField] private bool _haveCollectionCheck = true;
    [SerializeField] private int _defaultCapacity = 5;
    [SerializeField] private int _maxSize = 100;
    [SerializeField] private EnemySpawnPoint[] _spawnPoints;
    [SerializeField] private Transform _waypointParent;
    [SerializeField] private Transform _healthBarParent;

    private ObjectPool<EnemyEntryPoint> _enemies;

    public Transform HealthBarParent => _healthBarParent;

    private void Start()
    {
        _enemies = new ObjectPool<EnemyEntryPoint>(
            createFunc: () => CreateFunc(),
            actionOnGet: (enemyEntryPoint) => enemyEntryPoint.gameObject.SetActive(true),
            actionOnRelease: (enemyEntryPoint) => enemyEntryPoint.gameObject.SetActive(false),
            actionOnDestroy: (enemyEntryPoint) => Destroy(enemyEntryPoint),
            collectionCheck: _haveCollectionCheck,
            defaultCapacity: _defaultCapacity,
            maxSize: _maxSize);

        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            EnemyEntryPoint enemy = _enemies.Get();
            enemy.SetEnemyPoints(_spawnPoints[i]);
            enemy.transform.position = _spawnPoints[i].transform.position;
        }
    }

    public void Relise(EnemyEntryPoint enemyEntryPoint)
    {
        _enemies.Release(enemyEntryPoint);
    }

    private EnemyEntryPoint CreateFunc()
    {
        EnemyEntryPoint enemyEntryPoint = Instantiate(_prefab);
        enemyEntryPoint.Initilization(this);
        enemyEntryPoint.gameObject.SetActive(false);
        return enemyEntryPoint;
    }
}
