using UnityEngine;

namespace Enemy
{
    internal class PatrolState : EnemyState
    {
        [SerializeField] private float _walkSpeed = 3f;
        [SerializeField] private WaypointTarget _waypointTarget;

        private EnemySpawnPoint _enemySpawnPoint;

        private void Update()
        {
            transform.position = Vector2.MoveTowards(
                         transform.position,
                         _waypointTarget.transform.position,
                         _walkSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out WaypointTarget waypointTarget))
            {
                if (_waypointTarget == waypointTarget)
                {
                    _waypointTarget.transform.position = _enemySpawnPoint.GetNextPosition();
                }
            }
        }

        internal override void Initilization(Animator animator)
        {
            base.Initilization(animator);

            _waypointTarget.transform.position = _enemySpawnPoint.GetNextPosition();
        }

        internal void SetEnemyPoints(EnemySpawnPoint enemySpawnPoint)
        {
            _enemySpawnPoint = enemySpawnPoint;
        }

        internal override void Enable()
        {
            _waypointTarget.transform.position = _enemySpawnPoint.GetNextPosition();
            EnemyAnimator.SetTrigger(EnemyAnimationCommand.Walk);
        }

        internal override void Disable()
        {
            EnemyAnimator.ResetTrigger(EnemyAnimationCommand.Walk);
        }
    }
}
