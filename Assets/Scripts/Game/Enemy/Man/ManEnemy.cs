using UnityEngine;

namespace TDS.Game.Enemy.Man
{
    public class ManEnemy : MonoBehaviour
    {
        #region Variables

        [SerializeField] private ManAnimation manAnimation;
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
                manAnimation.ManDead();
            }
            else
                _hp -= 10;
        }

        #endregion
    }
}