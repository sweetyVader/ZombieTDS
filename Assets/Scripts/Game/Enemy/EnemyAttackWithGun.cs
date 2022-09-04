using System.Collections;
using TDS.Game.Player;
using UnityEngine;

namespace TDS.Game.Enemy
{
    public class EnemyAttackWithGun : MonoBehaviour
    {
        #region Variables

        private PlayerDeath _playerDeath;

        [SerializeField] private EnemyAnimation _enemyAnimation;
        [SerializeField] private EnemyDeath _enemyDeath;
        [SerializeField] private EnemyWalk _enemyWalk;

        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _bulletSpawnPosition;
        [SerializeField] private float _fireDelay = 0.3f;
        [SerializeField] private float _shootTime = 3f;
        [SerializeField] private float _shootDistance;
        [SerializeField] private int _damage = 15;

        private PlayerHp _player;

        private Transform _cachedTransform;
        private float _currentPlayerPosition;
        private float _timer;

        #endregion


        #region Properties

        public int DamageGun { get; private set; }

        #endregion


        #region Unity lifecycle

        private void Awake()
        {
            DamageGun = _damage;
            _cachedTransform = transform;
            // StartCoroutine(ShootTimer());
            _player = FindObjectOfType<PlayerHp>();
            _playerDeath = FindObjectOfType<PlayerDeath>();
        }

        private void Update()
        {
            if (_enemyDeath.IsDead || _playerDeath.IsDead)
                return;

            _currentPlayerPosition = Vector2.Distance(transform.position, _player.transform.position);

            if (_currentPlayerPosition <= _shootDistance)
            {
                _enemyWalk.enabled = false;
                Rotate();
                if (_timer <= 0)
                    Attack();
            }


            _enemyWalk.enabled = true;
            TickTimer();
        }

        #endregion


        IEnumerator ShootTimer()
        {
            if (_currentPlayerPosition <= _shootDistance)
            {
                Rotate();
                yield return new WaitForSeconds(_shootTime);
                Attack();
            }
        }


        #region Private methods

        private void Rotate()
        {
            Vector3 playerPosition = _player.transform.position;
            playerPosition.z = 0f;

            Vector3 direction = playerPosition - _cachedTransform.position;
            _cachedTransform.up = direction;
        }

        private void Attack()
        {
            _enemyAnimation.PlayShoot();
            Instantiate(_bulletPrefab, _bulletSpawnPosition.position, _cachedTransform.rotation);
            _timer = _fireDelay;
        }

        private void TickTimer()
        {
            _timer -= Time.deltaTime;
        }

        #endregion
    }
}