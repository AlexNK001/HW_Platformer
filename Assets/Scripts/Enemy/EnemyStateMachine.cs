using UnityEngine;

namespace Enemy
{
    public class EnemyStateMachine : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private AreaAction _pursueArea;
        [SerializeField] private AreaAction _attackArea;
        [SerializeField] private PatrolState _patrol;
        [SerializeField] private ChaseState _chase;
        [SerializeField] private CombatState _combat;

        private EnemyState[] _enemyActions;
        private EnemyState _currentState;
        private Heart _heart;

        private void Update()
        {
            EnemyState state;

            if (_pursueArea.HaveCharacter)
            {
                state = _attackArea.HaveCharacter ? _combat : _chase;
            }
            else
            {
                state = _patrol;
            }

            if (_currentState != state)
                SelectEnemyAction(state);
        }

        internal void Initilization(Heart heart)
        {
            _heart = heart;
            _heart.Died += Die;

            _enemyActions = new EnemyState[3]
            {
                _patrol,
                _chase,
                _combat
            };

            foreach (EnemyState state in _enemyActions)
            {
                state.Initilization(_animator);
                state.enabled = false;
            }

            _currentState = _patrol;
            _currentState.enabled = true;
            _currentState.Enable();
        }

        private void Die()
        {
            foreach (EnemyState state in _enemyActions)
            {
                state.enabled = false;
            }
        }

        private void SelectEnemyAction(EnemyState enemyState)
        {
            _currentState.Disable();
            _currentState.enabled = false;
            _currentState = enemyState;
            _currentState.enabled = true;
            _currentState.Enable();
        }
    }
}
