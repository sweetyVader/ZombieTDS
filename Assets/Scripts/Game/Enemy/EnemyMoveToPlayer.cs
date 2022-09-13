using TDS.Game.Player;
using UnityEngine;

namespace TDS.Game.Enemy
{
    public class EnemyMoveToPlayer : EnemyFollow
    {
        #region Variables

        [SerializeField] private EnemyMovement _movement;

        private Transform _playerTransform;

        #endregion


        #region Unity lifecycle

        private void Start()
        {
            _playerTransform = FindObjectOfType<PlayerHp>().transform;
        }

        public override void Activate()
        {
            base.Activate();
            SetTarget(_playerTransform);
        }

        public override void Deactivate()
        {
            base.Deactivate();
            SetTarget(null);
        }

        #endregion


        #region Private methods

        private void SetTarget(Transform target)
        {
            _movement.SetTarget(target);
        }

        #endregion
    }
}