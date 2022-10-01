using UnityEngine;

namespace TDS.Game.Enemy.Base
{
    [RequireComponent(typeof(EnemyFollowAgro))]
    [RequireComponent(typeof(EnemyAttackAgro))]
    public class EnemyStarter : MonoBehaviour
    {
        [SerializeField] private EnemyIdle _idle;

        public void Begin()
        {
            _idle.Activate();
        }
    }
}