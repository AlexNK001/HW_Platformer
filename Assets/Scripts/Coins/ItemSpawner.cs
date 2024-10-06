using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private SpawnPoint[] _points;
    [SerializeField] private Item _prefab;

    private void Start()
    {
        for (int i = 0; i < _points.Length; i++)
        {
            Instantiate(_prefab, _points[i].transform.position, Quaternion.identity);
        }
    }
}
