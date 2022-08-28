using UnityEngine;

namespace TDS.Game.Enemy.Zombie
{
    public class ZombieAnimation : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Animator _animator;

        #endregion


        #region Public methods

        public void PlayAttack()
        {
            _animator.SetTrigger("Attack");
        }

        public void ZombieDead(bool isDead)
        {
            _animator.SetBool("Dead", isDead);
        }
     

        #endregion
    }
}