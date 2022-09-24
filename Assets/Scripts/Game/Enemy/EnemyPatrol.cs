using TDS.Game.Enemy.Base;
using UnityEngine;

namespace TDS.Game.Enemy
{
    public class EnemyPatrol : EnemyIdle
    {
        #region Variables

        [SerializeField] private PatrolPath _path;
        [SerializeField] private EnemyMovement _movement;
        [SerializeField] private float _distanceToPoint = 1f;
        
        [SerializeField] private float _speed = 2;
        [SerializeField] private Transform _pointA;
        [SerializeField] private Transform _pointB;
        [SerializeField] private float _waitTimer = 3f;

        private bool _direction;
        private Transform _cachedTransform;
        private float _timer;
        private Vector3 _goToPointA;
        private Vector3 _goToPointB;

        #endregion



        public override void Activate()
        {
            base.Activate();
            SetCurrentPointAsTarget();
        }

        protected override void OnActiveUpdate()
        {
            base.OnActiveUpdate();

            CheckDistance();
        }

        public override void Deactivate()
        {
            base.Deactivate();
            SetTarget(null);
        }

        private void SetTarget(Transform target) =>
            _movement.SetTarget(target);

        private void CheckDistance()
        {
            if (_path.IsNear(transform.position, _distanceToPoint))
            {
                _path.SetNextPoint();
                SetCurrentPointAsTarget();
            }
        }

        private void SetCurrentPointAsTarget() =>
            SetTarget(_path.CurrentPoint());
/*
        private void Update()
        {
            Patrol();
            TickTimer();
        }

        #endregion


        #region Private methods

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

        #endregion*/
    }
}