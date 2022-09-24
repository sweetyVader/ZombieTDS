using UnityEngine;

namespace TDS.Game.Enemy.Base
{
    [RequireComponent(typeof(EnemyFollowAgro))]
    [RequireComponent(typeof(EnemyAttackAgro))]
    public class EnemyStarter : MonoBehaviour
    {
        [SerializeField] private EnemyIdle _idle;

        private void Start()
        {
            _idle.Activate();
        }
    }
}