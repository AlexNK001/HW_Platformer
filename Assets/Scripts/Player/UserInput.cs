using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    internal class UserInput : MonoBehaviour
    {
        private const string Horizontal = nameof(Horizontal);
        private const KeyCode Jump = KeyCode.Space;
        private const KeyCode Attack = KeyCode.O;
        private const KeyCode Run = KeyCode.LeftShift;
        private const KeyCode MoveDown = KeyCode.S;
        private const KeyCode Vampiric = KeyCode.Z;

        private PlayerPhysic _playerPhysic;
        private PlayerBatlleHandler _playerBatlleHandler;

        internal event UnityAction<float> Moved;
        internal event UnityAction Jumped;
        internal event UnityAction Attacked;
        internal event UnityAction<bool> Runed;
        internal event UnityAction MovingDown;
        internal event UnityAction SpellCast;

        private void Update()
        {
            if (_playerBatlleHandler.IsAttack == false)
            {
                Moved.Invoke(Input.GetAxisRaw(Horizontal));

                if (Input.GetKeyDown(Attack))
                    Attacked.Invoke();

                if (Input.GetKeyDown(MoveDown))
                    MovingDown.Invoke();

                if (Input.GetKeyDown(Vampiric))
                    SpellCast.Invoke();

                if (Input.GetKeyDown(Jump) && _playerPhysic.IsGround)
                    Jumped.Invoke();

                Runed.Invoke(Input.GetKey(Run) && _playerPhysic.IsGround);
            }
        }

        internal void Initilization(PlayerPhysic playerPhysic, PlayerBatlleHandler playerBattleHandler)
        {
            _playerPhysic = playerPhysic;
            _playerBatlleHandler = playerBattleHandler;
        }
    }
}
