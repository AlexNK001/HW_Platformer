using UnityEngine;

public class Fliper : MonoBehaviour
{
    private float _lastPosition;
    private float _currentPosition;

    private Vector3 _scaleRigth;
    private Vector3 _scaleLeft;

    private void Start()
    {
        _scaleRigth = transform.localScale;
        _scaleLeft = new Vector3
        (
            transform.localScale.x * -1,
            transform.localScale.y,
            transform.localScale.z
        );
    }

    private void Update()
    {
        _currentPosition = transform.position.x;

        if (_currentPosition > _lastPosition)
        {
            transform.localScale = _scaleRigth;
        }
        else if (_currentPosition < _lastPosition)
        {
            transform.localScale = _scaleLeft;
        }

        _lastPosition = _currentPosition;
    }
}
