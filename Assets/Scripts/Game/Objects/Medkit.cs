using Lean.Pool;
using TDS.Game.Player;
using UnityEngine;

namespace TDS.Game.Objects
{
    public class Medkit : MonoBehaviour
    {
        #region Variables

        [SerializeField] private int _heal = 50;

        private PlayerHp _playerHp;

        #endregion


        #region Unity lifecycle

        private void Start()
        {
            _playerHp = FindObjectOfType<PlayerHp>();
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (!col.gameObject.CompareTag(Tags.Player))
                return;
            LeanPool.Despawn(gameObject);
            _playerHp.ApplyHeal(_heal);
        }

        #endregion
    }
}