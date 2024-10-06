using System.Collections;
using UnityEngine;

public class CombatState : EnemyState
{
    [SerializeField] private AnimationClip _attackClip;
    [SerializeField] private AnimationClip _idleClip;

    private WaitForSeconds _attackDelay;
    private WaitForSeconds _delayBetweenAttacks;
    private Coroutine _repeatAttack;

    private void Start()
    {
        _attackDelay = new WaitForSeconds(_attackClip.length);
        _delayBetweenAttacks = new WaitForSeconds(_idleClip.length);
    }

    private void OnEnable()
    {
        _repeatAttack = StartCoroutine(RepeatAttack());
    }

    private void OnDisable()
    {
        if (_repeatAttack != null)
        {
            StopCoroutine(_repeatAttack);
        }

        _animator.ResetTrigger(EnemyAnimationCommand.FirstSkill);
        _animator.ResetTrigger(EnemyAnimationCommand.SecondIdle);
    }

    private IEnumerator RepeatAttack()
    {
        while (gameObject.activeSelf)
        {
            _animator.SetTrigger(EnemyAnimationCommand.FirstSkill);
            yield return _attackDelay;
            _animator.SetTrigger(EnemyAnimationCommand.SecondIdle);
            yield return _delayBetweenAttacks;
        }
    }
}
