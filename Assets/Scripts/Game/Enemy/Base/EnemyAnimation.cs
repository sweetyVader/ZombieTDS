using UnityEngine;

namespace TDS.Game.Enemy.Base
{
    public class EnemyAnimation : MonoBehaviour
    {
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Dead = Animator.StringToHash("Dead");
        private static readonly int Shoot = Animator.StringToHash("Shoot");
        
        #region Variables

        [SerializeField] private Animator _animator;
        

        #endregion


        #region Public methods

        public void PlayShoot()
        {
            _animator.SetTrigger(Shoot);
        }

        public void PlayAttack()
        {
            _animator.SetTrigger(Attack);
        }

        public void EnemyDead()
        {
            _animator.SetTrigger(Dead);
        }

        public void SetSpeed(float speed)
        {
            _animator.SetFloat(Speed, speed);
        }

        #endregion
    }
}