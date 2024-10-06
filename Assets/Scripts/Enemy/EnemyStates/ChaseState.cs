using UnityEngine;

public class ChaseState : EnemyState
{
    [SerializeField] private float _runSpeed = 6f;
    [SerializeField] private AreaAction _pursueArea;

    private void OnEnable()
    {
        _animator.SetTrigger(EnemyAnimationCommand.Run);
    }

    private void OnDisable()
    {
        _animator.ResetTrigger(EnemyAnimationCommand.Run);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(
                     transform.position,
                     _pursueArea.Target.position,
                     _runSpeed * Time.deltaTime);
    }
}