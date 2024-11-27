using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField] private Waypoint[] _waipoints;

    private int _waypointIndex;

    public Vector3 GetNextPosition()
    {
        _waypointIndex++;
        _waypointIndex %= _waipoints.Length;
        return _waipoints[_waypointIndex].transform.position;
    }
}
