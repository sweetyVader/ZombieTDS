using TDS.Game.Enemy.Base;
using TDS.Game.Player;
using UnityEngine;

namespace TDS.Game.Enemy
{
    public class EnemyAttackMelee : EnemyAttack
    {
        #region Variables

        [SerializeField] private EnemyAnimation _animation;

        [SerializeField] private int _damage = 20;
        [SerializeField] private EnemyAnimation _enemyAnimation;
        [SerializeField] private float _attackDelay;
        [SerializeField] private Transform _attackPoint;
        [SerializeField] private float _radius;
        [SerializeField] private LayerMask _layerMask;

        #endregion


        public void PerformDamage()
        {
            Collider2D col = Physics2D.OverlapCircle(_attackPoint.position, _radius, _layerMask);

            if (col == null)
                return;


            if (col.TryGetComponent(out PlayerHp playerHp))
                playerHp.ApplyDamage(_damage);
        }


        #region Private methods

        protected override void InternalAttack()
        {
            Timer = _attackDelay;
            _animation.PlayAttack();
        }

        #endregion
    }
}