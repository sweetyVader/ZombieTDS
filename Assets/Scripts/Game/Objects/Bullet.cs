using System.Collections;
using Lean.Pool;
using TDS.Game.Enemy;
using TDS.Game.Player;
using UnityEngine;

namespace TDS.Game.Objects
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        #region Variables

        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _lifeTime = 3f;
        [SerializeField] private int _damage;
        

        private EnemyAttackWithGun _enemyAttackWithGun;

        private Rigidbody2D _rb;
        private IEnumerator _lifeTimeRoutine;

        
        #endregion


        #region Unity lifecycle

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            _rb.velocity = transform.up * _speed;

            _lifeTimeRoutine = LifeTimeTimer();
            StartCoroutine(_lifeTimeRoutine);
        }

        private void OnDisable()
        {
            if (_lifeTimeRoutine != null)
            {
                StopCoroutine(_lifeTimeRoutine);
                _lifeTimeRoutine = null;
            }
        }

        private void Start()
        {
            _enemyAttackWithGun = FindObjectOfType<EnemyAttackWithGun>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag(Tags.Player) || col.gameObject.CompareTag(Tags.Enemy)
                || col.gameObject.CompareTag(Tags.Barrel))
                LeanPool.Despawn(gameObject);


            if (col.gameObject.CompareTag(Tags.Player))
            {
                PlayerHp playerHp = col.gameObject.GetComponent<PlayerHp>();
                playerHp.ApplyDamage(_damage);
            }
            else if (col.gameObject.CompareTag(Tags.Enemy))
            {
                EnemyHp enemyHp = col.gameObject.GetComponent<EnemyHp>();
                enemyHp.ApplyDamage(_damage);
            }
        }

        #endregion


        IEnumerator LifeTimeTimer()
        {
            yield return new WaitForSeconds(_lifeTime);
            LeanPool.Despawn(gameObject);
        }
    }
}