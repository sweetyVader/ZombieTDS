using System.Collections;
using TDS.Game.Enemy;
using UnityEngine;

namespace TDS.Game.Player
{
    public class PlayerDeath : MonoBehaviour
    {
        [SerializeField] private PlayerHp _playerHp;
        [SerializeField] private PlayerAnimation _playerAnimation;
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerAttack _playerAttack;

        private EnemyAttackWithGun _enemyAttackWithGun;


        #region Properties

        public bool IsDead { get; private set; }

        #endregion


        private void Start()
        {
            _enemyAttackWithGun = FindObjectOfType<EnemyAttackWithGun>();
            _playerHp.OnChanged += OnHpChanged;
        }

        private void OnHpChanged(int hp)
        {
            if (IsDead || hp > 0)
                return;

            IsDead = true;
            _playerAnimation.PlayerDead(IsDead);
            _playerMovement.enabled = false;
            _playerAttack.enabled = false;
            StartCoroutine(ReloadScene());
        }

        IEnumerator ReloadScene()
        {
            yield return new WaitForSeconds(5f);
            GameOver();
        }


        #region Private methods

        private void GameOver()
        {
            SceneLoader.Instance.ReloadCurrentScene();
            IsDead = false;
            _playerAnimation.PlayerDead(IsDead);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (IsDead)
                return;

            if (!col.gameObject.CompareTag(Tags.EnemyBullet))
                return;

            Destroy(col.gameObject);

            if (_playerHp.CurrentHp > 0)
                _playerHp.ApplyDamage(_enemyAttackWithGun.DamageGun);

            else
            {
                IsDead = true;
                _playerAnimation.PlayerDead(IsDead);
            }
        }

        #endregion
    }
}