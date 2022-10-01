using TDS.Game.Enemy.Base;
using UnityEngine;

namespace TDS.Game.Enemy
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyDirectMovement : EnemyMovement
    {
        #region Variables

        [SerializeField] private EnemyPatrol enemyPatrol;
        [SerializeField] private Transform _target;
        // private Transform _target;
        private Rigidbody2D _rb;
        private Transform _cachedTransform;

        #endregion


        #region Unity lifecycle

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _cachedTransform = transform;
        }

        private void FixedUpdate()
        {
            if (!IsTargetValid())
                return;

            //enemyPatrol.enabled = false;
            MoveToTarget();
            RotateToTarget();
        }

        private void OnDisable()
        {
            SetVelocity(Vector2.zero);
        }

        #endregion


        #region Public methods

        public override void SetTarget(Transform target)
        {
            _target = target;

            if (target == null)
            {
               // enemyPatrol.enabled = true;
                SetVelocity(Vector2.zero);
            }
        }

        #endregion


        #region Private methods

        private void MoveToTarget()
        {
            Vector3 direction = (_target.position - _cachedTransform.position).normalized;
            SetVelocity(direction * Speed);
        }

        private void RotateToTarget()
        {
            // Vector3 targetPosition = _target.transform.position;
            // targetPosition.z = 0f;

            Vector3 direction = _target.position - _cachedTransform.position;
            _cachedTransform.up = direction;
        }

        private bool IsTargetValid() =>
            _target != null;

        private void SetVelocity(Vector2 velocity)
        {
            _rb.velocity = velocity;
            SetAnimationSpeed(velocity.magnitude);
        }

        #endregion
    }
}