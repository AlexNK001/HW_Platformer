using UnityEngine;
using System.Collections;

namespace Enemy
{
    internal class CombatState : EnemyState
    {
        [SerializeField] private AnimationClip _attackClip;
        [SerializeField] private AnimationClip _idleClip;
        [SerializeField, Min(0f)] private float _weaponActivationTime;
        [SerializeField] private Weapon _weapon;

        private WaitForSeconds _attackDelay;
        private WaitForSeconds _weaponActivationDelay;
        private WaitForSeconds _delayBetweenAttacks;

        internal override void Initilization(Animator animator)
        {
            base.Initilization(animator);

            _weaponActivationDelay = new WaitForSeconds(_weaponActivationTime);
            _attackDelay = new WaitForSeconds(_attackClip.length - _weaponActivationTime);
            _delayBetweenAttacks = new WaitForSeconds(_idleClip.length);
        }

        internal override void Enable()
        {
            StartCoroutine(RepeatAttack());
        }

        internal override void Disable()
        {
            EnemyAnimator.ResetTrigger(EnemyAnimationCommand.FirstSkill);
            EnemyAnimator.ResetTrigger(EnemyAnimationCommand.SecondIdle);
        }

        private IEnumerator RepeatAttack()
        {
            while (enabled)
            {
                EnemyAnimator.SetTrigger(EnemyAnimationCommand.FirstSkill);
                yield return _weaponActivationDelay;
                _weapon.SetActiveCollider(true);
                yield return _attackDelay;
                EnemyAnimator.SetTrigger(EnemyAnimationCommand.SecondIdle);
                yield return _delayBetweenAttacks;
            }
        }
    }
}
