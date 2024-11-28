using UnityEngine;

namespace Player
{
    internal class EntryPoint : MonoBehaviour
    {
        [SerializeField] private UserInput _userInput;
        [SerializeField] private PlayerPhysic _playerPhysic;
        [SerializeField] private PlayerAnimator _playerAnimator;
        [SerializeField] private Mover _mover;
        [SerializeField] private Bug _bug;
        [SerializeField] private int _coinAmount;
        [SerializeField] private PlayerHeart _playerHeart;
        [SerializeField, Min(0f)] private float _maxHealth;
        [SerializeField, Min(0f)] private float _currentHealth;
        [SerializeField] private PlayerBatlleHandler _playerBattleHandler;
        [SerializeField] private UIHandler _uiHandler;

        private void Start()
        {
            _userInput.Initilization(_playerPhysic, _playerBattleHandler);
            _playerPhysic.Initilization(_userInput);
            _playerHeart.Initilization(_playerPhysic, _maxHealth, _currentHealth);
            _playerAnimator.Initilization(_userInput, _playerPhysic, _playerHeart, _currentHealth);
            _mover.Initilization(_userInput);
            _bug.Initilization(_playerPhysic, _coinAmount);
            _playerBattleHandler.Initilization(_userInput);
            _uiHandler.Initilization(_bug, _coinAmount, _playerHeart, _maxHealth, _currentHealth);
        }
    }
}
