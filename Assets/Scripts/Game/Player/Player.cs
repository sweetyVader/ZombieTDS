using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TDS.Game.Player
{
    public class Player : SingletonMonoBehaviour<Player>
    {
        #region Variables

        [SerializeField] private PlayerAnimation _playerAnimation;
        [SerializeField] private int _hp = 100;

        private float _timer;

        #endregion


        #region Properties

        public bool IsDead { get; private set; }

        #endregion


        #region Unity lifecycle

        private void Update()
        {
            if (IsDead)
            {
                PlayerDead();
            }

            TickTimer();
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (!col.gameObject.CompareTag(Tags.EnemyBullet))
                return;
            Destroy(col.gameObject);
            if (_hp <= 0)
            {
                IsDead = true;
                _playerAnimation.PlayerDead(IsDead);
                _timer = 15f;
            }
            else
                _hp -= 10;
        }

        #endregion


        #region Private methods

        private void PlayerDead()
        {
            if (_timer < 0)
                GameOver();
        }

        private void GameOver()
        {
            SceneLoader.Instance.ReloadCurrentScene();
            IsDead = false;
            _playerAnimation.PlayerDead(IsDead);
            _hp = 100;
        }

        private void TickTimer()
        {
            _timer -= Time.deltaTime;
        }

        #endregion
    }
}