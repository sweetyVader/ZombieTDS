﻿using UnityEngine;

namespace TDS.Game.Objects
{
    public class Explosive : MonoBehaviour
    {
        [SerializeField] private int _damage = 15;
        [SerializeField] private float _radius = 10;

        [SerializeField] private Animator _animator;

        private float _timer;
        private bool _isExplosed;

        private void Update()
        {
            TickTimer();

            if (_timer <= 0 && _isExplosed)
                Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag(Tags.Bullet) || col.gameObject.CompareTag(Tags.EnemyBullet) ||
                col.gameObject.CompareTag(Tags.Zombie) || col.gameObject.CompareTag(Tags.Player))

                if (col.gameObject.CompareTag(Tags.Bullet) || col.gameObject.CompareTag(Tags.EnemyBullet))
                    Destroy(col);

            _animator.SetTrigger("Explose");
            _timer = 2f;
            _isExplosed = true;
            Explode();
        }

        private void Explode()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(Vector3.zero, _radius);

            foreach (Collider2D col in colliders)
            {
                IHealth health = col.GetComponentInParent<IHealth>();
                if (health != null)
                {
                    health.ApplyDamage(_damage);
                }
            }
        }

        private void TickTimer() =>
            _timer -= Time.deltaTime;
    }
}