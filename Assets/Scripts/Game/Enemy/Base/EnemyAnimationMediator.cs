using UnityEngine;

namespace TDS.Game.Enemy.Base
{
    public class EnemyAnimationMediator : MonoBehaviour
    {
        [SerializeField] private EnemyAttackMelee _attackMelee;
        
        public void PerformDamage()
        {
            _attackMelee.PerformDamage();
        }
        
    }
}