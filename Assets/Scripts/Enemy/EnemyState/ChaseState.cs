using UnityEngine;

namespace Enemy
{
    internal class ChaseState : EnemyState
    {
        [SerializeField] private float _runSpeed = 6f;
        [SerializeField] private AreaAction _pursueArea;

        private void Update()
        {
            transform.position = Vector3.MoveTowards(
                         transform.position,
                         _pursueArea.Target.position,
                         _runSpeed * Time.deltaTime);
        }

        internal override void Enable()
        {
            EnemyAnimator.SetTrigger(EnemyAnimationCommand.Run);
        }

        internal override void Disable()
        {
            EnemyAnimator.ResetTrigger(EnemyAnimationCommand.Run);
        }
    }
}
