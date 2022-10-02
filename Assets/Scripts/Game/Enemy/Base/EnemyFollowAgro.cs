using System;
using Pathfinding;
using UnityEngine;

namespace TDS.Game.Enemy.Base
{
    public class EnemyFollowAgro : MonoBehaviour
    {
       // [SerializeField] private EnemyFollow _follow;
        [SerializeField] private EnemyIdle _idle;
        [SerializeField] private EnemyBackToIdle _backToIdle;
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private AiPathEnemyMovement _aiPathEnemyMovement;
        [SerializeField] private EnemyAttackAgro _attackAgro;
       

        [Header("Obstacles")]
        [SerializeField] private LayerMask _obstacleMask;

        private bool _isInAgro;
        private Transform _cachedTransform;


        #region Unity lifecycle

        private void Awake()
        {
            _cachedTransform = transform;
        }

        private void Start()
        {
            _triggerObserver.OnStayed += OnStayed;
            _triggerObserver.OnExited += OnExited;
        }

        private void OnStayed(Collider2D other)
        {
            //
            // if (_attackAgro.IsAttack)
            // {
            //     _aiPathEnemyMovement.Deactivate();
            //     
            // }
            //
            // else 
            if (_isInAgro)
                return;

            // Vector3 currentPosition = _cachedTransform.position;
            // Vector3 direction = other.ClosestPoint(currentPosition) - (Vector2) currentPosition;
            // RaycastHit2D hit2D = Physics2D.Raycast(currentPosition, direction, direction.magnitude,
            //     _obstacleMask);
            //
            // if (hit2D.collider == null)
            // {
                 EnterFollow();
            // }
        }

        private void OnExited(Collider2D other)
        {
           
           _aiPathEnemyMovement.Activate();
           _backToIdle.Activate();
            _isInAgro = false;
        }

        private void EnterFollow()
        {
          
           
            _isInAgro = true;
            if (_idle.IsActive)
                _idle.Deactivate();
            else
                _backToIdle.Deactivate();

            _aiPathEnemyMovement.Activate();
           
        }

        #endregion
    }
}