using System;

namespace TDS.Game
{
    public interface IHealth
    {
        #region Events

        event Action<int> OnChanged;

        #endregion


        #region Properties

        int CurrentHp { get; }
        int MaxHp { get; }

        #endregion


        void ApplyDamage(int damage);
        void ApplyHeal(int heal);
    }
}