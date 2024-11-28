using UnityEngine;

namespace Player
{
    internal class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private UserInput _userInput;
        private PlayerPhysic _playerPhysic;

        private Vector3 _scaleRigth;
        private Vector3 _scaleLeft;
        private int _currenVerticaltAnimation;
        private int _currentMoveAnimation;
        private Heart _heart;
        private float _currentHealth;

        private void OnDestroy()
        {
            _userInput.Moved -= Move;
            _userInput.Moved -= ChangeDirectionLook;
            _userInput.Attacked -= Attack;
            _userInput.Runed -= ChangeMoveAnimation;

            _playerPhysic.VerticalDirectionChanged -= ChooseVertivalAnimation;
        }

        internal void Initilization(UserInput userInput, PlayerPhysic playerPhysic, Heart heart, float currentHealth)
        {
            _userInput = userInput;

            _scaleRigth = transform.localScale;
            _scaleLeft = new Vector3
            (
                transform.localScale.x * -1,
                transform.localScale.y,
                transform.localScale.z
            );

            _userInput.Moved += Move;
            _userInput.Moved += ChangeDirectionLook;
            _userInput.Attacked += Attack;
            _userInput.Runed += ChangeMoveAnimation;
            _currentMoveAnimation = AnimationCommand.IsWalk;

            _playerPhysic = playerPhysic;
            _playerPhysic.VerticalDirectionChanged += ChooseVertivalAnimation;

            _heart = heart;
            _heart.HealthChanged += ChangeHairColor;
            _currentHealth = currentHealth;
        }

        private void ChangeMoveAnimation(bool isRun)
        {
            int animation = isRun ? AnimationCommand.IsRun : AnimationCommand.IsWalk;

            if (_currentMoveAnimation == animation == false)
            {
                _animator.SetBool(_currentMoveAnimation, false);
                _currentMoveAnimation = animation;
            }
        }

        private void ChangeHairColor(float currentHealtValue)
        {
            if (currentHealtValue < _currentHealth)
                _animator.SetTrigger(AnimationCommand.ChangeColor);

            _currentHealth = currentHealtValue;
        }

        private void ChangeDirectionLook(float horizontalAxisValue)
        {
            if (horizontalAxisValue > 0f)
            {
                transform.localScale = _scaleRigth;
            }
            else if (horizontalAxisValue < 0f)
            {
                transform.localScale = _scaleLeft;
            }
        }

        private void Move(float horizontalAxisValue)
        {
            bool isMove = horizontalAxisValue > 0 || horizontalAxisValue < 0;
            _animator.SetBool(_currentMoveAnimation, isMove && _playerPhysic.IsGround);
        }

        private void Attack()
        {
            _animator.SetTrigger(AnimationCommand.Attack);
        }

        private void ChooseVertivalAnimation(Vector2 verticalDirection)
        {
            _animator.SetBool(_currenVerticaltAnimation, false);

            if (verticalDirection == Vector2.up)
            {
                _currenVerticaltAnimation = AnimationCommand.IsJump;
            }
            else if (verticalDirection == Vector2.down)
            {
                _currenVerticaltAnimation = AnimationCommand.Fall;
            }
            else
            {
                return;
            }

            _animator.SetBool(_currenVerticaltAnimation, true);
        }
    }
}
