using UnityEngine;

namespace TDS.Game.Enemy
{
    public class EnemyWalk : MonoBehaviour
    {
        [SerializeField] private float _speed = 2;
        [SerializeField] private Transform _pointA;
        [SerializeField] private Transform _pointB;
        [SerializeField] private float _waitTimer = 3f;

        private bool _direction;
        private Transform _cachedTransform;
        private float _timer;
        private Vector3 _goToPointA;
        private Vector3 _goToPointB;

        private void Awake()
        {
            _goToPointA = _pointA.position;
            _goToPointB = _pointB.position;
                _cachedTransform = transform;
        }

        private void Update()
        {
            Patrol();
            TickTimer();
        }

        private void Patrol()
        {
            if (_timer <= 0)
                GoToPoint(_direction ? _goToPointA : _goToPointB);
        }

        private void GoToPoint(Vector3 point)
        {
            RotateToPoint(point);

            _cachedTransform.position = Vector2.MoveTowards(_cachedTransform.position,
                point, _speed * Time.deltaTime);
            _cachedTransform.position = _cachedTransform.position;
            if (!(Vector3.Distance(_cachedTransform.position, point) <= 0))
                return;
            _direction = !_direction;
            _timer = _waitTimer;
        }
        private void RotateToPoint(Vector3 point)
        {
            point.z = 0f;

            Vector3 direction = point - _cachedTransform.position;
            _cachedTransform.up = direction;
        }

        private void TickTimer() =>
            _timer -= Time.deltaTime;
    }
}