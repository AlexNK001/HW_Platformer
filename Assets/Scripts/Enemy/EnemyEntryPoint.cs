using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class EnemyEntryPoint : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private EnemyStateMachine _enemy;
        [SerializeField] private Heart _heart;
        [SerializeField, Min(0f)] private float _maxHealth;
        [SerializeField, Min(0f)] private float _currentHealth;
        [SerializeField] private HeartListener _heartListener;
        [SerializeField] private AnimationClip _deadAnimation;
        [SerializeField] private Collider2D _mainCollider;
        [SerializeField] private Collider2D _deathCollider;
        [SerializeField] private WaypointTarget _waypointTarget;
        [SerializeField] private PatrolState _patrol;
        [SerializeField] private Follower _follower;

        private EnemySpawner _enemySpawner;

        private void Start()
        {
            _enemy.Initilization(_heart);
            _heart.Initilization(_maxHealth, _currentHealth);
            _heart.Died += Die;
            _heartListener.Initilization(_heart, _maxHealth, _currentHealth);

            _waypointTarget.transform.SetParent(null);
        }

        internal void SetEnemyPoints(EnemySpawnPoint enemySpawnPoint)
        {
            _patrol.SetEnemyPoints(enemySpawnPoint);
        }

        internal void Initilization(EnemySpawner enemySpawner)
        {
            _enemySpawner = enemySpawner;
            _follower.transform.SetParent(_enemySpawner.HealthBarParent);
        }

        private void Die()
        {
            StartCoroutine(DiedDelay());
            _enemy.enabled = false;
            _animator.SetTrigger(EnemyAnimationCommand.Death);
            _deathCollider.enabled = true;
            _mainCollider.enabled = false;
        }

        private IEnumerator DiedDelay()
        {
            yield return new WaitForSeconds(_deadAnimation.length);
            _enemySpawner.Relise(this);
        }
    }
}
