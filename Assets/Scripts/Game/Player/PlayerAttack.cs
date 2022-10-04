using System;
using Lean.Pool;
using TDS.Game.UI;
using UnityEngine;

namespace TDS.Game.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        #region Variables

        [SerializeField] private PlayerAnimation _playerAnimation;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _bulletSpawnPosition;
        [SerializeField] private float _fireDelay = 0.3f;

        private Transform _cachedTransform;
        private float _timer;
        private BulletBar _bulletBar;
        private bool _isEmptyBullet;

        public event Action OnShoot;

        #endregion
        public int NumBullet { get; private set; }

        #region Unity lifecycle

        private void Awake()
        {
            _cachedTransform = transform;
            NumBullet = 10;
            _bulletBar = FindObjectOfType<BulletBar>();
            _bulletBar.EmptyBullet += CurrentBullet;
        }

        private void CurrentBullet(bool isEmpty) =>
            _isEmptyBullet = isEmpty;

        private void Update()
        {
            if (CanAttack())
                Attack();

            TickTimer();
        }

        #endregion


        #region Private methods

        private bool CanAttack() =>
            Input.GetButton("Fire1") && _timer <= 0 && !_isEmptyBullet;

        private void Attack()
        {
            _playerAnimation.PlayShoot();
            OnShoot?.Invoke();
            LeanPool.Spawn(_bulletPrefab, _bulletSpawnPosition.position, _cachedTransform.rotation);
            _timer = _fireDelay;
        }

        private void TickTimer() =>
            _timer -= Time.deltaTime;

        #endregion
    }
}