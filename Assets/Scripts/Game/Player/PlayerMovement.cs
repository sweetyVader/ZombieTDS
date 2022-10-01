using TDS.Game.InputService;
using UnityEngine;

namespace TDS.Game.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        #region Variables

        [SerializeField] private PlayerAnimation _playerAnimation;
        [SerializeField] private float _speed = 4f;

        private Rigidbody2D _rb;
        private Transform _cachedTransform;
        private Vector3 _startPosition;
        private IInputService _inputService;

        #endregion


        #region Unity lifecycle

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _cachedTransform = transform;
            _startPosition = _cachedTransform.position;
        }

        private void Update()
        {
            if (_inputService == null)
                return;
            
            Move();
            Rotate();
        }

        #endregion


        #region Constructor

        public void Construct(IInputService inputService) =>
            _inputService = inputService;

        #endregion


        #region Private methods

        private void Move()
        {
            Vector2 direction = _inputService.Axes;
            Vector3 moveDelta = direction * _speed;
            _rb.velocity = moveDelta;

            _playerAnimation.SetSpeed(direction.magnitude);
        }

        private void Rotate()
        {
            _cachedTransform.up = _inputService.LookDirection;
        }

        private void RestartPosition()
        {
            _cachedTransform.position = _startPosition;
        }

        #endregion
    }
}