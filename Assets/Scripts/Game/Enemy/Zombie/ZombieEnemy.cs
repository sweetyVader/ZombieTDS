﻿using UnityEngine;

namespace TDS.Game.Enemy.Zombie
{
    public class ZombieEnemy : MonoBehaviour
    {
        #region Variables

        [SerializeField] private ZombieAnimation zombieAnimation;
        [SerializeField] private int _hp = 100;

        #endregion


        #region Properties

        public bool IsDead { get; private set; }

        #endregion


        #region Unity lifecycle

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (IsDead)
                return;

            if (!col.gameObject.CompareTag(Tags.Bullet))
                return;
            Destroy(col.gameObject);
            if (_hp <= 0)
            {
                IsDead = true;
                zombieAnimation.ZombieDead(IsDead);
            }
            else
                _hp -= 10;
        }

        #endregion
    }
}