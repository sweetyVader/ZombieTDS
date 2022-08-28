using System.Collections;
using UnityEngine;

namespace TDS.Game.Enemy.Zombie
{
    public class ZombieAttack : MonoBehaviour
    {
        #region Variables

        [SerializeField] private ZombieAnimation _zombieAnimation;
        [SerializeField] private ZombieEnemy _zombieEnemy;
        [SerializeField] private float _speed = 4f;
        [SerializeField] private float _shootTime = 3f;
        [SerializeField] private float _shootDistance;
        
        private Player.Player _player;
        private Transform _cachedTransform;
        private float _currentPlayerPosition;
       
        #endregion


        #region Unity lifecycle

        private void Awake()
        {
            _cachedTransform = transform;
            _player = FindObjectOfType<Player.Player>();
        }

        private void Update()
        {
            if (_zombieEnemy.IsDead || _player.IsDead)
                return;


            _currentPlayerPosition = Vector2.Distance(transform.position, _player.transform.position);

            if (_currentPlayerPosition <= _shootDistance)
            {
                Rotate();
                GoToPlayer();
            }
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag(Tags.Player))
                Attack();
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

        private void GoToPlayer()
        {
            _cachedTransform.position = Vector2.MoveTowards(_cachedTransform.position,
                _player.transform.position, _speed * Time.deltaTime);
        }

        private void Attack()
        {
            _zombieAnimation.PlayAttack();
        }

        #endregion
    }
}