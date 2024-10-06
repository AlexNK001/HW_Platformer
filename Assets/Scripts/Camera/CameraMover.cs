using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offSet;

    private void Update()
    {
        transform.position = _target.position + _offSet;
    }
}
