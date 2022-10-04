using TDS.Game.Player;
using UnityEngine;

namespace TDS.Game.PickUp
{
    public class Medkit : PickUpBase
    {
        #region Variables

        [SerializeField] private int _heal = 50;

        private PlayerHp _playerHp;

        #endregion


        #region Unity lifecycle

        private void Start() =>
            _playerHp = FindObjectOfType<PlayerHp>();

        #endregion


        protected override void ApplyEffect(Collision2D col) =>
            _playerHp.ApplyHeal(_heal);
    }
}