using UnityEngine;

namespace TDS.Game.Enemy
{
    public class EnemyIdle : MonoBehaviour
    {
        [SerializeField] private EnemyMoveToPlayer _enemyMoveToPlayer;

        private void Awake()
        {
        //    _enemyMoveToPlayer.enabled = false;
        }
    }
}