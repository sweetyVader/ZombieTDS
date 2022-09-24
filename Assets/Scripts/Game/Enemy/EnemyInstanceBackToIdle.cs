using TDS.Game.Enemy.Base;
using UnityEngine;

namespace TDS.Game.Enemy
{
    public class EnemyInstanceBackToIdle : EnemyBackToIdle
    {
        [SerializeField] private EnemyIdle _idle;

        public override void Activate()
        {
            base.Activate();
            _idle.Activate();
        }

       
    }
}