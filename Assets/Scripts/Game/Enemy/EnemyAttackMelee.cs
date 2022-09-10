using TDS.Game.Player;
using UnityEngine;

namespace TDS.Game.Enemy
{
    public class EnemyAttackMelee : EnemyAttack
    {
        #region Variables

        [SerializeField] private int _damage = 20;
        [SerializeField] private EnemyAnimation _enemyAnimation;
        [SerializeField] private float _attackDelay;
        [SerializeField] private Transform _attackPoint;
        [SerializeField] private float _radius;
        [SerializeField] private LayerMask _layerMask;

        #endregion


        #region Private methods

        protected override void InternalAttack()
        {
            Timer = _attackDelay;
            Collider2D col = Physics2D.OverlapCircle(_attackPoint.position, _radius, _layerMask);

            if (col == null)
                return;

            _enemyAnimation.PlayAttack();
            if (col.TryGetComponent(out PlayerHp playerHp))
                playerHp.ApplyDamage(_damage);
        }

        #endregion
    }
}