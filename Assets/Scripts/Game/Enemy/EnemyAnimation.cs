using UnityEngine;

namespace TDS.Game.Enemy
{
    public class EnemyAnimation : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Animator _animator;

        #endregion


        #region Public methods

        public void PlayShoot()
        {
            _animator.SetTrigger("Shoot");
        }

        public void PlayAttack()
        {
            _animator.SetTrigger("Attack");
        }
        
        public void EnemyDead()
        {
            _animator.SetTrigger("Dead");
        }

        public void SetSpeed(float speed)
        {
            _animator.SetFloat("Speed", speed);
        }

        #endregion
    }
}