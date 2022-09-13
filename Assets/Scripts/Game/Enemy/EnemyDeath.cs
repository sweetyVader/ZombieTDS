using System;
using TDS.Game.Objects;
using TDS.Game.Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TDS.Game.Enemy
{
    public class EnemyDeath : MonoBehaviour
    {
        #region Variables

        [SerializeField] private EnemyHp _enemyHp;
        [SerializeField] private EnemyAnimation _enemyAnimation;
        [SerializeField] private EnemyDirectMovement _enemyDirectMovement;
        [SerializeField] private EnemyAttack _enemyAttack;
        [SerializeField] private EnemyAttackAgro _enemyAttackAgro;
        [SerializeField] private EnemyWalk _enemyWalk;
        [SerializeField] private EnemyMoveToPlayer _enemyMoveToPlayer;
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private EnemyHp _hp;
        [Range(0f, 1f)]
        [SerializeField] private float _medkitSpawnChance;
        [SerializeField] private Medkit _medkitPrefab;

        private PlayerAttack _playerAttack;

        #endregion


        #region Properties

        public bool IsDead { get; private set; }

        #endregion


        #region Unity lifecycle

        private void Start()
        {
            _enemyHp.OnChanged += OnHpChanged;
            _playerAttack = FindObjectOfType<PlayerAttack>();
        }

        #endregion


        #region Private methods

        private void OnHpChanged(int hp)
        {
            if (IsDead || hp > 0)
                return;

            IsDead = true;
            _enemyAnimation.EnemyDead();
            _enemyDirectMovement.enabled = false;
            _enemyWalk.enabled = false;
            _enemyAttackAgro.enabled = false;
            _enemyAttack.enabled = false;


            if (_enemyMoveToPlayer != null)
                _enemyMoveToPlayer.enabled = false;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (IsDead)
                return;


            if (_hp.CurrentHp > 0)
            {
                _hp.ApplyDamage(_playerAttack.PlayerDamage);
            }
            else
            {
                IsDead = true;
                _enemyAnimation.EnemyDead();
                SpawnMedkit();
            }
        }

        private void SpawnMedkit()
        {
            float random = Random.Range(0f, 1f);
            if (random > _medkitSpawnChance)
                return;

            Instantiate(_medkitPrefab, transform.position, Quaternion.identity);
        }

        #endregion
    }
}