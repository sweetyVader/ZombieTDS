using TDS.Game.Player;
using UnityEngine;

namespace TDS.Game.Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private int _damage = 20;
        [SerializeField] private EnemyAnimation _enemyAnimation;
        
        [SerializeField] private float _attackDelay;
        [SerializeField] private Transform _attackPoint;
        [SerializeField] private float _radius;
        [SerializeField] private LayerMask _layerMask;

        private float _timer;

        private void Update()
        {
            TickTimer();
        }

        public void Attack()
        {
            if (CanAttack())
                InternalAttack();
        }

        public void TickTimer() =>
            _timer -= Time.deltaTime;

        private void InternalAttack()
        {
            _timer = _attackDelay;
            Collider2D col = Physics2D.OverlapCircle(_attackPoint.position, _radius, _layerMask);

            if (col == null)
                return;

            _enemyAnimation.PlayAttack();
            if (col.TryGetComponent(out PlayerHp playerHp))
                playerHp.ApplyDamage(_damage);
        }

        private bool CanAttack() =>
            _timer <= 0;
    }
}