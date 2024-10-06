using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private UserInput _userInput;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private void Start()
    {
        _userInput.Moved += Move;
        _userInput.Jumped += Jump;
    }

    private void OnDisable()
    {
        _userInput.Moved -= Move;
        _userInput.Jumped -= Jump;
    }

    private void Move(float horizontalAxisValue)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = horizontalAxisValue;
        transform.Translate(_speed * Time.deltaTime * moveDirection);
    }

    private void Jump()
    {
        _rigidbody.AddForce(Vector2.up * _jumpForce);
    }
}
