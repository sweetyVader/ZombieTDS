using UnityEngine;

namespace TDS.Game.Enemy
{
    public class EnemyAttackAgro : MonoBehaviour
    {
        #region Variables

        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private EnemyAttack _attack;
        [SerializeField] private EnemyFollow _follow;

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
            _follow.Deactivate();
            _attack.Activate();
        }

        private void OnExited(Collider2D col)
        {
            _follow.Activate();
            _attack.Deactivate();
        }

        #endregion
    }
}