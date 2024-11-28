using System;
using System.Collections;
using UnityEngine;

namespace Player
{
    internal class PlayerPhysic : MonoBehaviour
    {
        [SerializeField] private ContactFilter2D _filter;
        [SerializeField, Min(0f)] private float _raycastDistanse = 0.2f;
        [SerializeField] private CapsuleCollider2D _mainCollider;

        private UserInput _userInput;
        private RaycastHit2D[] _hits;
        private float _lastHigth;
        private Vector2 _currentVerticalDirection;
        private WaitForSeconds _delayDisablePlatform;

        internal bool IsGround { get; private set; } = true;

        internal event Action<Item> Raised;
        internal event Action<Vector2> VerticalDirectionChanged;

        private void JumpOffPlatform()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);

            if (hit.transform.TryGetComponent(out PlatformEffector2D platform))
            {
                StartCoroutine(TemporarilyDisablePlatform(platform));
            }
        }

        private void FixedUpdate()
        {
            if (_currentVerticalDirection != Vector2.up)
                IsGround = HaveGround();

            Vector2 direction = GetVerticalDirection();

            if (direction != _currentVerticalDirection)
            {
                VerticalDirectionChanged.Invoke(direction);
                _currentVerticalDirection = direction;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Item item))
            {
                Raised.Invoke(item);
            }
        }

        private void OnDestroy()
        {
            _userInput.MovingDown -= JumpOffPlatform;
        }

        internal void Initilization(UserInput userInput)
        {
            _userInput = userInput;
            _userInput.MovingDown += JumpOffPlatform;

            _hits = new RaycastHit2D[10];
            _lastHigth = transform.position.y;
            _currentVerticalDirection = Vector2.zero;

            _delayDisablePlatform = new WaitForSeconds(Mathf.Sqrt(_mainCollider.size.y * 2 / Mathf.Abs(Physics.gravity.y)));
        }

        private IEnumerator TemporarilyDisablePlatform(PlatformEffector2D platform)
        {
            float currentRotationalOffset = platform.rotationalOffset;
            platform.rotationalOffset += 180;
            yield return _delayDisablePlatform;
            platform.rotationalOffset = currentRotationalOffset;
        }

        private Vector2 GetVerticalDirection()
        {
            float currentHigth = transform.position.y;
            Vector2 verticalDirection;

            if (IsGround)
            {
                verticalDirection = Vector2.zero;
            }
            else
            {
                bool isMovingUp = _lastHigth < transform.position.y;
                verticalDirection = isMovingUp ? Vector2.up : Vector2.down;
            }

            _lastHigth = currentHigth;
            return verticalDirection;
        }

        private bool HaveGround()
        {
            int hitCount = Physics2D.Raycast(transform.position, Vector2.down, _filter, _hits, _raycastDistanse);
            return hitCount > 0;
        }
    }
}
