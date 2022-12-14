using UnityEngine;

namespace TDS.Game.Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Animator _animator;

        #endregion


        #region Public methods

        public void PlayShoot()
        {
            _animator.SetTrigger("Shoot");
        }

        public void SetSpeed(float speed)
        {
            _animator.SetFloat("Speed", speed);
        }

        public void PlayerDead(bool isDead)
        {
            _animator.SetBool("Dead", isDead);
        }

        #endregion
    }
}