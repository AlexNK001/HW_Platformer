using System;
using UnityEngine;

namespace Player
{
    internal class Mover : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _walkSpeed;
        [SerializeField] private float _runSpeed;
        [SerializeField] private float _jumpForce;

        private UserInput _userInput;

        private float _currentSpeed;

        private void OnDestroy()
        {
            _userInput.Moved -= Move;
            _userInput.Jumped -= Jump;
        }

        internal void Initilization(UserInput userInput)
        {
            _userInput = userInput;
            _userInput.Moved += Move;
            _userInput.Jumped += Jump;
            _userInput.Runed += ChangeSpeed;

            _currentSpeed = _walkSpeed;
        }

        private void ChangeSpeed(bool isRun)
        {
            _currentSpeed = isRun ? _runSpeed : _walkSpeed;
        }

        private void Move(float horizontalAxisValue)
        {
            Vector3 moveDirection = Vector3.zero;
            moveDirection.x = horizontalAxisValue;
            transform.Translate(_currentSpeed * Time.deltaTime * moveDirection);
        }

        private void Jump()
        {
            _rigidbody.AddForce(Vector2.up * _jumpForce);
        }
    }
}
