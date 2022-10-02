using Pathfinding;
using UnityEngine;

namespace TDS.Game.Enemy
{
    public class EnemyAttackAgro : MonoBehaviour
    {
        #region Variables

        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private EnemyAttack _attack;
        [SerializeField] private AiPathEnemyMovement _aiPathEnemyMovement;
        [SerializeField] private AIBase _aiPath;

        #endregion


        #region Unity lifecycle

        private void OnEnable()
        {
            _triggerObserver.OnEntered += OnEntered;
            _triggerObserver.OnExited += OnExited;
        }

        private void OnDisable()
        {
            _triggerObserver.OnEntered -= OnEntered;
            _triggerObserver.OnExited -= OnExited;
        }

        private void OnEntered(Collider2D col)
        {
            _aiPathEnemyMovement.enabled = false;
            _aiPath.maxSpeed = 0;
            _attack.Activate();
        }

        private void OnExited(Collider2D col)
        {
            _attack.Deactivate();
            _aiPathEnemyMovement.enabled = true;
        }

        #endregion
    }
}