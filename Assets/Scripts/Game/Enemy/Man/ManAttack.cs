using System.Collections;
using UnityEngine;

namespace TDS.Game.Enemy.Man
{
    public class ManAttack : MonoBehaviour
    {
        #region Variables

        [SerializeField] private EnemyAnimation _enemyAnimation;
        [SerializeField] private Enemy _enemy;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _bulletSpawnPosition;
        [SerializeField] private float _fireDelay = 0.3f;
        [SerializeField] private float _shootTime = 3f;
        [SerializeField] private float _shootDistance;
        private Player.Player _player;

        private Transform _cachedTransform;
        private float _currentPlayerPosition;
        private float _timer;
        private bool _isShoot;

        #endregion


        #region Unity lifecycle

        private void Awake()
        {
            _cachedTransform = transform;
            // StartCoroutine(ShootTimer());
            _player = FindObjectOfType<Player.Player>();
        }

        private void Update()
        {
            if (_enemy.IsDead || Player.Player.Instance.IsDead)
                return;

            _currentPlayerPosition = Vector2.Distance(transform.position, _player.transform.position);

            if (_currentPlayerPosition <= _shootDistance)
            {
                Rotate();
                if (_timer <= 0)
                    Attack();
            }

            TickTimer();
        }

        #endregion


        #region Private methods

        private void Rotate()
        {
            Vector3 playerPosition = _player.transform.position;
            playerPosition.z = 0f;

            Vector3 direction = playerPosition - _cachedTransform.position;
            _cachedTransform.up = direction;
        }

        IEnumerator ShootTimer()
        {
            yield return new WaitForSeconds(_shootTime);
            Attack();
        }
        /*  private void OnTriggerStay2D(Collider2D col)
          {
              if (_enemy.IsDead)
                  return;
              if (col.gameObject.CompareTag(Tags.Player))
              {
                  _isShoot = true;
                  Transform playerTransform = col.GetComponent<Transform>();
                  if (_timer <= 0)
                      Attack(playerTransform);
              }
          }*/

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