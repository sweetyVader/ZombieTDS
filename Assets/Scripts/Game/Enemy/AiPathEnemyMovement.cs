using Pathfinding;
using TDS.Game.Enemy.Base;
using UnityEngine;

namespace TDS.Game.Enemy
{
    [RequireComponent(typeof(AIDestinationSetter))]
    [RequireComponent(typeof(Seeker))]
    public class AiPathEnemyMovement : EnemyMovement
    {
        [Header(nameof(AiPathEnemyMovement))]
        [SerializeField] private AIDestinationSetter _destinationSetter;
        [SerializeField] private AIBase _aiPath;

        private void OnEnable() =>
            _aiPath.maxSpeed = Speed;

        protected override void OnUpdate()
        {
            base.OnUpdate();
            if (_destinationSetter.target != null)
                SetAnimationSpeed(_aiPath.velocity.magnitude);
        }

        public override void SetTarget(Transform target)
        {
            _destinationSetter.target = target;
            _aiPath.canMove = target != null;

            if (target == null)
                SetAnimationSpeed(0);
        }
    }
}