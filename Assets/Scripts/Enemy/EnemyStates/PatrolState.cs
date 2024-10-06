using UnityEngine;

public class PatrolState : EnemyState
{
    [SerializeField] private Waypoint[] _points;
    [SerializeField] private float _walkSpeed = 3f;

    private Transform _target;

    private void Start()
    {
        _target = _points[0].transform;
    }

    private void OnEnable()
    {
        _animator.SetTrigger(EnemyAnimationCommand.Walk);
    }

    public void Update()
    {
        transform.position = Vector2.MoveTowards(
                     transform.position,
                     _target.position,
                     _walkSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Waypoint waypoint))
        {
            for (int i = 0; i < _points.Length; i++)
            {
                if (waypoint == _points[i])
                {
                    _target = _points[GetNextIndex(i)].transform;
                }
            }
        }
    }

    private int GetNextIndex(int currentIndex)
    {
        currentIndex++;
        return currentIndex < _points.Length ? currentIndex : 0;
    }
}