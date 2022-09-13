using System;
using UnityEngine;

namespace TDS.Game.Enemy
{
    public class EnemyInstanceBackToIdle : EnemyBackToIdle
    {
        [SerializeField] private EnemyIdle _idle;

        private void OnEnable()
        {
            _idle.Activate();
        }
    }
}