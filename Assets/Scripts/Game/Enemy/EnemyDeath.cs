using TDS.Game.Player;
using UnityEngine;

namespace TDS.Game.Enemy
{
    public class EnemyDeath : MonoBehaviour
    {
        #region Variables

        [SerializeField] private EnemyHp _enemyHp;
        [SerializeField] private EnemyAnimation _enemyAnimation;
        [SerializeField] private EnemyMovement _enemyMovement;
        [SerializeField] private EnemyAttack _enemyAttack;
        [SerializeField] private EnemyAttackWithGun _enemyAttackWithGun;
        [SerializeField] private EnemyWalk _enemyWalk;
        [SerializeField] private EnemyMoveToPlayer _enemyMoveToPlayer;

        [SerializeField] private EnemyHp _hp;

        private PlayerAttack _playerAttack;

        #endregion


        #region Properties

        public bool IsDead { get; private set; }

        #endregion


        #region Unity lifecycle

        private void Start()
        {
            _enemyHp.OnChanged += OnHpChanged;
            _playerAttack = FindObjectOfType<PlayerAttack>();
        }

        #endregion


        #region Private methods

        private void OnHpChanged(int hp)
        {
            if (IsDead || hp > 0)
                return;

            IsDead = true;
            _enemyAnimation.EnemyDead();
            _enemyMovement.enabled = false;
            _enemyWalk.enabled = false;

            if (_enemyAttack != null)
                _enemyAttack.enabled = false;
            if (_enemyAttackWithGun != null)
                _enemyAttackWithGun.enabled = false;
            if (_enemyMoveToPlayer != null)
                _enemyMoveToPlayer.enabled = false;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (IsDead)
                return;

            if (!col.gameObject.CompareTag(Tags.Bullet))
                return;

            Destroy(col.gameObject);

            if (_hp.CurrentHp > 0)
            {
                _hp.ApplyDamage(_playerAttack.PlayerDamage);
            }
            else
            {
                IsDead = true;
                _enemyAnimation.EnemyDead();
            }
        }

        #endregion
    }
}