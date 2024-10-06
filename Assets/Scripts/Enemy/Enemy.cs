using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private AreaAction _pursueArea;
    [SerializeField] private AreaAction _attackArea;

    [SerializeField] private PatrolState _waypointMovement;
    [SerializeField] private ChaseState _stalker;
    [SerializeField] private CombatState _attack;

    private EnemyState[] _enemyActions;

    private void Start()
    {
        _enemyActions = new EnemyState[3] { _waypointMovement, _stalker, _attack };
        SelectEnemyAction(_waypointMovement);
    }

    private void Update()
    {
        if (_pursueArea.HaveCharacter)
        {
            if (_attackArea.HaveCharacter)
            {
                SelectEnemyAction(_attack);
            }
            else
            {
                SelectEnemyAction(_stalker);
            }
        }
        else
        {
            SelectEnemyAction(_waypointMovement);
        }
    }

    private void SelectEnemyAction(EnemyState enemyState)
    {
        foreach (EnemyState state in _enemyActions)
        {
            state.enabled = state == enemyState;
        }
    }
}
