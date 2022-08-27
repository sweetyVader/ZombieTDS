using UnityEngine;

namespace TDS.Game.Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public void PlayShoot()
        {
            _animator.SetTrigger("Shoot");
        }

        public void SetSpeed(float speed)
        {
            _animator.SetFloat("Speed",speed);
        }
    }
}