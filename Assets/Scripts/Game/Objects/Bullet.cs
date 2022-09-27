using System;
using System.Collections;
using TDS.Game.Enemy;
using TDS.Game.Player;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TDS.Game.Objects
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        #region Variables

        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _lifeTime = 3f;

        private PlayerHp _playerHp;
        private EnemyAttackWithGun _enemyAttackWithGun;

        private Rigidbody2D _rb;

        #endregion


        #region Unity lifecycle

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _rb.velocity = transform.up * _speed;

            StartCoroutine(LifeTimeTimer());
        }

        private void Start()
        {
            _playerHp = FindObjectOfType<PlayerHp>();
            _enemyAttackWithGun = FindObjectOfType<EnemyAttackWithGun>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag(Tags.Player) || col.gameObject.CompareTag(Tags.Enemy)
                || col.gameObject.CompareTag(Tags.Barrel))
                Destroy(gameObject);

            
            if (col.gameObject.CompareTag(Tags.Player) && _playerHp.CurrentHp > 0)
                _playerHp.ApplyDamage(_enemyAttackWithGun.DamageGun);
        }

        #endregion


        IEnumerator LifeTimeTimer()
        {
            yield return new WaitForSeconds(_lifeTime);
            Destroy(gameObject);
        }
    }
}