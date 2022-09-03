using System.Collections;
using UnityEngine;

namespace TDS.Game.Player
{
    public class Player : MonoBehaviour
    {
        #region Variables

        [SerializeField] private PlayerAnimation _playerAnimation;
        [SerializeField] private int _hp = 100;

        private Vector3 _startPosition;

        #endregion


        #region Properties

        public bool IsDead { get; private set; }

        #endregion


        #region Unity lifecycle

        private void Awake()
        {
           
            _startPosition = transform.position;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            switch (col.gameObject.tag)
            {
                case Tags.Health:
                    _hp += 40;
                    Destroy(col.gameObject);
                    break;
                case Tags.EnemyBullet:
                    _hp -= 10;
                    Destroy(col.gameObject);
                    break;
                case Tags.Zombie:
                    _hp -= 20;
                    break;
            }

            CheckHealth();
        }

        private void CheckHealth()
        {
            if (_hp > 0)
                return;
            IsDead = true;
            _playerAnimation.PlayerDead(IsDead);
            StartCoroutine(ReloadScene());
        }

        #endregion


        #region Private methods

        private void GameOver()
        {
            SceneLoader.Instance.ReloadCurrentScene();
            IsDead = false;
            _playerAnimation.PlayerDead(IsDead);
            _hp = 100;
            transform.position = _startPosition;
        }

        IEnumerator ReloadScene()
        {
            yield return new WaitForSeconds(5f);
            GameOver();
        }

        #endregion
    }
}