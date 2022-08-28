﻿using UnityEngine;

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

        #endregion


        #region Unity lifecycle

        private void Awake()
        {
            _cachedTransform = transform;
        }

        private void Update()
        {
            if (Player.Instance.IsDead)
                return;

            if (CanAttack())
                Attack();

            TickTimer();
        }

        #endregion


        #region Private methods

        private bool CanAttack()
        {
            if (Player.Instance.IsDead)
                return false;
            return Input.GetButton("Fire1") && _timer <= 0;
        }

        private void Attack()
        {
            _playerAnimation.PlayShoot();
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