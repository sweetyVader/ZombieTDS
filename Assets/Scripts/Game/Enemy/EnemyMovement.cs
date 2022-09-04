using System;
using UnityEngine;

namespace TDS.Game.Enemy
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private EnemyAnimation _enemyAnimation;
        [SerializeField] private float _speed = 4;

        private Transform _target;
        private Rigidbody2D _rb;
        private Transform _cachedTransform;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _cachedTransform = transform;
        }

        private void FixedUpdate()
        {
            if (!IsTargetValid())
                return;

            MoveToTarget();
            RotateToTarget();
        }

        
        private void OnDisable()
        {
            SetVelocity(Vector2.zero);
        }

        public void SetTarget(Transform target)
        {
            _target = target;

            if (target == null)
                SetVelocity(Vector2.zero);
        }

        private void MoveToTarget()
        {
            Vector3 direction = (_target.position - _cachedTransform.position).normalized;
            SetVelocity(direction * _speed);
            _enemyAnimation.SetSpeed(_speed);
        }
        private void RotateToTarget()
        {
            Vector3 targetPosition = _target.transform.position;
            targetPosition.z = 0f;

            Vector3 direction = targetPosition - _cachedTransform.position;
            _cachedTransform.up = direction;
        }

        private bool IsTargetValid() =>
            _target != null;

        private void SetVelocity(Vector2 velocity) =>
            _rb.velocity = velocity;
    }
}