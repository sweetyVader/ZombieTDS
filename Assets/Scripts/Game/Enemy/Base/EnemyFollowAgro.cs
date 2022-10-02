using UnityEngine;

namespace TDS.Game.Enemy.Base
{
    public class EnemyFollowAgro : MonoBehaviour
    {
        [SerializeField] private EnemyIdle _idle;
        [SerializeField] private EnemyBackToIdle _backToIdle;
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private AiPathEnemyMovement _aiPathEnemyMovement;

        private bool _isInAgro;


        #region Unity lifecycle

        private void Start()
        {
            _triggerObserver.OnStayed += OnStayed;
            _triggerObserver.OnExited += OnExited;
        }

        private void OnStayed(Collider2D other)
        {
            if (_isInAgro)
                return;

            EnterFollow();
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