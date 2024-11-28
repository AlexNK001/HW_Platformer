using System.Collections;
using UnityEngine;

namespace Player
{
    internal class PlayerBatlleHandler : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;
        [SerializeField] private AnimationClip _attackClip;
        [SerializeField, Min(0f)] private float _weaponActivationTime;
        [SerializeField] private Vampirism _vampirism;

        private UserInput _userInput;
        private WaitForSeconds _weaponActivationDelay;
        private WaitForSeconds _attackDuration;

        internal bool IsAttack;

        private void OnDestroy()
        {
            _userInput.Attacked -= TryAttack;
        }

        internal void Initilization(UserInput userInput)
        {
            _userInput = userInput;
            _userInput.Attacked += TryAttack;
            _weapon.SetActiveCollider(false);
            _weaponActivationDelay = new WaitForSeconds(_weaponActivationTime);
            _attackDuration = new WaitForSeconds(_attackClip.length - _weaponActivationTime);

            _vampirism.Initilization(userInput);
        }

        private void TryAttack()
        {
            if (IsAttack == false)
                StartCoroutine(Attack());
        }

        private IEnumerator Attack()
        {
            IsAttack = true;
            yield return _weaponActivationDelay;
            _weapon.SetActiveCollider(true);
            yield return _attackDuration;
            _weapon.SetActiveCollider(false);
            IsAttack = false;
        }
    }
}
