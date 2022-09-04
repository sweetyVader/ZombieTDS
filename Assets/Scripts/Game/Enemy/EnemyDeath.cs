using System.Collections;
using TDS.Game.Player;
using UnityEngine;

namespace TDS.Game.Enemy
{
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private EnemyHp _enemyHp;
        [SerializeField] private EnemyAnimation _enemyAnimation;
        [SerializeField] private EnemyMovement _enemyMovement;
        [SerializeField] private EnemyAttack _enemyAttack;
        [SerializeField] private EnemyAttackWithGun _enemyAttackWithGun;
        [SerializeField] private EnemyHp _hp;

        private PlayerAttack _playerAttack;


        #region Properties

        public bool IsDead { get; private set; }

        #endregion


        private void Start()
        {
            _enemyHp.OnChanged += OnHpChanged;
            _playerAttack = FindObjectOfType<PlayerAttack>();
        }

        private void OnHpChanged(int hp)
        {
            if (IsDead || hp > 0)
                return;

            IsDead = true;
            _enemyAnimation.EnemyDead();
            _enemyMovement.enabled = false;
            if (_enemyAttack != null)
                _enemyAttack.enabled = false;
            else if (_enemyAttackWithGun != null)
                _enemyAttackWithGun.enabled = false;
        }


        #region Private methods

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