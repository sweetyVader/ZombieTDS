using TDS.Game.Enemy.Base;
using TDS.Game.Player;
using UnityEngine;

namespace TDS.Game.Enemy
{
    public class EnemyPatrol : EnemyIdle
    {
        #region Variables

        [SerializeField] private PatrolPath _path;
        [SerializeField] private EnemyMovement _movement;
        [SerializeField] private float _distanceToPoint = 1f;

        private Transform _playerTransform;
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
            _playerTransform = FindObjectOfType<PlayerHp>().transform;
            SetTarget(_playerTransform);
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
    }
}