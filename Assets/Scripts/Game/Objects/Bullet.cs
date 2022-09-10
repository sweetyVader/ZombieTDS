using System;
using System.Collections;
using UnityEngine;

namespace TDS.Game.Objects
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        #region Variables

        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _lifeTime = 3f;

        private Rigidbody2D _rb;

        #endregion


        #region Unity lifecycle

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _rb.velocity = transform.up * _speed;

            StartCoroutine(LifeTimeTimer());
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag(Tags.Player) || col.gameObject.CompareTag(Tags.Enemy))
                Destroy(gameObject);
                
        }

        #endregion


        IEnumerator LifeTimeTimer()
        {
            yield return new WaitForSeconds(_lifeTime);
            Destroy(gameObject);
        }
    }
}