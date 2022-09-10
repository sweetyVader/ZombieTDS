using UnityEngine;

namespace TDS.Game.Enemy
{
    public class EnemyAttackWithGun : EnemyAttack
    {
        #region Variables

        [SerializeField] private EnemyAnimation _enemyAnimation;
        [SerializeField] private EnemyWalk _enemyWalk;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _bulletSpawnPosition;
        [SerializeField] private float _fireDelay = 0.3f;
        [SerializeField] private int _damage = 15;

        private Transform _cachedTransform;
        private float _currentPlayerPosition;

        #endregion


        #region Properties

        public int DamageGun { get; private set; }

        #endregion


        #region Unity lifecycle

        private void Awake()
        {
            DamageGun = _damage;
            _cachedTransform = transform;
        }

        #endregion


        #region Private methods

        private void Rotate()
        {
            Vector3 playerPosition = PlayerHp.transform.position;
            playerPosition.z = 0f;

            Vector3 direction = playerPosition - _cachedTransform.position;
            _cachedTransform.up = direction;
        }

        protected override void InternalAttack()
        {
            _enemyWalk.enabled = false;
            Rotate();
            if (!(Timer <= 0))
                return;
            _enemyAnimation.PlayShoot();
            Instantiate(_bulletPrefab, _bulletSpawnPosition.position, _cachedTransform.rotation);
            Timer = _fireDelay;
        }

        #endregion
    }
}