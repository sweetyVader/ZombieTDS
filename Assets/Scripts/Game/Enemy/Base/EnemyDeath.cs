using System;
using Lean.Pool;
using TDS.Game.Enemy.Base;
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
        [SerializeField] private EnemyMovement _enemyMovement;
        [SerializeField] private EnemyAttack _enemyAttack;
        [SerializeField] private EnemyAttackAgro _enemyAttackAgro;
        [SerializeField] private EnemyPatrol enemyPatrol;
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private EnemyHp _hp;
        [Range(0f, 1f)]
        [SerializeField] private float _medkitSpawnChance;
        [SerializeField] private Medkit _medkitPrefab;

        private PlayerAttack _playerAttack;

        public event Action<EnemyDeath> OnHappened;

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
            if (hp > 0)
                return;
            OnHappened?.Invoke(this);
            _enemyAnimation.EnemyDead();
            _enemyMovement.enabled = false;
            enemyPatrol.enabled = false;
            _enemyAttackAgro.enabled = false;
            _enemyAttack.enabled = false;
            SpawnMedkit();
        }

        private void SpawnMedkit()
        {
            float random = Random.Range(0f, 1f);
            if (random > _medkitSpawnChance)
                return;

            LeanPool.Spawn(_medkitPrefab, transform.position, Quaternion.identity);
        }

        #endregion
    }
}