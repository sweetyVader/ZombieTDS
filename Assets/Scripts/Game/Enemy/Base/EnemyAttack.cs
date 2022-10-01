using TDS.Game.Enemy.Base;
using TDS.Game.Player;
using UnityEngine;

namespace TDS.Game.Enemy
{
    public abstract class EnemyAttack : EnemyBehaviour
    {
        #region Variables

        protected float Timer;
        protected PlayerHp PlayerHp;

        #endregion


        #region Unity lifecycle

        private void Start()
        {
            PlayerHp = FindObjectOfType<PlayerHp>();
        }

        #endregion


        protected override void OnUpdate()
        {
            base.OnUpdate();
            TickTimer();
        }

        protected override void OnActiveUpdate()
        {
            base.OnActiveUpdate();
            Attack();
        }


        #region Public methods

        private void Attack()
        {
            if (CanAttack())
                InternalAttack();
        }

        #endregion


        protected abstract void InternalAttack();


        #region Private methods

        private bool CanAttack() =>
            Timer <= 0 && PlayerHp.CurrentHp > 0;

        private void TickTimer() =>
            Timer -= Time.deltaTime;

        #endregion
    }
}