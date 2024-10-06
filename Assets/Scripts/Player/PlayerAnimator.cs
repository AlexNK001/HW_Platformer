using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private UserInput _userInput;
    [SerializeField] private PlayerPhysic _physicCharacter;
    [SerializeField] private Animator _animator;

    private void Start()
    {
        _userInput.Moved += Move;
        _userInput.AttackedEvent += Attack;
    }

    private void Update()
    {
        _animator.SetBool(AnimationCommand.IsJump, _physicCharacter.IsGround == false);
    }

    private void OnDisable()
    {
        _userInput.Moved -= Move;
        _userInput.AttackedEvent -= Attack;
    }

    private void Move(float direction)
    {
        bool isRun = direction > 0 || direction < 0;
        _animator.SetBool(AnimationCommand.IsWalk, isRun && _physicCharacter.IsGround);
    }

    private void Attack(bool isAttack)
    {
        _animator.SetBool(AnimationCommand.Attack, isAttack);
    }
}
