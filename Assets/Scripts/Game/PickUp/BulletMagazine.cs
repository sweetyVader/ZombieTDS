using TDS.Game.UI;
using UnityEngine;

namespace TDS.Game.PickUp
{
    public class BulletMagazine : PickUpBase
    {
        #region Variables

        [SerializeField] private int _bullet = 10;

        private BulletBar _bulletBar;

        #endregion


        #region Unity lifecycle

        private void Start() =>
            _bulletBar = FindObjectOfType<BulletBar>();

      

        protected override void ApplyEffect(Collision2D col) =>
            _bulletBar.AddBullet(_bullet);

        #endregion
    }
}