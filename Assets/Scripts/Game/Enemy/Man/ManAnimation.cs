using UnityEngine;

namespace TDS.Game.Enemy.Man
{
    public class ManAnimation : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Animator _animator;

        #endregion


        #region Public methods

        public void PlayShoot()
        {
            _animator.SetTrigger("Shoot");
        }

        public void ManDead()
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