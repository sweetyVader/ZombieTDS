using System;
using Lean.Pool;
using Pathfinding;
using TDS.Game.Enemy.Base;
using TDS.Game.PickUp;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TDS.Game.Enemy
{
    public class EnemyDeath : MonoBehaviour
    {
        #region Variables

        [SerializeField] private EnemyHp _enemyHp;
        [SerializeField] private EnemyAnimation _enemyAnimation;
        [SerializeField] private AIBase _aiPath;
        [SerializeField] private EnemyAttack _enemyAttack;
        [SerializeField] private EnemyAttackAgro _enemyAttackAgro;
        [SerializeField] private EnemyPatrol enemyPatrol;

        [Range(0f, 1f)]
        [SerializeField] private float _spawnChance;
        [SerializeField] private PickUpInfo[] _spawnPrefabs;

        public event Action<EnemyDeath> OnHappened;

        #endregion


        #region Unity lifecycle

        private void Start() =>
            _enemyHp.OnChanged += OnHpChanged;

        #endregion


        #region Private methods

        private void OnHpChanged(int hp)
        {
            if (hp > 0)
                return;
            OnHappened?.Invoke(this);
            _enemyAnimation.EnemyDead();
            _aiPath.enabled = false;
            enemyPatrol.enabled = false;
            _enemyAttackAgro.enabled = false;
            _enemyAttack.enabled = false;
            SpawnPickUp();
        }

        private void SpawnPickUp()
        {
            if (_spawnPrefabs == null || _spawnPrefabs.Length == 0)
                return;

            float random = Random.Range(0f, 1f);
            if (random > _spawnChance)
                return;
            int chanceSum = 0;

            foreach (PickUpInfo pickUpInfo in _spawnPrefabs)
                chanceSum += pickUpInfo.SpawnChance;

            int randomChance = Random.Range(0, chanceSum);
            int currentChance = 0;
            int currentIndex = 0;

            for (int i = 0; i < _spawnPrefabs.Length; i++)
            {
                PickUpInfo pickUpInfo = _spawnPrefabs[i];
                currentChance += pickUpInfo.SpawnChance;

                if (currentChance >= randomChance)
                {
                    currentIndex = i;
                    break;
                }
            }

            PickUpBase pickUpPrefab = _spawnPrefabs[currentIndex].PickUpPrefab;
            LeanPool.Spawn(pickUpPrefab, transform.position, pickUpPrefab.transform.rotation);
        }

        #endregion
    }
}